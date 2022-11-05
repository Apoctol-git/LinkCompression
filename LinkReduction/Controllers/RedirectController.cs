using LinkReduction.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkReduction.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : Controller
    {
        private readonly RedirectHandler _handler;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string compressUrl)
        {
            var link = await _handler.GetLink(compressUrl);
            if(link.FindStatus)
            {
                return Redirect(link.CompresedLink.Link);
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
