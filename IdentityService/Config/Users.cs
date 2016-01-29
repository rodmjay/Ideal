using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityService.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>(){
                new InMemoryUser(){
                    Username = "admin",
                    Password = "secret",
                    Subject = "123ABC"
                },
                new InMemoryUser(){
                    Username = "admin",
                    Password = "secret",
                    Subject = "789XYZ"
                }
            };
        } 
    }
}