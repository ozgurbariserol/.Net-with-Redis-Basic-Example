using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RedisDeneme1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
       readonly IMemoryCache _memoryCache;

        public RedisController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("set/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("name", name);
        }

        [HttpGet]
        public string GetName()
        {

            if(_memoryCache.TryGetValue<string>("name",out string name))
            {
                return name.Substring(3);
            }
            return "";
            //var name = _memoryCache.Get<string>("name");
            //return name.Substring(3);

        }

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

        }

        [HttpGet("GetDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }

}
