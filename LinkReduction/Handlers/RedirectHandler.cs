using LinkReduction.Context;
using LinkReduction.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkReduction.Handlers
{
    public class RedirectHandler
    {
        private readonly DBContext _context;

        public RedirectHandler(DBContext context)
        {
            _context = context;
        }
        public async Task<RedirectHandlerResponse> GetLink(string compressLink)
        {
            var result = new RedirectHandlerResponse();
            var compLink = await _context.CompresedLinks.FirstOrDefaultAsync(r=> r.CompressLink == compressLink);
            if (compLink != null)
            {
                result.CompresedLink = compLink;
                result.FindStatus = true;
            }
            else
            {
                result.FindStatus= false;
            }
            return result;
        }
    }
}
