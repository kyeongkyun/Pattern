﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
    }
}
