﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public string confirmEmail { get; set; }



    }
}