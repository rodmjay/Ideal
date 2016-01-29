using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityService.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>(){
                new Scope(){
                    Name="gallerymanagement",
                    DisplayName="Gallery Management",
                    Description="Allow the application to manage galleries on your behalf",
                    Type=ScopeType.Resource
                }
            };
        }
    }
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>();
        }
    }
}