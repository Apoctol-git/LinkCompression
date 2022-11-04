using LinkReduction.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkReduction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompressLinkController : Controller
    {
        private readonly CompressLinkHandler _handler;
        [HttpPost]
        public async Task<IActionResult> Index(string url)
        {
            try
            {
                //var a = Microsoft.AspNetCore.Http.StatusCodes.Status302Found;
                var compresedHandlerResponse = await _handler.Compress(url);
                if (compresedHandlerResponse.ExistedStatus)
                {
                   return Ok(compresedHandlerResponse.CompresedLink.CompressLink);
                }
                else
                {

                    return Created(compresedHandlerResponse.CompresedLink.CompressLink, compresedHandlerResponse.CompresedLink);
                }
            }
            catch (System.Exception)
            {
                // Если хватит времени - сделать разветвлённую систему ответа
                return BadRequest();
            } 
        }
    }
}
