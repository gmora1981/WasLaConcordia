using LaConcordia.Model;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public interface IAccountsRepository
    {
        Task<UserToken> Login(UserLogin userInfo);
        Task<UserToken> Register(UserEditDTO userInfo);
        Task<UserToken> RenewToken();
    }
}
