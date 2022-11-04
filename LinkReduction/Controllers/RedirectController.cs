using Microsoft.AspNetCore.Mvc;

namespace LinkReduction.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("https://google.com");
        }
    }
}
