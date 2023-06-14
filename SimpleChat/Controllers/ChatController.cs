namespace SimpleChat.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ViewModels;

public class ChatController : Controller
{
    private static IList<KeyValuePair<string, string>> _messages = new List<KeyValuePair<string, string>>();

    public ChatController()
    {
    }

    [HttpGet]
    public IActionResult Show()
    {
        if (!_messages.Any()) return View(new ChatViewModel());

        var chatModel = new ChatViewModel()
        {
            Messages = _messages
                .Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    MessageText = m.Value
                })
                .ToList()
        };

        return View(chatModel);
    }

    [HttpPost]
    public IActionResult Send(ChatViewModel chat)
    {
        var newMessage = chat.CurrentMessage;

        _messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

        return RedirectToAction("Show");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
