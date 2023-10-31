using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace BitBot.Bot
{
    public class Bot_Engine
    {
        private static TelegramBotClient _botClient;

        public static async Task StartReceiving()
        {
            _botClient = new TelegramBotClient("Token-bot"); // Initialize the botClient
            var token = new CancellationTokenSource();
            var canceltoken = token.Token;
            var reOPT = new ReceiverOptions { AllowedUpdates = { } };
            await _botClient.ReceiveAsync(Response.OnMessage, ErrorMessage, reOPT, canceltoken);
        }

        public static async Task ErrorMessage(ITelegramBotClient telegramBot, Exception e, CancellationToken cancellation)
        {
            if (e is ApiRequestException requestException)
            {
                await _botClient.SendTextMessageAsync("", e.Message.ToString());
            }
        }

        public static async Task SendMessage(Message message)
        {
            await _botClient.SendTextMessageAsync(message.Chat.Id, $"سلام  {message.Chat.FirstName} ");

        }
    }
}
