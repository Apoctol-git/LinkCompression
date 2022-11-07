using LinkReduction.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkReduction.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : ControllerBase
    {
        private readonly RedirectHandler _handler;

        public RedirectController(RedirectHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string compressUrl)
        {
            try
            {
                var link = await _handler.GetLink(compressUrl);
                if (link.FindStatus)
                {
                    return Redirect(link.CompresedLink.Link);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message+":"+ex.StackTrace);
            }

            
        }
    }
}
