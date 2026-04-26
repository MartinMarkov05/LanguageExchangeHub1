using System;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Base;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities.RequestStatus;
using Microsoft.EntityFrameworkCore;

namespace LanguageExchangeHub1.Services
{
	public class RequestService : BaseService, IRequestService
	{
        private readonly IEfRepository<Request> requestRepository;

      

        public RequestService( IUserData userData
            , IServicesResourceProvider servicesResourceProvider) : base( userData, servicesResourceProvider)
		{
			this.requestRepository = ServicesResourceProvider.GetEfRepositoryOfType<Request>();   
		}

        public async Task<OperationResponse> CreateAsync(int courseId)
        {
            if (  await requestRepository.All().AnyAsync(r => r.CourseId == courseId && r.UserId == UserData.UserId))
            {

                return new OperationResponse { ErrorMessage = "You are already in this course", IsSuccessful = false };
            }
          
            Request request = new();
            request.CourseId = courseId;
            request.UserId = UserData.UserId;
            
            this.requestRepository.Add(request);
            await this.requestRepository.SaveChangesAsync();

            return new OperationResponse { IsSuccessful = true };
        }

        public Task<List<Request>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetAsync(string requestId)
        {
            throw new NotImplementedException();
        }
    }
}

