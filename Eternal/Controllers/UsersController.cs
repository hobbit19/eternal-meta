using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eternal.Data;
using Eternal.Models;
using Eternal.Models.ViewModels;
using Eternal.Utility;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Eternal.Controllers
{
    public class UsersController : Controller
    {

        [Authorize]
        public async Task<IActionResult> Index(int id, bool? passwordChanged = false, bool? deckDeleted = false)
        {
            var decks = await DbHelper.GetDecks();

            var userIndex = new UserIndex
            {
                User = await DbHelper.GetUser(id),
                Decks = decks.Where(d => d.UserID == id)
            };

            if (passwordChanged.GetValueOrDefault())
            {
                ViewData["PasswordChangedMessage"] = "Password successfully changed.";
            }

            if (deckDeleted.GetValueOrDefault())
            {
                ViewData["DeckDeletedMessage"] = "Your deck has been deleted.";
            }

            return View(userIndex);
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Email, Username, Password")]User newUser)
        {

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password, BCrypt.Net.BCrypt.GenerateSalt());

            newUser.Token = Guid.NewGuid().ToString();

            var emailExists = await DbHelper.EmailExists(newUser.Email);
            var usernameExists = await DbHelper.UsernameExists(newUser.Username);

            if (emailExists || usernameExists)
            {
                return RedirectToAction("Login", new { duplicateEmail = emailExists, duplicateUsername = usernameExists });
            }

            var userId = await DbHelper.AddUser(newUser);

            newUser.UserID = userId;

            await TransactionEmail.SendVerificationEmail(newUser);

            return RedirectToAction("Login", new { emailSent = true });
        }

        public async Task<IActionResult> Activate(int userId, string token)
        {
            var user = await DbHelper.GetUser(userId);
            if (user.Token == token)
            {
                await DbHelper.ActivateUser(userId);
                return RedirectToAction("Login", new { activated = true });
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> Login(bool? invalidLogin = false, bool? unverified = false, bool? emailSent = false, bool? activated = false, bool? passwordReset = false, string email = null, bool? duplicateEmail = false, bool? duplicateUsername = false)
        {
            if (unverified.GetValueOrDefault())
            {
                var user = await DbHelper.GetUserByEmail(email);

                ViewData["RegisterEmailInputValue"] = "";

                ViewData["UnverifiedEmail"] = email;

                ViewData["UnverifiedMessage"] = "Please activate your account.";
            }
            if (invalidLogin.GetValueOrDefault())
            {
                ViewData["InvalidLoginMessage"] = "Invalid Email or Password!";
            }
            if (emailSent.GetValueOrDefault())
            {
                ViewData["VerificationEmailMessage"] = "An email verification link has been sent to your inbox.";
            }
            if (activated.GetValueOrDefault())
            {
                ViewData["ActivationMessage"] = "Your account has been successfully activated.";
            }
            if (passwordReset.GetValueOrDefault())
            {
                ViewData["PasswordResetMessage"] = "Your password has been successfully reset.";
            }
            if (duplicateUsername.GetValueOrDefault())
            {
                ViewData["DuplicateUsernameMessage"] = "Username already in use!";
            }
            if (duplicateEmail.GetValueOrDefault())
            {
                ViewData["DuplicateEmailMessage"] = "Email already in use!";
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            var storedUser = await DbHelper.GetUserByEmail(user.Email);

            if (BCrypt.Net.BCrypt.Verify(user.Password, storedUser.Password) && storedUser.Active == true)
            {
                var claims = new List<Claim>()
                {
                    new Claim("UserID", storedUser.UserID.ToString()),
                    new Claim("Username", storedUser.Username)
                };

                var principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            else if (BCrypt.Net.BCrypt.Verify(user.Password, storedUser.Password) && storedUser.Active == false)
            {
                return RedirectToAction("Login", new { unverified = true, email = user.Email });
            }
            else
            {
                return RedirectToAction("Login", new { invalidLogin = true });
            }

            return Redirect("/");
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }


        [Authorize]
        public IActionResult ChangePassword(bool? invalidPassword = false)
        {
            if (invalidPassword.GetValueOrDefault())
            {
                ViewData["InvalidPasswordMessage"] = "Old Password is invalid.";
            }

            var changePassword = new ChangePassword();
            return View(changePassword);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([Bind("OldPassword, NewPassword")]ChangePassword changePassword)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            var user = await DbHelper.GetUser(userId);

            if (BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword, BCrypt.Net.BCrypt.GenerateSalt());
                await DbHelper.ChangePassword(user);
            }
            else
            {
                return RedirectToAction("ChangePassword", new { invalidPassword = true });
            }
            return RedirectToAction("Index", new { id = userId, passwordChanged = true });
        }


        public IActionResult ForgotPassword(bool? emailSent = false)
        {
            if (emailSent.GetValueOrDefault())
            {
                ViewData["PasswordResetMessage"] = "Password reset link sent to your inbox.";
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await DbHelper.GetUserByEmail(email);
            TransactionEmail.SendRecoveryEmail(user);

            return RedirectToAction("ForgotPassword", new { emailSent = true });
        }


        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            var user = await DbHelper.GetUser(int.Parse(userId));
            if (token == user.Token)
            {
                return View();
            }
            else
            {
                return Forbid();
            }
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword([Bind("UserID, NewPassword")]ResetPassword resetPassword)
        {
            var user = await DbHelper.GetUser(resetPassword.UserID);
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.NewPassword, BCrypt.Net.BCrypt.GenerateSalt());
            await DbHelper.ChangePassword(user);
            return RedirectToAction("Login", new { passwordReset = true });
        }

    }
}
