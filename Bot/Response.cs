using BitBot.Keyboard_UI_UX;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace BitBot.Bot
{
    public class Response
    {
        public static async Task OnMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellation)
        {
            if (update.Message is Message message && message.Text != null)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    var chatId = update.Message.Chat.Id;

                    // تعریف دیکشنری
                    Dictionary<string, Action<Message>> commands = new Dictionary<string, Action<Message>>();

                    // افزودن دستورات به دیکشنری
                    commands.Add("/start", (message) =>
                    {
                        Bot_Engine.SendMessage(message);
                        Keyboard.ShowKeyboard(botClient, chatId, "دستور خود را وارد کنید:");
                    });
                    commands.Add("ارز دیجیتال 💲", (message) =>
                    {
                        botClient.SendTextMessageAsync(chatId, "لطفا یک دکمه را انتخاب کنید:", replyMarkup: Keyboard.GetInlineKeyboard());
                    });
                    commands.Add("بیت کوین", async (message) =>
                    {
                        Bot_Methods.GetCryptocurrencyValue(botClient, chatId, "Bitcoin");

                    });
                    commands.Add("اتریوم", async (message) =>
                    {
                        Bot_Methods.GetCryptocurrencyValue(botClient, chatId, "Ethereum");

                    });
                    commands.Add("قیمت لحظه ای تتر", async (message) =>
                    {
                        Bot_Methods.GetCryptocurrencyValue(botClient, chatId, "Tether");

                    });

                    commands.Add("بازگشت 🔙", async (message) =>
                    {
                        Keyboard.ShowKeyboard(botClient, chatId, "دستور خود را وارد کنید:");

                    });

                    commands.Add("ماشین حساب ارز دیجیتال 📟", async (message) =>
                    {

                        botClient.SendTextMessageAsync(chatId, "لطفا یک دکمه را انتخاب کنید:", replyMarkup: Keyboard.CalculatorKeyboard());
                    });
                    commands.Add("طلا 🔜", async (message) =>
                    {

                        botClient.SendTextMessageAsync(chatId, "در دست ساخت است مهندس ");
                    });

                    /////////////////////////////////////////////////////

                    if (message.Text.StartsWith("تبدیل بیت کوین به اتریوم 🔄", StringComparison.OrdinalIgnoreCase))
                    {
                        botClient.SendTextMessageAsync(chatId, $"تعداد بیت کوین را وارد کنید\n (3 bitcoin) = مثال ");

                    }

                    else if ((message.Text.EndsWith("bitcoin", StringComparison.OrdinalIgnoreCase)))
                    {

                        botClient.SendTextMessageAsync(chatId, $"در حال دریافت اطلاعات....\nلطفا منتظر بمانید");

                        // Check if the user's input is a valid number.
                        string userInput = message.Text.Replace("bitcoin", "").Trim();

                        double amount = Bot_Methods.ConvertStringToDouble(userInput);

                        // Calculate the equivalent amount of Ethereum and dollars.
                        double ethereumAmount = await Bot_Methods.CalculateCurrencyEquivalent("BTC", "ETH", amount);


                        // Send a response to the user with the calculated amounts.
                        botClient.SendTextMessageAsync(chatId, $"{amount} Bitcoin Equals {ethereumAmount} Ethereum");
                    }

                    /////////////////////////////////////////////////////

                    if (message.Text.StartsWith("محسابه ارزش بیت کوین 💵", StringComparison.OrdinalIgnoreCase))
                    {
                        botClient.SendTextMessageAsync(chatId, $"تعداد بیت کوین را وارد کنید\n (3 btc) = مثال ");

                    }

                    else if(message.Text.EndsWith("btc", StringComparison.OrdinalIgnoreCase))
                    {
                        botClient.SendTextMessageAsync(chatId, $"در حال دریافت اطلاعات....\nلطفا منتظر بمانید");

                        // Check if the user's input is a valid number.
                        string userInput = message.Text.Replace("btc", "").Trim();

                        double amount = Bot_Methods.ConvertStringToDouble(userInput);

                        // Calculate the equivalent amount of Ethereum and dollars.
                        double dollarAmount = await Bot_Methods.CalculateCurrencyToDollar("BTC", amount);

                        // Send a response to the user with the calculated amounts.
                        botClient.SendTextMessageAsync(chatId, $"{amount} Bitcoin Equals {dollarAmount} dollars 💵");
                    }

                    /////////////////////////////////////////////////////

                    // بررسی متن ورودی کاربر
                    bool command = commands.TryGetValue(message.Text, out Action<Message> action);

                    // در صورت وجود دستور، آن را فراخوانی کنید
                    if (action != null)
                    {
                        action(message);
                    }
                }
            }
        }
    }
}
