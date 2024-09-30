﻿using System;

namespace MVC002.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName  { get; set; }
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();    //if we n eed to create role
        }

    }
}
