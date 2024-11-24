using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    public class Proxy : Subject
    {
        private RealSubject realSubject;
        private Dictionary<string, string> cache = new Dictionary<string, string>();
        private DateTime lastCacheClearTime;
        private TimeSpan cacheDuration = TimeSpan.FromMinutes(5);

        private bool HasAccess(string request)
        {
            return request.Contains("admin");
        }

        private void CacheRequest(string request)
        {
            if (!cache.ContainsKey(request))
            {
                cache[request] = $"[ХЕШ: {request}]";
            }
        }

        private void ClearCache()
        {
            if (DateTime.Now - lastCacheClearTime > cacheDuration)
            {
                cache.Clear();
                lastCacheClearTime = DateTime.Now;
            }
        }

        public override void Request(string request)
        {
            ClearCache();

            if (!HasAccess(request))
            {
                Console.WriteLine("Нет прав доступа");
                return;
            }

            if (cache.ContainsKey(request))
            {
                Console.WriteLine($"Хешированный результат для '{request}': {cache[request]}");
            }
            else
            {
                if (realSubject == null)
                    realSubject = new RealSubject();

                realSubject.Request(request);
                CacheRequest(request);
            }
        }
    }
}
