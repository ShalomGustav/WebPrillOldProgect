using CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RequestsLibrary;
using System.Threading.Tasks;
using WebPrill.Models;
using WebPrill.Servises;

namespace WebPrill.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IValutesService _valutesService;


        const string EndPoint = "https://localhost";
        const int Port = 44365;
        const string UrlCrypts = "rest/listcrypts";
        const string UrlValutes = "rest/valutes";
        const string UrlTranslate = "rest/translate";
        //добавить

        public HomeController(ILogger<HomeController> logger, IValutesService valutesService)
        {
            _logger = logger;
            _valutesService = valutesService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("translate")]
        public async Task<IActionResult> Translate()
        {
            return View("Translate");
        }

        [HttpGet("translate-result")]
        public async Task<IActionResult> Translate(string text)
        {
            var credentials = GenerateCredentials(nameof(Translate));
            var request = new Request()
            {
                Text = text
            };

            var urlCrypts = $"{credentials.EndPoint}:{credentials.Port}/{credentials.Url}";

            var content = await RequestsService.PostAsync<ResponceTranslateView>(urlCrypts, JsonConvert.SerializeObject(request));

            return View("Translate", content);
        }

        [HttpGet("valutes")]
        public async Task<IActionResult> GetValutes(ValuteEnum valuteEnum)
        {
            var responseValutes = await _valutesService.GetValutes(GenerateCredentials(nameof(GetValutes)), valuteEnum);
            

            if (responseValutes.Errors != null)
            {
                return View("ResponseError", responseValutes);

            }
            
            
            return View("Valutes", responseValutes);

            
        }

        [HttpGet("crypts")]
        public async Task<IActionResult> GetCrypts(CryptsEnum cryptsEnum)
        {
            var responceCrypts = await _valutesService.GetCrypts(GenerateCredentials(nameof(GetCrypts)), cryptsEnum);
            

            if (responceCrypts.Errors != null)
            {
                return View("ResponseError", responceCrypts);
            }
            


            return View("Crypts", responceCrypts);
        }

        private Credentials GenerateCredentials(string metod)
        {
            var credentials = new Credentials();
            credentials.EndPoint = EndPoint;
            credentials.Port = Port;

            if (metod == nameof(GetCrypts))
            {
                credentials.Url = UrlCrypts;
            }
            if (metod == nameof(GetValutes))
            {
                credentials.Url = UrlValutes;
            }
            if(metod == nameof(Translate))
            {
                credentials.Url = UrlTranslate;
            }

            return credentials;
        }
    }
}
