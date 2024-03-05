using System;
using System.Net;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;


using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities;
using Microsoft.AspNetCore.Identity;

namespace LanguageExchangeHub1.Services.Models
{
    public class UserService : BaseService, IUserService
    {

        private readonly IEfRepository<User> userRepository;
        private readonly UserManager<User> userManager;


        public UserService(IMapper mapper,

                       IEfRepository<User> userRepository
                       ,
                       UserManager<User> userManager,
                       IUserData userData
                      )

        : base(mapper, userData)
        {

            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task<OperationResponse> CreateAsync(UserRegistrationModel model, string role)
        {
            if (model == null)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Моделът не е валиден!" };
            }



            var users = this.userRepository.All().Where(u => u.UserName == model.Username && u.Email == model.Email).ToList();

            if (users.Any())
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Потребителят съществува!" };
            }

            var modelForCreate = this.Mapper.Map<User>(model);
            var response = await this.userManager.CreateAsync(modelForCreate, model.Password);

            if (!response.Succeeded)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Неуспешно създаване на потребителя!" };
            }
            await userManager.AddToRoleAsync(modelForCreate, role);
            return new OperationResponse { IsSuccessful = true };
        }

        public async Task<UserViewModel> EditProfile(UserViewModel viewModel)
        {
            var user = userRepository.GetById(UserData.UserId);
            user.UserName = viewModel.Username;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            var newViewModel = Mapper.Map<UserViewModel>(user);
            return newViewModel;
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var response = this.userRepository.All();
            return this.Mapper.Map<List<UserViewModel>>(response);
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userID)        {

                var userEntity = userRepository.All().FirstOrDefault(u => u.Id == userID);

                if (userEntity == null)
                {
                    // User not found, handle accordingly (throw an exception, return null, etc.)
                    throw new Exception("User not found");
                }

            var userViewModel = await UserMapper.MapToViewModel(userEntity);

                return userViewModel;
            
}

        public async Task<UserViewModel> GetUserByNameAsync(string userName)
        {

            var userEntity = userRepository.All().FirstOrDefault(u => u.UserName == userName);

            if (userEntity == null)
            {
                // User not found, handle accordingly (throw an exception, return null, etc.)
                throw new Exception("User not found");
            }

            // Map the user entity to UserViewModel
            var userViewModel = Mapper.Map<UserViewModel>(userEntity);

            return userViewModel;
        }


    }

    }






