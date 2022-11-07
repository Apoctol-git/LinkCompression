using HashidsNet;
using LinkReduction.Context;
using LinkReduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LinkReduction.Handlers
{
    public class CompressLinkHandler
    {
        private readonly DBContext _context;

        public CompressLinkHandler(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// TODO: Заполнить описание
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// 
        public async Task<CompresedHandlerResponse> Compress(string url)
        {
            var result = new CompresedHandlerResponse();
            var existedRecord = await TryGetExistLink(url);
            result.ValidStatus = ValidateLink(url);
            if (result.ValidStatus)
            {
                if (existedRecord == null)
                {
                    CompresedLink createdRecord = await CreateCompressLink(url);
                    result.CompresedLink = createdRecord;
                    result.ExistedStatus = false;
                    return result;
                }
                result.CompresedLink = existedRecord;
                result.ExistedStatus = true;
                return result;
            }
            else
            {
                result.ValidStatus = false;
                return result;
            }
        }

        private bool ValidateLink(string url)
        {
            var isHttp = url.Substring(0, 4) == "http";
            var isHttps = url.Substring(0, 5) == "https";
            return isHttp&& isHttps;
        }

        /// <summary>
        /// Метод спроектирован так, что весь на сервис НЕЛЬЗЯ РАЗВЕРНУТЬ БОЛЬШЕ ОДНОЙ НОДЫ НА БАЗУ. 
        /// Есть пару идей как разобраться с этим вопрос с помощью базы или инфрааструктуры. Но это за рамками ТЗ
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<CompresedLink> CreateCompressLink(string url)
        {
            var max = await _context.CompresedLinks.MaxAsync(o => (int?)o.Id);
            var newRecord = new CompresedLink();
            if (max!=null)
            {
                newRecord.Id = (int)max + 1;
            }
            else
            {
                newRecord.Id = 1;
            }
            newRecord.Link = url;
            newRecord.CompressLink = CompressLink(url, newRecord.Id);
            _context.CompresedLinks.Add(newRecord);
            _context.SaveChanges();
            return newRecord;
        }

        private string CompressLink(string url, int id)
        {
            var hashid = new Hashids(url);
            var result = hashid.Encode(id);
            return result;
        }

        private async Task<CompresedLink> TryGetExistLink(string url)
        {
            return await _context.CompresedLinks.FirstOrDefaultAsync(x => x.Link == url);
        }
    }
}
