using BitBot.API;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using static BitBot.API.API_Model;

namespace BitBot.Bot
{
    public class Bot_Methods
    {

        public static async Task GetCryptocurrencyValue(ITelegramBotClient botClient, long chatId, string cryptocurrency)
        {
            botClient.SendTextMessageAsync(chatId, $"لطفا صبر کنید.\nدر حال گرفتن اطلاعات...");

            var Data = await Read_API.Response();

            foreach (var item in Data)
            {
                if (item.name_en == cryptocurrency && item.price_change_24h>0)
                {
                    botClient.SendTextMessageAsync(chatId, $"🔸Key Name: {item.key}\n🔹Market Cap: {item.market_cap}$\n🔸Price Change 24h: {item.price_change_24h}🔺\n🔹Dominance: {item.dominance}%");
                }
                else if (item.name_en == cryptocurrency && item.price_change_24h < 0)
                {
                    botClient.SendTextMessageAsync(chatId, $"🔸Key Name: {item.key}\n🔹Market Cap: {item.market_cap}$\n🔸Price Change 24h: {item.price_change_24h}🔻\n🔹Dominance: {item.dominance}%");
                }
            }
        }


        /////////////////////////////////////////////// 


        public static async Task<double> CalculateCurrencyEquivalent(string sourceSymbol, string targetSymbol, double sourceAmount)
        {


            var currencyStats = Read_API.Response();
            Result sourceCurrency = currencyStats.Result.Find(c => c.key == sourceSymbol);
            Result targetCurrency = currencyStats.Result.Find(c => c.key == targetSymbol);


            if (sourceCurrency != null && targetCurrency != null)
            {
                double sourcePrice = sourceCurrency.price;
                double targetPrice = targetCurrency.price;

                if (sourcePrice > 0 && targetPrice > 0)
                {
                    return (sourceAmount * sourcePrice) / targetPrice;
                }
            }

            return 0.0;
        }


        /////////////////////////////////////////////// 


        public static async Task<double> CalculateCurrencyToDollar(string cryptocurrency, double Amount)
        {
            var currencyStats = Read_API.Response();

            Result sourceCurrency = currencyStats.Result.Find(c => c.key == cryptocurrency);
            return sourceCurrency.price * Amount;
        }


        /////////////////////////////////////////////// 


        public static double ConvertStringToDouble(string userInput)
        {
            if (double.TryParse(userInput, out double result))
            {
                // The input is already a valid double.
                return result;
            }
            else
            {
                // Split the string into an array of strings.
                string[] parts = userInput.Split('.');

                // Get the integer part of the number.
                string integerPart = parts[0];

                // Initialize the fractional part as "0" in case it's not present.
                string fractionalPart = "0";

                // If there is a fractional part, extract it.
                if (parts.Length > 1)
                {
                    fractionalPart = parts[1];
                }

                // Convert the integer part of the number to a double.
                double integerNumber = double.Parse(integerPart);

                // Convert the fractional part of the number to a double.
                double fractionalNumber = double.Parse(fractionalPart) / Math.Pow(10, fractionalPart.Length);

                // Combine the integer and fractional parts of the number.
                double number = integerNumber + fractionalNumber;

                return number;
            }
        }
    }
}



