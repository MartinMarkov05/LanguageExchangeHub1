
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Users;

namespace LanguageExchangeHub1.Services.Contracts
{
	public interface IUserService
	{
        Task<List<UserViewModel>> GetAllAsync();

        Task<UserViewModel> GetUserByIdAsync(string userId);

        Task<OperationResponse> CreateAsync(UserRegistrationModel model,string role);

        Task<UserViewModel> GetUserByNameAsync(string userName);
        Task<UserViewModel> EditProfile(UserViewModel viewModel);
    }
}

