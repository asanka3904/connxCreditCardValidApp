using atm.EceptionHandle;
using atm.Enum;
using atm.Models;
using atm.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;

namespace atm.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreditCard _credcardservice;

        public HomeController(ILogger<HomeController> logger, ICreditCard creditCard)
        {
            _logger = logger;
            _credcardservice = creditCard;
        }




        [HttpGet]
        public IActionResult Index()
        {
            //call logger . if need should config globally 
            _logger.LogInformation("Request Index Logged on {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);

            object obj = TempData.Peek("Items");
            var data = obj == null ? null : JsonConvert.DeserializeObject<List<CreditCardModel>>((string)obj);

            TempData.Remove("Items");

            return View(data);


        }


        [HttpPost]
        public IActionResult ValidateCard(List<string> dynamicField)
        {
            //call logger . if need should config globally 
            _logger.LogInformation("Request ValidateCard Logged on {PlaceHolderName:MMMM dd, yyyy}", DateTimeOffset.UtcNow);

            if(dynamicField == null)
            {
                return View();
            }
            var result = _credcardservice.checkCard(dynamicField);
           
            
            TempData["Items"] = JsonConvert.SerializeObject( result);
            return RedirectToAction("Index", "Home",result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}