﻿using DomainModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IUserSession
    {
        UserDto CurrentUser { get; set; }
        bool IsAuthenticated { get; set; }
        void ClearSession();
        void AuthenticateUser(users user);
    }
}