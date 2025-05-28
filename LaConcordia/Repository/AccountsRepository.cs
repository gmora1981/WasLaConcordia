using LaConcordia.Helpers;
using LaConcordia.Model;
using static LaConcordia.Repository.AccountsRepository;
using System.Threading.Tasks;
using System;


namespace LaConcordia.Repository
{
   
        public class AccountsRepository : IAccountsRepository
        {
            private readonly IHttpService httpService;
            private readonly string baseURL = "api";

            public AccountsRepository(IHttpService httpService)
            {
                this.httpService = httpService;
            }


            public async Task<UserToken> Register(UserEditDTO userLogin)
            {
                var httpResponse = await httpService.Post<UserEditDTO, UserToken>($"{baseURL}/Accounts/create", userLogin);

                if (!httpResponse.Success)
                {
                    throw new ApplicationException(await httpResponse.GetBody());
                }

                return httpResponse.Response;
            }

            public async Task<UserToken> Login(UserLogin userLogin)
            {
                var httpResponse = await httpService.Post<UserLogin, UserToken>($"{baseURL}/Accounts/Login", userLogin);

                if (!httpResponse.Success)
                {
                    throw new ApplicationException(await httpResponse.GetBody());
                }

                return httpResponse.Response;
            }

            public async Task<UserToken> RenewToken()
            {
                var response = await httpService.Get<UserToken>($"{baseURL}/Accounts/RenewToken");

                if (!response.Success)
                {
                    throw new ApplicationException(await response.GetBody());
                }

                return response.Response;
            }
        }
}
