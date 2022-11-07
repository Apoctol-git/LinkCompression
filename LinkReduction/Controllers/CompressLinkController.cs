using LinkReduction.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkReduction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompressLinkController : ControllerBase
    {

        private readonly CompressLinkHandler _handler;

        public CompressLinkController(CompressLinkHandler handler)
        {
            _handler = handler;
        }

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
            catch (System.Exception ex)
            {
                // Если хватит времени - сделать разветвлённую систему ответа
                return BadRequest(ex.Message+":"+ex.StackTrace);
            } 
        }
    }
}
