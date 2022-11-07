using IronBarCode;
using IronSoftware.Drawing;
using LinkReduction.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkReduction.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly RedirectHandler _handler;

        public BarcodeController(RedirectHandler handler)
        {
            _handler = handler;
        }

        public async Task<IActionResult> Barcode(string compressUrl)
        {
            try
            {
                var link = await _handler.GetLink(compressUrl);
                if (link.FindStatus)
                {
                    var barcode = BarcodeWriter.CreateBarcode(link.CompresedLink.Link, BarcodeWriterEncoding.QRCode);
                    var html = "<html><body>" + barcode.ToHtmlTag() + "</body></html>";
                    return Content(html, "text/html");
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
