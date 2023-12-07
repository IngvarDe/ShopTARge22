using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto.AccountsDtos;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IAccountsServices
    {
        Task<ApplicationUser> Register(ApplicationUserDto dto);
    }
}
