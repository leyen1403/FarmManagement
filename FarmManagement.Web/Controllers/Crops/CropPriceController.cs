using Microsoft.AspNetCore.Mvc;

namespace FarmManagement.Web.Controllers.Crops
{
    public class CropPriceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
