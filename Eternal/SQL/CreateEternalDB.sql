create database Eternal;
go

use Eternal;
go

create table [Card] (
CardID int identity(1,1),
[Name] varchar(75) not null,
[Type] varchar(25) not null,
[Text] varchar(250),
Factions varchar(50) not null,
[Set] varchar(75) not null,
Rarity varchar(15) not null,
Cost int not null,
ImageUrl varchar(250) not null,
primary key (CardID)
);

create table [User] (
UserID int identity(1,1),
Email varchar(300) unique not null,
Username varchar(50) unique not null,
[Password] varchar(150) not null,
Active bit default 0,
Token varchar(100) unique not null,
Reports int default 0,
Banned bit default 0,
primary key (UserID)
);

create table CardRating (
CardID int references [Card](CardID),
UserID int references [User](UserID),
[Date] date,
primary key clustered (CardID, UserID)
);

create table CardComment (
CardCommentID int identity(1,1),
CardID int references [Card](CardID),
UserID int references [User](UserID),
Comment varchar(300) not null,
Reports int default 0,
[Date] date,
primary key (CardCommentID)
);

create table CardCommentRating (
CardCommentID int references [CardComment](CardCommentID),
UserID int references [User](UserID),
primary key clustered (CardCommentID, UserID)
);

create table Deck (
DeckID int identity(1,1),
UserID int references [User](UserID),
[Name] varchar(100) not null,
Factions varchar(50) not null,
Guide varchar(max) not null,
DeckList varchar(max) not null,
[Date] date,
primary key (DeckID)
);

create table DeckRating (
DeckID int references [Deck](DeckID),
UserID int references [User](UserID),
[Date] date,
primary key clustered (DeckID, UserID)
);

create table DeckComment (
DeckCommentID int identity(1,1),
DeckID int references [Deck](DeckID),
UserID int references [User](UserID),
Comment varchar(300) not null,
Reports int default 0,
[Date] date,
primary key (DeckCommentID)
);

create table DeckCommentRating (
DeckCommentID int references [DeckComment](DeckCommentID),
UserID int references [User](UserID),
primary key clustered (DeckCommentID, UserID)
);


SET IDENTITY_INSERT [dbo].[Card] ON
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (1, N'Fire Sigil', N'Power', N'', N'Fire', N'The Empty Throne', N'Common', 0, N'~/images/cards/Fire Sigil.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (2, N'Granite Monument', N'Power', N'Depleted.  Transmute 5: 4/1 Magmahound with Charge', N'Fire', N'The Empty Throne', N'Uncommon', 0, N'~/images/cards/Granite Monument.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (3, N'Trail Stories', N'Spell', N'Reduce the cost of a spell in your hand by 1.', N'Fire', N'The Empty Throne', N'Rare', 0, N'~/images/cards/Trail Stories.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (4, N'Firemane Cub', N'Elemental Beast', N'Summon: Deal 1 damage to an enemy unit.', N'Fire', N'Omens of the Past', N'Uncommon', 1, N'~/images/cards/Firemane Cub.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (5, N'Forge Wolf', N'Beast', N'Summon: Deal 1 damage to the enemy player.', N'Fire', N'Omens of the Past', N'Common', 1, N'~/images/cards/Forge Wolf.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (6, N'Grenadin Drone', N'Grenadin', N'Summon: Play a 1/1 Grenadin.', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Grenadin Drone.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (7, N'Heavy Axe', N'Weapon', N'', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Heavy Axe.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (8, N'Hidden Shiv', N'Weapon', N'Warp', N'Fire', N'Omens of the Past', N'Uncommon', 1, N'~/images/cards/Hidden Shiv.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (9, N'Light the Fuse', N'Spell', N'Put five Firebombs into the enemy deck.', N'Fire', N'The Empty Throne', N'Legendary', 1, N'~/images/cards/Light the Fuse.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (10, N'On the Hunt', N'Spell', N'Put a 4/4 Hellhound with Charge on top of your deck.', N'Fire', N'Omens of the Past', N'Rare', 1, N'~/images/cards/On the Hunt.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (11, N'Oni Ronin', N'Oni', N'Warcry', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Oni Ronin.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (12, N'Pummel', N'Fast Spell', N'Give one of your attacking units +2/+2 this turn. Look at the top card of your deck. You may put it on the bottom.', N'Fire', N'Omens of the Past', N'Common', 1, N'~/images/cards/Pummel.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (13, N'Ruin', N'Fast Spell', N'Kill and enemy attachment.', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Ruin.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (14, N'Ruthless Stranger', N'Stranger', N'Strangers have +1 Power.', N'Fire', N'The Empty Throne', N'Uncommon', 1, N'~/images/cards/Ruthless Stranger.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (15, N'Temper', N'Fast Spell', N'Deal 1 damage. The top unit or weapon of your deck gets +1/+1.', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Temper.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (16, N'Ticking Grenadin', N'Grenadin', N'Entomb: Deal 3 damage to the enemy player.', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Ticking Grenadin.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (17, N'Torch', N'Fast Spell', N'Deal 3 damage.', N'Fire', N'The Empty Throne', N'Common', 1, N'~/images/cards/Torch.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (18, N'Time Sigil', N'Power', N'', N'Time', N'The Empty Throne', N'Common', 0, N'~/images/cards/Time Sigil.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (19, N'Amber Monument', N'Power', N'Depleted.  Transmute 5: 5/5 Rhinarc with Overwhelm.', N'Time', N'The Empty Throne', N'Uncommon', 0, N'~/images/cards/Amber Monument.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (20, N'Sand Warrior', N'Illusion Warrior', N'', N'Time', N'The Empty Throne', N'Uncommon', 0, N'~/images/cards/Sand Warrior.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (21, N'Cult Aspirant', N'Cultist', N'Overwhelm. Lifeforce: When you gain Health, Cult Aspirant gets +1/+1.', N'Time', N'Omens of the Past', N'Uncommon', 1, N'~/images/cards/Cult Aspirant.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (22, N'Envelop', N'Fast Spell', N'Put an attacking enemy unit into its owner''s hand.', N'Time', N'Omens of the Past', N'Common', 1, N'~/images/cards/Envelop.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (23, N'Humbug', N'Vermin', N'Flying', N'Time', N'The Empty Throne', N'Common', 1, N'~/images/cards/Humbug.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (24, N'Infinite Hourglass', N'Relic', N'Your units have Endurance.', N'Time', N'The Empty Throne', N'Uncommon', 1, N'~/images/cards/Infinite Hourglass.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (25, N'Initiate of the Sands', N'Mage', N'+1 Maximum Power.', N'Time', N'The Empty Throne', N'Common', 1, N'~/images/cards/Initiate of the Sands.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (26, N'Ornamental Daggers', N'Weapon', N'Echo', N'Time', N'The Empty Throne', N'Common', 1, N'~/images/cards/Ornamental Daggers.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (27, N'Predator''s Instinct', N'Spell', N'A unit gets Killer.', N'Time', N'The Empty Throne', N'Common', 1, N'~/images/cards/Predator''s Instinct.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (28, N'Sanctuary Priest', N'Cleric', N'Empower: You gain 1 Health.', N'Time', N'The Empty Throne', N'Common', 1, N'~/images/cards/Sanctuary Priest.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (29, N'Silence', N'Spell', N'Silence a unit.', N'Time', N'The Empty Throne', N'Uncommon', 1, N'~/images/cards/Silence.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (30, N'Slow', N'Spell', N'Double the cost of a card in the enemy player''s hand.', N'Time', N'The Empty Throne', N'Uncommon', 1, N'~/images/cards/Slow.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (31, N'Wandering Wisp', N'Wisp', N'{T}{T}{T}{T}{T}: At the start of your turn, Wandering Wisp gets +1/+1 for each card in your hand.', N'Time', N'Omens of the Past', N'Legendary', 1, N'~/images/cards/Wandering Wisp.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (32, N'Accelerate', N'Spell', N'Give a unit in your hand Charge.', N'Time', N'The Empty Throne', N'Common', 2, N'~/images/cards/Accelerate.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (33, N'Alluring Ember', N'Elemental', N'Flying, Charge, Warp', N'["Fire","Time"]', N'Omens of the Past', N'Uncommon', 1, N'~/images/cards/Alluring Ember.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (34, N'Noble Firemane', N'Elemental Beast', N'Pay 5 to give your units +1/+1 this turn.', N'["Fire","Time"]', N'Omens of the Past', N'Uncommon', 2, N'~/images/cards/Noble Firemane.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (35, N'Fearless Nomad', N'Rebel', N'Overwhelm', N'["Fire","Justice"]', N'The Empty Throne', N'Rare', 1, N'~/images/cards/Fearless Nomad.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (36, N'Justice Sigil', N'Power', N'', N'Justice', N'The Empty Throne', N'Common', 0, N'~/images/cards/Justice Sigil.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (37, N'Crownwatch Squire', N'Soldier', N'Ultimate: When Crownwatch Squire is a Student, he gets +2/+2.', N'Justice', N'Omens of the Past', N'Uncommon', 1, N'~/images/cards/Crownwatch Squire.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (38, N'Argenport Soldier', N'Soldier', N'', N'Justice', N'The Empty Throne', N'Common', 2, N'~/images/cards/Argenport Soldier.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (39, N'Primal Sigil', N'Power', N'', N'Primal', N'The Empty Throne', N'Common', 0, N'~/images/cards/Primal Sigil.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (40, N'Blind Storyteller', N'Explorer', N'Pay 2 and exhaust Blind Storyteller to draw a card, then discard a card.', N'Primal', N'The Empty Throne', N'Uncommon', 1, N'~/images/cards/Blind Storyteller.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (41, N'Backlash', N'Fast Spell', N'Negate an enemy spell and deal 2 damage to that player.', N'Primal', N'The Empty Throne', N'Common', 2, N'~/images/cards/Backlash.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (42, N'Dragonbreath', N'Spell', N'Mentor: The Student deals its Strength in damage to an enemy unit.', N'Primal', N'Omens of the Past', N'Common', 2, N'~/images/cards/Dragonbreath.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (43, N'Shadow Sigil', N'Power', N'', N'Shadow', N'The Empty Throne', N'Common', 0, N'~/images/cards/Shadow Sigil.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (44, N'Blood Beetle', N'Vermin', N'Infiltrate: +1 Strength and Flying.', N'Shadow', N'The Empty Throne', N'Common', 1, N'~/images/cards/Blood Beetle.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (45, N'Annihilate', N'Fast Spell', N'Kill a unit with a single faction.', N'Shadow', N'The Empty Throne', N'Uncommon', 2, N'~/images/cards/Annihilate.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (46, N'Safe Return', N'Fast Spell', N'Put one of your units into your hand and give it +1/+1.', N'["Time","Justice"]', N'The Empty Throne', N'Common', 1, N'~/images/cards/Safe Return.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (47, N'Awakened Student', N'Mystic', N'Empower: +1/+1.', N'["Time","Justice"]', N'The Empty Throne', N'Common', 2, N'~/images/cards/Awakened Student.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (48, N'Combrei Emissary', N'Mystic', N'You can play an additional power card each turn.', N'["Time","Justice"]', N'Jekk''s Bounty', N'Rare', 3, N'~/images/cards/Combrei Emissary.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (49, N'Call the Ancients', N'Spell', N'Put four 6/6 Titans with Aegis and Endurance into your deck.', N'["Time","Primal"]', N'The Empty Throne', N'Legendary', 1, N'~/images/cards/Call the Ancients.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (50, N'Storm Lynx', N'Beast', N'Ambush', N'["Time","Primal"]', N'The Empty Throne', N'Uncommon', 2, N'~/images/cards/Storm Lynx.png')
INSERT INTO [dbo].[Card] ([CardID], [Name], [Type], [Text], [Factions], [Set], [Rarity], [Cost], [ImageUrl]) VALUES (51, N'Blistersting Wasp', N'Vermin', N'Flying, Deadly', N'["Time","Shadow"]', N'Omens of the Past', N'Uncommon', 2, N'~/images/cards/Blistersting Wasp.png')
SET IDENTITY_INSERT [dbo].[Card] OFF

SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([UserID], [Email], [Username], [Password], [Active], [Token], [Reports], [Banned]) VALUES (17, N'cryer.jessie@gmail.com', N'jcryer', N'$2b$10$nDzkeCo04UnQsz9x5lu3jOQQ2LdvVEt/kraK02D7rOlnZ.TjgvPd2', 1, N'5f3e4b6e-d896-437f-8edf-dd89b38350e0', 0, 0)
SET IDENTITY_INSERT [dbo].[User] OFF

INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (1, 17, N'2017-12-12')
INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (18, 17, N'2017-12-12')
INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (19, 17, N'2017-12-12')
INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (36, 17, N'2017-12-12')
INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (39, 17, N'2017-12-12')
INSERT INTO [dbo].[CardRating] ([CardID], [UserID], [Date]) VALUES (43, 17, N'2017-12-12')

SET IDENTITY_INSERT [dbo].[Deck] ON
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (1, 17, N'Mono Fire', N'["Fire"]', N'Some information on how to play the deck.', N'[{"cardId":"1","count":50,"factions":"Fire","type":"Power","cardName":"Fire Sigil","cardCost":"0"},{"cardId":"3","count":4,"factions":"Fire","type":"Spell","cardName":"Trail Stories","cardCost":"0"},{"cardId":"4","count":4,"factions":"Fire","type":"Elemental Beast","cardName":"Firemane Cub","cardCost":"1"},{"cardId":"8","count":4,"factions":"Fire","type":"Weapon","cardName":"Hidden Shiv","cardCost":"1"},{"cardId":"7","count":4,"factions":"Fire","type":"Weapon","cardName":"Heavy Axe","cardCost":"1"},{"cardId":"6","count":4,"factions":"Fire","type":"Grenadin","cardName":"Grenadin Drone","cardCost":"1"},{"cardId":"5","count":4,"factions":"Fire","type":"Beast","cardName":"Forge Wolf","cardCost":"1"},{"cardId":"9","count":1,"factions":"Fire","type":"Spell","cardName":"Light the Fuse","cardCost":"1"}]', N'2017-12-12')
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (2, 17, N'Mono Time', N'["Time"]', N'Some information on how to play the deck.', N'[{"cardId":"18","count":50,"factions":"Time","type":"Power","cardName":"Time Sigil","cardCost":"0"},{"cardId":"20","count":4,"factions":"Time","type":"Illusion Warrior","cardName":"Sand Warrior","cardCost":"0"},{"cardId":"22","count":4,"factions":"Time","type":"Fast Spell","cardName":"Envelop","cardCost":"1"},{"cardId":"23","count":4,"factions":"Time","type":"Vermin","cardName":"Humbug","cardCost":"1"},{"cardId":"24","count":4,"factions":"Time","type":"Relic","cardName":"Infinite Hourglass","cardCost":"1"},{"cardId":"21","count":4,"factions":"Time","type":"Cultist","cardName":"Cult Aspirant","cardCost":"1"},{"cardId":"25","count":4,"factions":"Time","type":"Mage","cardName":"Initiate of the Sands","cardCost":"1"},{"cardId":"26","count":1,"factions":"Time","type":"Weapon","cardName":"Ornamental Daggers","cardCost":"1"}]', N'2017-12-12')
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (3, 17, N'Fire/Justice', N'["Fire","Justice"]', N'Some information on how to play the deck.', N'[{"cardId":"1","count":30,"factions":"Fire","type":"Power","cardName":"Fire Sigil","cardCost":"0"},{"cardId":"3","count":4,"factions":"Fire","type":"Spell","cardName":"Trail Stories","cardCost":"0"},{"cardId":"4","count":4,"factions":"Fire","type":"Elemental Beast","cardName":"Firemane Cub","cardCost":"1"},{"cardId":"8","count":4,"factions":"Fire","type":"Weapon","cardName":"Hidden Shiv","cardCost":"1"},{"cardId":"6","count":4,"factions":"Fire","type":"Grenadin","cardName":"Grenadin Drone","cardCost":"1"},{"cardId":"7","count":4,"factions":"Fire","type":"Weapon","cardName":"Heavy Axe","cardCost":"1"},{"cardId":"5","count":4,"factions":"Fire","type":"Beast","cardName":"Forge Wolf","cardCost":"1"},{"cardId":"9","count":1,"factions":"Fire","type":"Spell","cardName":"Light the Fuse","cardCost":"1"},{"cardId":"36","count":20,"factions":"Justice","type":"Power","cardName":"Justice Sigil","cardCost":"0"}]', N'2017-12-12')
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (4, 17, N'Fire/Primal', N'["Fire","Primal"]', N'Some information on how to play the deck.', N'[{"cardId":"1","count":26,"factions":"Fire","type":"Power","cardName":"Fire Sigil","cardCost":"0"},{"cardId":"3","count":4,"factions":"Fire","type":"Spell","cardName":"Trail Stories","cardCost":"0"},{"cardId":"4","count":4,"factions":"Fire","type":"Elemental Beast","cardName":"Firemane Cub","cardCost":"1"},{"cardId":"8","count":4,"factions":"Fire","type":"Weapon","cardName":"Hidden Shiv","cardCost":"1"},{"cardId":"9","count":4,"factions":"Fire","type":"Spell","cardName":"Light the Fuse","cardCost":"1"},{"cardId":"39","count":24,"factions":"Primal","type":"Power","cardName":"Primal Sigil","cardCost":"0"},{"cardId":"40","count":4,"factions":"Primal","type":"Explorer","cardName":"Blind Storyteller","cardCost":"1"},{"cardId":"41","count":4,"factions":"Primal","type":"Fast Spell","cardName":"Backlash","cardCost":"2"},{"cardId":"42","count":1,"factions":"Primal","type":"Spell","cardName":"Dragonbreath","cardCost":"2"}]', N'2017-12-12')
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (5, 17, N'Fire/Shadow', N'["Fire","Shadow"]', N'Some information on how to play the deck.', N'[{"cardId":"1","count":25,"factions":"Fire","type":"Power","cardName":"Fire Sigil","cardCost":"0"},{"cardId":"43","count":25,"factions":"Shadow","type":"Power","cardName":"Shadow Sigil","cardCost":"0"},{"cardId":"44","count":4,"factions":"Shadow","type":"Vermin","cardName":"Blood Beetle","cardCost":"1"},{"cardId":"45","count":4,"factions":"Shadow","type":"Fast Spell","cardName":"Annihilate","cardCost":"2"},{"cardId":"17","count":4,"factions":"Fire","type":"Fast Spell","cardName":"Torch","cardCost":"1"},{"cardId":"9","count":4,"factions":"Fire","type":"Spell","cardName":"Light the Fuse","cardCost":"1"},{"cardId":"16","count":4,"factions":"Fire","type":"Grenadin","cardName":"Ticking Grenadin","cardCost":"1"},{"cardId":"12","count":4,"factions":"Fire","type":"Fast Spell","cardName":"Pummel","cardCost":"1"},{"cardId":"11","count":1,"factions":"Fire","type":"Oni","cardName":"Oni Ronin","cardCost":"1"}]', N'2017-12-12')
INSERT INTO [dbo].[Deck] ([DeckID], [UserID], [Name], [Factions], [Guide], [DeckList], [Date]) VALUES (6, 17, N'Time/Primal', N'["Time","Primal"]', N'Some information on how to play the deck.', N'[{"cardId":"18","count":25,"factions":"Time","type":"Power","cardName":"Time Sigil","cardCost":"0"},{"cardId":"39","count":25,"factions":"Primal","type":"Power","cardName":"Primal Sigil","cardCost":"0"},{"cardId":"31","count":4,"factions":"Time","type":"Wisp","cardName":"Wandering Wisp","cardCost":"1"},{"cardId":"40","count":4,"factions":"Primal","type":"Explorer","cardName":"Blind Storyteller","cardCost":"1"},{"cardId":"50","count":4,"factions":"[\"Time\",\"Primal\"]","type":"Beast","cardName":"Storm Lynx","cardCost":"2"},{"cardId":"49","count":4,"factions":"[\"Time\",\"Primal\"]","type":"Spell","cardName":"Call the Ancients","cardCost":"1"},{"cardId":"42","count":4,"factions":"Primal","type":"Spell","cardName":"Dragonbreath","cardCost":"2"},{"cardId":"41","count":4,"factions":"Primal","type":"Fast Spell","cardName":"Backlash","cardCost":"2"},{"cardId":"32","count":1,"factions":"Time","type":"Spell","cardName":"Accelerate","cardCost":"2"}]', N'2017-12-12')
SET IDENTITY_INSERT [dbo].[Deck] OFF

INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (1, 17, N'2017-12-12')
INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (2, 17, N'2017-12-12')
INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (3, 17, N'2017-12-12')
INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (4, 17, N'2017-12-12')
INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (5, 17, N'2017-12-12')
INSERT INTO [dbo].[DeckRating] ([DeckID], [UserID], [Date]) VALUES (6, 17, N'2017-12-12')