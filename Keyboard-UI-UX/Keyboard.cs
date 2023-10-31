using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BitBot.Keyboard_UI_UX
{
    public class Keyboard
    {
        // اضافه کردن کد برای نمایش کیبورد
        public static async Task ShowKeyboard(ITelegramBotClient botClient, ChatId chatId, string text)
        {
            // Define the custom keyboard markup
            var replyMarkup = new ReplyKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        new KeyboardButton("ارز دیجیتال 💲"),
                        new KeyboardButton("ماشین حساب ارز دیجیتال 📟"),
                    },
                    new[]
                    {
                        new KeyboardButton("طلا 🔜"),
                    },
                }
            );


            // Send a message with the custom keyboard markup
            await botClient.SendTextMessageAsync(chatId, "دستور خود را وارد کنید", replyMarkup: replyMarkup);
        }

        ////////////////////////////////////////////////////


        public static ReplyKeyboardMarkup GetInlineKeyboard()
        {
            var replyMarkup = new ReplyKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        new KeyboardButton("بیت کوین"),
                        new KeyboardButton("اتریوم"),
                    },
                    new[]
                    {
                        new KeyboardButton("قیمت لحظه ای تتر"),
                    },
                    new[]
                    {
                        new KeyboardButton("بازگشت 🔙"), // Use a regular text button for "Back"
                    },
                }
            );

            return replyMarkup;
        }



        ////////////////////////////////////////////////////



        public static ReplyKeyboardMarkup CalculatorKeyboard()
        {
            var replyMarkup = new ReplyKeyboardMarkup
            (
                new[]
                {
                    new[]
                    {
                        new KeyboardButton("تبدیل بیت کوین به اتریوم 🔄"),
                        new KeyboardButton("محسابه ارزش بیت کوین 💵"),
                    },
                    new[]
                    {
                        new KeyboardButton("بازگشت 🔙"), // Use a regular text button for "Back"
                    },
                }
            );

            return replyMarkup;
        }
    }
}
