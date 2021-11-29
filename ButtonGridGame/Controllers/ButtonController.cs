using ButtonGridGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ButtonGridGame.Controllers
{
    public class ButtonController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        Random random = new Random();
        const int GridSize = 25;
        public IActionResult Index()
        {
            if(buttons.Count < GridSize)
            {
                for (int i = 0; i < GridSize; i++)
                {
                    buttons.Add(new ButtonModel { Id = i, ButtonState = random.Next(4) });
                }
            }
            return View("Index", buttons);
        }
        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bn = int.Parse(buttonNumber);
            buttons.ElementAt(bn).ButtonState = (buttons.ElementAt(bn).ButtonState + 1) % 4;

            return View("Index", buttons);

        }
        public IActionResult ShowOneButton(int buttonNumber)
        {
            buttons.ElementAt(buttonNumber).ButtonState = (buttons.ElementAt(buttonNumber).ButtonState + 1) % 4;

            string buttonstring = RenderRazorViewToString(this, "ShowOneButton", buttons.ElementAt(buttonNumber));

            bool DidIWinYet = true;
            for (int i = 0; i < buttons.Count; i++)
            {
                if(buttons.ElementAt(i).ButtonState != buttons.ElementAt(0).ButtonState){
                    DidIWinYet = false;
                }
            }
            string messageString = "";
            if (DidIWinYet == true)
            {
                messageString = "<p>Congratulations, all the buttons are the same!</p>";
            }
            else
            {
                messageString = "<p>Try and see if you can change all the buttons color to the same color!</p>";
            }

            var package = new { part1 = buttonstring, part2 = messageString };

            return Json(package);
        }

        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine =
                    controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as
                        ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
