using ButtonGridGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace ButtonGridGame.Controllers
{
    public class ButtonController : Controller
    {
        List<ButtonModel> buttons = new List<ButtonModel>();
        public IActionResult Index()
        {
            buttons.Add(new ButtonModel { Id = 0, ButtonState = 0 });
            buttons.Add(new ButtonModel { Id = 1, ButtonState = 1 });
            buttons.Add(new ButtonModel { Id = 2, ButtonState = 2 });
            buttons.Add(new ButtonModel { Id = 3, ButtonState = 3 });
            buttons.Add(new ButtonModel { Id = 4, ButtonState = 4 });
            return View();
        }
    }
}
