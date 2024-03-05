using System;
using LanguageExchangeHub1.Utilities;
using AutoMapper;
using System.Security.Claims;

namespace LanguageExchangeHub1.Services.Base
{
    public abstract class BaseService
    {
        

        protected readonly IMapper Mapper;
        protected readonly IUserData UserData;
        

        public BaseService(IMapper mapper,IUserData userData)
           
        {
            this.UserData = userData;
            this.Mapper = mapper;
            
           


        }
    }

}

