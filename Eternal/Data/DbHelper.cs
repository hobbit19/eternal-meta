using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Eternal.Models;
using Eternal.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Diagnostics;

namespace Eternal.Data
{
    public class DbHelper
    {
        private const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Eternal;Trusted_Connection=True;MultipleActiveResultSets=true";

        private static IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        // ------------- Card -------------

        // Get Card
        public static async Task<Card> GetCard(int cardId)
        {
            var sql = @"
            SELECT * FROM [Card]
            WHERE CardID = @cardId;";

            Card card;

            using (var conn = CreateConnection())
            {
                card = await conn.QueryFirstAsync<Card>(sql, new { cardId });
            }

            return card;
        }

        // Get all Card
        public static async Task<IEnumerable<Card>> GetCards()
        {
            var sql = @"
            SELECT * FROM [Card];";

            IEnumerable<Card> cards;

            using (var conn = CreateConnection())
            {
                cards = await conn.QueryAsync<Card>(sql);
            }

            return cards;
        }

        // Get Featured Cards
        public static async Task<IEnumerable<FeaturedCard>> GetFeaturedCards()
        {
            var last30Days = DateTime.Today.AddDays(-30);

            var sql = @"
            SELECT TOP 6 Card.CardID, Card.ImageUrl
            FROM Card
            INNER JOIN CardRating ON Card.CardID = CardRating.CardID
            WHERE Date >= @last30Days
            GROUP BY Card.CardID, Card.ImageUrl
            ORDER BY COUNT(*) DESC;";

            IEnumerable<FeaturedCard> featuredCards;

            using (var conn = CreateConnection())
            {
                featuredCards = await conn.QueryAsync<FeaturedCard>(sql, new { last30Days });
            }

            return featuredCards;
        }


        // ------------- CardRating ---------------------------------------------------------------------

        // Add CardRating
        public static async Task AddCardRating(int cardId, int userId)
        {
            var date = DateTime.Today;

            var sql = @"
            INSERT INTO CardRating
            VALUES (@cardId, @userId, @date);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { cardId, userId, date });
            }
        }

        // Get CardRating for CardID and UserID
        public static async Task<int> GetUserCardRating(int cardId, int userId)
        {
            var sql = @"
            SELECT * FROM CardRating
            WHERE EXISTS (SELECT * FROM CardRating WHERE CardID = @cardId AND UserID = @userId)
            AND CardID = @cardId AND UserID = @userId;";

            IEnumerable<int> rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryAsync<int>(sql, new { cardId, userId });
            }

            if (rating.Count() == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // Get all CardRating for CardID
        public static async Task<int> GetCardLikes(int cardId)
        {
            var sql = @"
            SELECT COUNT(*)
            FROM CardRating
            WHERE CardID = @cardId;";

            int rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryFirstAsync<int>(sql, new { cardId });
            }

            return rating;
        }

        // Remove CardRating
        public static async Task RemoveCardRating(int cardId, int userId)
        {
            var sql = @"
            DELETE FROM CardRating
            WHERE CardID = @cardId AND UserID = @userId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { cardId, userId });
            }
        }



        // ------------ CardComment --------------------------------------------------------------------

        // Add CardComment
        public static async Task AddCardComment(CardComment comment)
        {
            var sql = @"
            INSERT INTO CardComment (CardID, UserID, Comment, Date)
            VALUES (@CardID, @UserID, @Comment, @Date);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, comment);
            }
        }

        // Get all CardComment
        public static async Task<IEnumerable<CardComment>> GetCardComments(int cardId)
        {
            var sql = @"
            SELECT * FROM CardComment
            WHERE CardID = @cardId;";

            IEnumerable<CardComment> comments;

            using (var conn = CreateConnection())
            {
                comments = await conn.QueryAsync<CardComment>(sql, new { cardId });
            }

            return comments;
        }

        // Edit CardComment
        public static async Task EditCardComment(int commentId, string comment)
        {
            var sql = @"
            UPDATE CardComment
            SET Comment = @comment
            WHERE CardCommentID = @commentId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId, comment });
            }
        }

        // Report CardComment
        public static async Task ReportCardComment(int commentId)
        {
            var sql = @"
            UPDATE CardComment
            SET Reports = Reports + 1
            WHERE CardCommentID = @commentId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId });
            }
        }

        // Remove CardComment
        public static async Task DeleteCardComment(int commentId)
        {
            var sql = @"
            DELETE FROM CardComment
            WHERE CardCommentID = @commentId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId });
            }
        }



        // ------------ CardCommentRating ---------------------------------------------------------------

        // Add CardCommentRating
        public static async Task AddCardCommentRating(int commentId, int userId)
        {
            var sql = @"
            INSERT INTO CardCommentRating
            VALUES (@commentId, @userId);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId, userId});
            }
        }

        // Get CardCommentRating for CommentID and UserID
        public static async Task<int> GetUserCardCommentRating(int commentId, int userId)
        {
            var sql = @"
            SELECT * FROM CardCommentRating
            WHERE EXISTS (SELECT * FROM CardCommentRating WHERE CardCommentID = @commentId AND UserID = @userId)
            AND CardCommentID = @commentId AND UserID = @userId;";

            IEnumerable<int> rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryAsync<int>(sql, new { commentId, userId });
            }

            if (rating.Count() == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // Get all CardCommentRating for CommentID
        public static async Task<int> GetCardCommentRating(int commentId)
        {
            var sql = @"
            SELECT Count(*)
            FROM CardCommentRating
            WHERE CardCommentID = @commentId;";

            int rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryFirstAsync<int>(sql, new { commentId });
            }

            return rating;
        }

        // Remove CardCommentRating
        public static async Task RemoveCardCommentRating(int commentId, int userId)
        {
            var sql = @"
            DELETE FROM CardCommentRating
            WHERE CardCommentID = @commentId and UserID = @userId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId, userId });
            }
        }



        // ----------- Deck --------------------------------------------------------------------------

        // Add Deck
        public static async Task<int> AddDeck(Deck deck)
        {
            var sql = @"
            INSERT INTO Deck (UserID, Name, Factions, Guide, DeckList, Date)
            VALUES (@UserID, @Name, @Factions, @Guide, @DeckList, @Date);
            SELECT SCOPE_IDENTITY();";

            int deckId;

            using (var conn = CreateConnection())
            {
                deckId = await conn.QueryFirstAsync<int>(sql, deck);
            }

            return deckId;
        }

        // Get Deck
        public static async Task<Deck> GetDeck(int deckId)
        {
            var sql = @"
            SELECT * FROM Deck
            WHERE DeckID = @deckId;";

            Deck deck;

            using (var conn = CreateConnection())
            {
                deck = await conn.QueryFirstAsync<Deck>(sql, new { deckId });
            }

            return deck;
        }

        // Get all Decks
        public static async Task<IEnumerable<Deck>> GetDecks()
        {
            var sql = @"
            SELECT * FROM Deck;";

            IEnumerable<Deck> decks;

            using (var conn = CreateConnection())
            {
                decks = await conn.QueryAsync<Deck>(sql);
            }

            return decks;
        }

        // UPDATE 
        public static async Task EditDeck(Deck deck)
        {
            var sql = @"
            UPDATE Deck
            SET Name = @Name, Factions = @Factions, Guide = @Guide, DeckList = @DeckList
            WHERE DeckID = @DeckID;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, deck);
            }
        }

        // DELETE
        public static async Task RemoveDeck(int deckId)
        {
            var sql = @"
            DELETE FROM Deck
            WHERE DeckID = @deckId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { deckId });
            }
        }

        // Get Featured Decks
        public static async Task<IEnumerable<FeaturedDeck>> GetFeaturedDecks()
        {
            var last30Days = DateTime.Today.AddDays(-30);

            var sql = @"
            SELECT TOP 6 Deck.DeckID, Deck.UserID, Deck.Name, Deck.Factions
            FROM Deck
            INNER JOIN DeckRating ON Deck.DeckID = DeckRating.DeckID
            WHERE DeckRating.Date >= @last30Days
            GROUP BY Deck.DeckID, Deck.UserID, Deck.Name, Deck.Factions
            ORDER BY Count(*) DESC;";

            IEnumerable<FeaturedDeck> featuredDecks;

            using (var conn = CreateConnection())
            {
                featuredDecks = await conn.QueryAsync<FeaturedDeck>(sql, new { last30Days });
            }

            return featuredDecks;
        }



        // ------------ DeckRating ----------------------------------------------------------------------

        // Add DeckRating
        public static async Task AddDeckRating(int deckId, int userId)
        {
            var date = DateTime.Today;

            var sql = @"
            INSERT INTO DeckRating
            VALUES (@deckId, @userId, @date);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { deckId, userId, date });
            }
        }

        // Get DeckRating
        public static async Task<int> GetUserDeckRating(int deckId, int userId)
        {
            var sql = @"
            SELECT * FROM DeckRating
            WHERE EXISTS (SELECT * FROM DeckRating WHERE DeckID = @deckId AND UserID = @userId)
            AND DeckID = @deckId AND UserID = @userId;";

            IEnumerable<int> rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryAsync<int>(sql, new { deckId, userId });
            }

            if (rating.Count() == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // Get all DeckRating
        public static async Task<int> GetDeckRating(int deckId)
        {
            var sql = @"
            SELECT COUNT(*)
            FROM DeckRating
            WHERE DeckID = @deckId;";

            int rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryFirstAsync<int>(sql, new { deckId });
            }

            return rating;
        }

        // DELETE
        public static async Task RemoveUserDeckRating(int deckId, int userId)
        {
            var sql = @"
            DELETE FROM DeckRating
            WHERE DeckID = @deckId AND UserID = @userId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { deckId, userId });
            }
        }



        // ----------- DeckComment --------------------------------------------------------------------

        // Add DeckComment
        public static async Task AddDeckComment(DeckComment comment)
        {
            var sql = @"
            INSERT INTO DeckComment (DeckID, UserID, Comment)
            VALUES (@DeckID, @UserID, @Comment);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, comment);
            }
        }

        // Get all DeckComment
        public static async Task<IEnumerable<DeckComment>> GetDeckComments(int deckId)
        {
            var sql = @"
            SELECT * FROM DeckComment
            WHERE DeckID = @deckId;";

            IEnumerable<DeckComment> comments;

            using (var conn = CreateConnection())
            {
                comments = await conn.QueryAsync<DeckComment>(sql, new { deckId });
            }

            return comments;
        }

        // Edit DeckComment
        public static async Task EditDeckComment(int id, string comment)
        {
            var sql = @"
            UPDATE DeckComment
            SET Comment = @comment
            WHERE DeckCommentID = @id;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { id, comment });
            }
        }

        // Report DeckComment
        public static async Task ReportDeckComment(int id)
        {
            var sql = @"
            UPDATE DeckComment
            SET Reports = Reports + 1
            WHERE DeckCommentID = @id;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { id });
            }
        }

        // Remove DeckComment
        public static async Task DeleteDeckComment(int id)
        {
            var sql = @"
            DELETE FROM DeckComment
            WHERE DeckCommentID = @id;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { id });
            }
        }



        // ----------- DeckCommentRating ----------------------------------------------------------------

        // Add DeckCommentRating
        public static async Task AddDeckCommentRating(int commentId, int userId)
        {
            var sql = @"
            INSERT INTO DeckCommentRating
            VALUES (@commentId, @userId);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId, userId });
            }
        }

        // Get DeckCommentRating 
        public static async Task<int> GetUserDeckCommentRating(int commentId, int userId)
        {
            var sql = @"
            SELECT * FROM DeckCommentRating
            WHERE EXISTS (SELECT * FROM DeckCommentRating WHERE DeckCommentID = @commentId AND UserID = @userId)
            AND DeckCommentID = @commentId AND UserID = @userId;";

            IEnumerable<int> rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryAsync<int>(sql, new { commentId, userId });
            }

            if (rating.Count() == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // Get all DeckCommentRating
        public static async Task<int> GetDeckCommentRating(int commentId)
        {
            var sql = @"
            SELECT COUNT(*)
            FROM DeckCommentRating
            WHERE DeckCommentID = @commentId;";

            int rating;

            using (var conn = CreateConnection())
            {
                rating = await conn.QueryFirstAsync<int>(sql, new { commentId });
            }

            return rating;
        }

        // Remove DeckCommentRating
        public static async Task RemoveDeckCommentRating(int commentId, int userId)
        {
            var sql = @"
            DELETE FROM DeckCommentRating
            WHERE DeckCommentID = @commentId and UserID = @userId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { commentId, userId });
            }
        }



        // ----------- DeckCard -----------------------------------------------------------------------

        // Add DeckCard
        /*public static async Task AddDeckCard(DeckCard deckCard)
        {
            var sql = @"
            INSERT INTO DeckCard
            VALUES (@DeckID, @CardID, @Count);";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, deckCard);
            }
        }*/

        // Get DeckCards
        /*public static async Task<IEnumerable<DeckCard>> GetDeckCards(int deckId)
        {
            var sql = @"
            SELECT * FROM DeckCard
            WHERE DeckID = @deckId;";

            IEnumerable<DeckCard> deckCards;

            using (var conn = CreateConnection())
            {
                deckCards = await conn.QueryAsync<DeckCard>(sql, new { deckId });
            }

            return deckCards;
        }*/

        // Remove DeckCards
        public static async Task RemoveDeckCards(int deckId)
        {
            var sql = @"
            DELETE FROM DeckCard
            WHERE DeckID = @deckId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { deckId });
            }
        }



        // -------------- User ------------------------------------------------------------------------

        // Add User
        public static async Task<int> AddUser(User user)
        {
            var sql = @"
            INSERT INTO [User] (Email, Username, Password, Token)
            VALUES (@Email, @Username, @Password, @Token);
            SELECT SCOPE_IDENTITY();";

            int id;

            using (var conn = CreateConnection())
            {
                id = await conn.QueryFirstAsync<int>(sql, user);
            }

            return id;
        }

        // Get User by Email
        public static async Task<User> GetUserByEmail(string email)
        {
            var sql = @"
            SELECT * FROM [User]
            WHERE Email = @email;";

            User user;

            using (var conn = CreateConnection())
            {
                user = await conn.QueryFirstAsync<User>(sql, new { email });
            }

            return user;
        }

        // Get User by Username
        public static async Task<User> GetUserByUsername(string username)
        {
            var sql = @"
            SELECT * FROM [User]
            WHERE EXISTS(SELECT * FROM [User] WHERE Username = @username)
            AND Username = @username;";

            IEnumerable<User> user;

            using (var conn = CreateConnection())
            {
                user = await conn.QueryAsync<User>(sql, new { username });
            }

            if (user.Count() == 0)
            {
                return null;
            }
            else
            {
                return user.First();
            }
        }

        // Get User by UserID
        public static async Task<User> GetUser(int userId)
        {
            var sql = @"
            SELECT * FROM [User]
            WHERE UserID = @userId;";

            User user;

            using (var conn = CreateConnection())
            {
                user = await conn.QueryFirstAsync<User>(sql, new { userId });
            }

            return user;
        }

        // Get Username by UserID
        public static async Task<string> GetUsername(int id)
        {
            var sql = @"
            SELECT Username FROM [User]
            WHERE UserID = @id;";

            string username;

            using (var conn = CreateConnection())
            {
                username = await conn.QueryFirstAsync<string>(sql, new { id });
            }

            return username;
        }

        // Get All User
        public static async Task<IEnumerable<User>> GetUsers()
        {
            var sql = @"
            SELECT UserID, Username 
            FROM [User];";

            IEnumerable<User> users;

            using (var conn = CreateConnection())
            {
                users = await conn.QueryAsync<User>(sql);
            }

            return users;
        }

        // Activate User
        public static async Task ActivateUser(int userId)
        {
            var sql = @"
            UPDATE [User]
            SET Active = 1
            WHERE UserID = @userId;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, new { userId });
            }
        }

        // Change Password
        public static async Task ChangePassword(User user)
        {
            var sql = @"
            UPDATE [User]
            SET Password = @Password
            WHERE UserID = @UserID;";

            using (var conn = CreateConnection())
            {
                await conn.ExecuteAsync(sql, user);
            }
        }

        // Check if Email exists
        public static async Task<bool> EmailExists(string email)
        {
            var sql = @"
            SELECT COUNT(*) FROM [User]
            WHERE Email = @email;";

            bool exists;

            using (var conn = CreateConnection())
            {
                exists = await conn.QueryFirstAsync<bool>(sql, new { email });
            }

            return exists;
        }

        // Check if Username exists
        public static async Task<bool> UsernameExists(string username)
        {
            var sql = @"
            SELECT COUNT(*) FROM [User]
            WHERE Username = @username;";

            bool exists;

            using (var conn = CreateConnection())
            {
                exists = await conn.QueryFirstAsync<bool>(sql, new { username });
            }

            return exists;
        }
    }
}
