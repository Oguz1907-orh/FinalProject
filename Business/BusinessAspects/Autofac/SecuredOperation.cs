﻿using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extension;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{           //JWT
    public class SecuredOperation : MethodInterception
    {
        
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //her istek için httpcontext oluşturuyor.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //bir metini belirttiğimiz karaktere göre belirleyip array'e yollar.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
