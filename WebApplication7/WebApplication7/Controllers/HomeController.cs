using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Channels;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ImageProcess()
        {
            DateTime dt2 = DateTime.Today;

            //Add image as a watermark
            //using (var image = new MagickImage("C:/Users/Hasi/Downloads/25.jpeg"))
            //{

            /*using (var watermark = new MagickImage("C:/Users/Hasi/Desktop/my officials/my sign.png"))
            {


                image.Composite(watermark, Gravity.Southeast,CompositeOperator.Over);


                watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 1);


                image.Composite(watermark, 400, 500, CompositeOperator.Over);
            }*/



            //image.Write("C:/Users/Hasi/Downloads/watermark.jpg");
            //}



            using (var image = new MagickImage("C:/Users/Hasi/Downloads/25.jpeg"))
                {
                using (var copyright = new MagickImage("xc:none",700,900)) // -size 300x100 xc:none
                {
                    copyright.Draw(new Drawables()
                        .FillColor(new MagickColor("Black")) // -fill grey
                        .Gravity(Gravity.Northwest) // -gravity NorthWest
                        .FontPointSize(50)
                        //.TextDecoration(TextDecoration.NoRepeat
                        .Text(600, 800, "Hasini")); // -draw "text 10,10 'Copyright'"

                    image.Composite(copyright, CompositeOperator.Over); // -tile

                }

                using (var date = new MagickImage("xc:none", 1000, 100)) // -size 300x100 xc:none
                {
                    

                    date.Draw(new Drawables()
                        .FillColor(new MagickColor("Blue")) // -fill grey
                        .Gravity(Gravity.Northeast) // -gravity NorthWest
                        .FontPointSize(30)
                        .Text(50, 50, dt2.ToShortDateString())); // -draw "text 10,10 'Copyright'"

                    image.Composite(date, CompositeOperator.Over); // -tile

                }


                image.Write("C:/Users/Hasi/Downloads/watermark.jpg");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}