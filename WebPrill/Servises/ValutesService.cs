using CoreLibrary.Models;
using CoreLibrary.Services;
using Newtonsoft.Json;
using RequestsLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebPrill.Servises
{
    public class ValutesService : IValutesService
    {
        const string UserName = "Maksim";
        const string Key = "384a18ae-effb-4c31-ad80-5701337b3a6d";

        //const string UserName = "";
        //const string Key = "";

        public async Task<ResponceLocal> GetCrypts(Credentials credentials, CryptsEnum cryptsEnum = CryptsEnum.NULL)
        {
            var crypts = new List<CryptsEnum>();
            if(cryptsEnum == CryptsEnum.NULL)
            {
                crypts.Add(CryptsEnum.BTC);
                crypts.Add(CryptsEnum.ETH);
            }
            else
            {
                crypts.Add(cryptsEnum);
            }

            var request = new Request();
            request.Crypts = crypts;
            request.Credentials = new Credentials();
            request.Credentials.Client = new Client();
            request.Credentials.Client.UserName = UserName;
            request.Credentials.Client.Key = Key;

            var urlCrypts = $"{credentials.EndPoint}:{credentials.Port}/{credentials.Url}";

            var content = await RequestsService.PostAsync<ResponceLocal>(urlCrypts, JsonConvert.SerializeObject(request));

            return content;
        }

        public async Task<ResponceLocal> GetValutes(Credentials credentials, ValuteEnum valuteEnum = ValuteEnum.NULL)
        {
            var valutes = new List<ValuteEnum>();
            if (valuteEnum == ValuteEnum.NULL)
            {
                valutes.Add(ValuteEnum.USD);
                valutes.Add(ValuteEnum.EUR);
                valutes.Add(ValuteEnum.KRW);
                valutes.Add(ValuteEnum.AMD);
                valutes.Add(ValuteEnum.AUD);
            }
            else
            {
                valutes.Add(valuteEnum);
            }

            var request = new Request();
            request.Valutes = valutes;
            request.Credentials = new Credentials();
            request.Credentials.Client = new Client();
            request.Credentials.Client.UserName = UserName;
            request.Credentials.Client.Key = Key;

            var urlValutes = $"{credentials.EndPoint}:{credentials.Port}/{credentials.Url}";
            
            var content = await RequestsService.PostAsync<ResponceLocal>(urlValutes, JsonConvert.SerializeObject(request));

            return content;
        }
    }
}
