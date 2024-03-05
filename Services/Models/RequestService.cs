using System;
using AutoMapper;
using LanguageExchangeHub1.Data.Models;
using LanguageExchangeHub1.Repository;
using LanguageExchangeHub1.Services.Base;
using LanguageExchangeHub1.Services.Contracts;
using LanguageExchangeHub1.Services.Models;
using LanguageExchangeHub1.Services.Models.Courses;
using LanguageExchangeHub1.Services.Models.Users;
using LanguageExchangeHub1.Utilities.RequestStatus;

namespace LanguageExchangeHub1.Services
{
	public class RequestService : BaseService, IRequestService
	{
        private readonly IEfRepository<Request> requestRepository;
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public RequestService(IMapper mapper, IUserData userData
            , IEfRepository<Request> requestRepository,
            ICourseService courseService,
            IUserService userService) : base(mapper, userData)
		{
			this.requestRepository = requestRepository;
            this.courseService = courseService;
            this.userService = userService;
		}

        public async Task<RequestViewModel> CreateAsync(string courseId)
        {
            Request request = new Request
            {
               // Course = Mapper.Map<Course>(await courseService.GetAsync(courseId)),
                CourseId = courseId,
               // User = Mapper.Map<User>( userService.GetUserByIdAsync(UserData.UserId)),
                UserId = UserData.UserId,
                RequestStatus = RequestStatus.None
            };

            requestRepository.Add(request);
            await requestRepository.SaveChangesAsync();

            return Mapper.Map<RequestViewModel>(request);
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

