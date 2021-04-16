using DeezNuts.Data.Models;
using DeezNuts.Enums;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace DeezNuts.Data
{
    public static class SampleData
    {
        public static void Initialize(DeezNutsContext context)
        {
            //try {
            //    var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
            //    databaseCreator.CreateTables();
            //}
            //catch (Microsoft.Data.SqlClient.SqlException)
            //{
            //    //A SqlException will be thrown if tables already exist. So simply ignore it.
            //}

            context.Database.EnsureCreated();

            if (!context.TypedListeningActions.Any())
                context.TypedListeningActions.AddRange(BuildTypedListeningActions());

            if (!context.GeneralListeningActions.Any())
                context.GeneralListeningActions.AddRange(BuildGeneralListeningActions());

            if (!context.Messages.Any())
                context.TypedMessages.AddRange(BuildTypedMessages());

            if (!context.Products.Any())
                context.Products.AddRange(BuildProducts());

            if (!context.Settings.Any())
                context.Settings.AddRange(BuildSettings());

            context.SaveChanges();
        }

        private static TypedListeningAction[] BuildTypedListeningActions()
        {
            return new TypedListeningAction[] {
                new TypedListeningAction
                {
                    Name = "Schedule",
                    RegexMatch = "(information)|(schedule)",
                    ResponseMessageType = MessageType.Schedule,
                    NextState = SessionState.Listening
                },
                new TypedListeningAction
                {
                    Name = "Products List",
                    RegexMatch = "((((what is)|(what's) ((on )|(the )|(this )?))))?((menu)|(products)|(week))(.*)",
                    ResponseMessageType = MessageType.ProductsList,
                    NextState = SessionState.Listening
                },
                new TypedListeningAction
                {
                    Name = "Help",
                    RegexMatch = "(help)|(what do i do)",
                    ResponseMessageType = MessageType.Help,
                    NextState = SessionState.Listening
                },
                new TypedListeningAction
                {
                    Name = "Order",
                    RegexMatch = "order",
                    ResponseMessageType = MessageType.OrderResponse,
                    NextState = SessionState.Order
                }
            };
        }

        private static GeneralListeningAction[] BuildGeneralListeningActions()
        {
            return new GeneralListeningAction[] {
                new GeneralListeningAction
                {
                    Name = "Nice to meet you",
                    RegexMatch = ".*(nice)|(good) to meet you.*",
                    NextState = SessionState.Listening,
                    Responses = new Message[]
                    {
                        new Message { Text = "Enchanté!" },
                        new Message { Text = "'Sup boo!" },
                        new Message { Text = "Nuh uh! Nice to meet YOU!" },
                    }
                },
                new GeneralListeningAction
                {
                    Name = "Joke",
                    RegexMatch = ".*(what's a( (good)|(great)|(short)?)|(tell me a)|(do you have a)) joke.*",
                    NextState = SessionState.Listening,
                    Responses = new Message[]
                    {
                        new Message { Text = $"Hmm, here's a joke for you! What do you call a fish with no eyes. {Environment.NewLine}A fsh!" },
                        new Message { Text = $"How about this one? I still remember what my grandpa said before he kicked the bucket.. {Environment.NewLine}{Environment.NewLine}\"Hey how far do you think I can kick this bucket?\"" },
                        new Message { Text = $"What did the green grape say to the purple grape? {Environment.NewLine}{Environment.NewLine}\"Breathe idiot! Breathe!!!\" Laugh damnit! :))" },
                    }
                }
            };
        }

        private static TypedMessage[] BuildTypedMessages()
        {
            return new TypedMessage[] {
                new TypedMessage { Type = MessageType.IntroGreetingNew, Text = @"Hello! Nice to meet you! I take it you are interested in {COMPANYNAME}? If so, my name is {BOTNAME} and I'm your girl!" },
                new TypedMessage { Type = MessageType.IntroGreetingNew, Text = $"New phone who dis? {Environment.NewLine}{Environment.NewLine}Just kidding! This is {{BOTNAME}} from {{COMPANYNAME}}. I was told to expect a message from you!" },
                new TypedMessage { Type = MessageType.IntroGreetingNew, Text = @"Hello, you have reached {COMPANYNAME} Distribution. You're gonna love my nuts! My name is {BOTNAME}, nice to meet you!" },
                new TypedMessage { Type = MessageType.IntroGreetingReturning, Text = @"Heyyyyy you! What's up?" },
                new TypedMessage { Type = MessageType.IntroGreetingReturning, Text = @"Hello! *GLOMP*" },
                new TypedMessage { Type = MessageType.IntroGreetingReturning, Text = $"You're back!!! I missed you! NEVER LEAVE ME AGAIN >:(" },
                new TypedMessage { Type = MessageType.OrderResponse, Text = $"Oooh, exciting! What can I put in the cart for you?" },
                new TypedMessage { Type = MessageType.OrderResponse, Text = $"Hooray! What can I get for you? I'll need the product name, the size, and how many!" },
                new TypedMessage { Type = MessageType.OrderResponse, Text = $"Absolutely! Let me know what you want, what size you want, and how many you need! Make sure you get a lot of stuff otherwise I get put into the bad girl box." },
                new TypedMessage { Type = MessageType.RequestName, Text = @"It looks like we don't have a file for you! What is your name?" },
                new TypedMessage { Type = MessageType.RequestName, Text = @"Sooo.. what's your name?" },
                new TypedMessage { Type = MessageType.RequestName, Text = @"I don't think we've officially met yet! What's your name?" },
                new TypedMessage { Type = MessageType.RequestNameResponseFail, Text = @"Well that's an exotic name I think I'll never remember. Do you have a nickname or a stage name that you go by? Ooooh, maybe your cool Top Gun call sign! You can't be Iceman unless you buy me dinner first, then I'll think about it!" },
                new TypedMessage { Type = MessageType.RequestNameResponseFail, Text = @"Come on, that's not a name! What is your ACTUAL name?" },
                new TypedMessage { Type = MessageType.RequestNameResponseFail, Text = @"Ha. Ha. funny. Okay what's your name for reals?" },
                new TypedMessage { Type = MessageType.RequestNameResponseSuccess, Text = $"Aww {{CustName}}, that's a pretty name! Nice to meet you! Welcome to the {{COMPANYNAME}} family! :) What can I do for you?" },
                new TypedMessage { Type = MessageType.RequestNameResponseSuccess, Text = @"Well {CustName}, that makes us official doesn't it! Nice to meet you! How can I help you today?" },
                new TypedMessage { Type = MessageType.RequestNameResponseSuccess, Text = $"{{CustName}}, {{CustName}}, {{CUSTNAME}}... Okay now I'll never forget it! Now your birthday, SIN, home address, and bank account numbers... {Environment.NewLine}{Environment.NewLine}Kidding!" },
                new TypedMessage { Type = MessageType.ListeningActionResponseNoMatch, Text = @"Okay you got me. I have no idea what you're talking about. Don't tell my boss though, if I get in trouble the boss only feeds me tofurkey bacon for a week! Ask me about the delivery schedule or something!" },
                new TypedMessage { Type = MessageType.ListeningActionResponseNoMatch, Text = $"Haha! {Environment.NewLine}{Environment.NewLine}Wait, was that supposed to be funny? Omgod sorry! Try asking me for the menu or something, I'm good at that one!" },
                new TypedMessage { Type = MessageType.ListeningActionResponseNoMatch, Text = @"Uhhhh, what the heck is ""{INPUT}""?? Maybe you want to put in an order, see the delivery schedule, check the stock this week?" },
                new TypedMessage { Type = MessageType.ListeningActionResponseMultipleMatches, Text = $"Hold up. I'm not a bunny and you're not a frog so let's not jump ahead. {Environment.NewLine}{Environment.NewLine}Are you talking about {{ACTIONMATCHES}}?" },
                new TypedMessage { Type = MessageType.ListeningActionResponseMultipleMatches, Text = @"Slow down! Let's do one thing at a time! Did you want to start with {ACTIONMATCHES}?" },
                new TypedMessage { Type = MessageType.ListeningActionResponseMultipleMatches, Text = @"What the heck is ""{INPUT}""? Do you want to start with {ACTIONMATCHES}?" },
                new TypedMessage { Type = MessageType.Schedule,
                    Text =
@"Here's our whole deal:  

We run a bicycle delivery service booking by text only, and delivering Monday to Friday about noon to 9.

We cover Commercial to Alma and about as far south as 16th. Our usual route/schedule is about the same every weekday:

(noon) Leaving Commercial Drive
(1215-1245) Mt. Pleasent & Fairview
(1 -145) 1st Kits pass
(2-530) Downtown & Gastown
(545-7) 2nd Kits pass
(715-745) Fairview
(8ish) Mt. Pleasent
(By 9) Finishing on Commercial

On Saturdays and stat-holidays we do a half-day about noon to 4ish.

We are by appointment only, and proud to be on time so no-one is waiting around.

Please let us know your prefered time - frame well ahead(usually 3 hours is plenty) as our schedule often fills up. If your timing doesn't work with our core route we'll do our best to figure something out. 

Your satisfaction is our goal. If you are ever unhappy with any of our products please let us know right away and we will exchange it for you.

If this sounds good to you, send us your address, buzzer and suite # (or we can plan to meet outside as well) and we'll add you to the list."
                },
                new TypedMessage { Type = MessageType.Help, Text = @"Hmm, I can give you the menu for this week or if you already know what you want you can put in an order!" },
                new TypedMessage { Type = MessageType.ProductsList, Text = $"Glad you asked! Here's what we got this week: {Environment.NewLine}{Environment.NewLine}{{PRODUCTS}}" },
                new TypedMessage { Type = MessageType.ProductsList, Text = $"Let me show you! {Environment.NewLine}{Environment.NewLine}{{PRODUCTS}}" },
                new TypedMessage { Type = MessageType.ProductsList, Text = $"Okie dokie! Here's what's on the menu: {Environment.NewLine}{Environment.NewLine}{{PRODUCTS}}" },
                new TypedMessage { Type = MessageType.Error, Text = @"ERROR. ERROR. You broke me somehow! BZZRRRKK!!@#" },
           };
        }

        private static Product[] BuildProducts()
        {
            return new Product[] {
                new Product {Name="BOFA", Description="Double coffee, double cinnamon, double coffee, double everything!", IsActive = true },
                new Product {Name="Wendy's", Description="Original favourite: vanilla, coffee and cocoa! Wendy's nuts hit your tongue you'll never want to stop!", IsActive = true },
                new Product {Name="Blackened", Description="Bold and dark with slightly bitter notes. For the adventurous!", IsActive = true },
                new Product {Name="Chocolate Salted", Description="Limited run, currently out of stock. Delightfully salty with the right amount of sweetness. I could lick the salt off these nuts for hours!", IsActive = false }
            };
        }

        private static Setting[] BuildSettings()
        {
            return new Setting[] {
                new Setting {Type = SettingType.BotName, Value="Wendy" },
                new Setting {Type = SettingType.CompanyName, Value="Deez Nuts" },
                new Setting {Type = SettingType.SessionTimeoutMinutes, Value="60" }
            };
        }
    }
}
