using CoreLibrary.Models;
using System.Threading.Tasks;

namespace WebPrill.Servises

{
    public interface IValutesService
    {
        Task<ResponceLocal> GetValutes(Credentials credentials, ValuteEnum valuteEnum = ValuteEnum.NULL);
        Task<ResponceLocal> GetCrypts(Credentials credentials, CryptsEnum cryptsEnum = CryptsEnum.NULL);
    }
}
