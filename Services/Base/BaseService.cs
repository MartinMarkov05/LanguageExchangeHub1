using System;
using LanguageExchangeHub1.Utilities;
using AutoMapper;
using System.Security.Claims;

namespace LanguageExchangeHub1.Services.Base
{
    public abstract class BaseService
    {



        protected IServicesResourceProvider ServicesResourceProvider { get; private set; }

        protected IUserData UserData { get; private set; }


        public BaseService(IUserData userData, IServicesResourceProvider servicesResourceProvider)
           
        {
            this.UserData = userData;


            this.ServicesResourceProvider = servicesResourceProvider;


        }
    }

}

