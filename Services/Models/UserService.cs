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
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services.Models
{
    public class UserService : BaseService, IUserService
    {

        private readonly IEfRepository<User> userRepository;
        private readonly UserManager<User> userManager;


        public UserService(

                     IServicesResourceProvider servicesResourceProvider
                       ,
                       UserManager<User> userManager,
                       IUserData userData
                      )

        : base(userData,servicesResourceProvider)
        {

            this.userManager = userManager;
            this.userRepository = ServicesResourceProvider.GetEfRepositoryOfType<User>();
        }

        public async Task<OperationResponse> CreateAsync(UserRegistrationModel model)
        {
            if (model == null)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Моделът не е валиден!" };
            }

            var users = userRepository.All().Where(u => u.UserName == model.Username && u.Email == model.Email).ToList();

            if (users.Any())
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Потребителят съществува!" };
            }

            var modelForCreate = model.MapUserRegistrationModelToUserEntity();
            var response = await userManager.CreateAsync(modelForCreate, model.Password);

            if (!response.Succeeded)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Неуспешно създаване на потребителя!" };
            }

            await userManager.AddToRoleAsync(modelForCreate, model.Role);

            return new OperationResponse { IsSuccessful = true };
        }

        public async Task<UserViewModel> EditProfile(UserViewModel viewModel)
        {
            var user = userRepository.GetById(UserData.UserId);
            user.UserName = viewModel.Username;
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            var newViewModel = user.MapUserEntityToUserViewModel();
            return newViewModel;
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            return await userRepository.All()
                .Select(u => u.MapUserEntityToUserViewModel())
                .ToListAsync();
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userID)
        {

            var userEntity = await userRepository.All().FirstOrDefaultAsync(u => u.Id == userID)
                ?? throw new Exception("User not found");

            return userEntity.MapUserEntityToUserViewModel();
        }

        public async Task<UserViewModel> GetUserByNameAsync(string userName)
        {
            var userEntity = await userRepository.All().FirstOrDefaultAsync(u => u.UserName == userName)
                ?? throw new Exception("User not found");

            return userEntity.MapUserEntityToUserViewModel();
        }


    }

    }






