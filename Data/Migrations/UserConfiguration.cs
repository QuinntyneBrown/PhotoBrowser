using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PhotoBrowser.Data;
using PhotoBrowser.Features.Security;
using PhotoBrowser.Model;

namespace PhotoBrowser.Data.Migrations
{
    public class UserConfiguration
    {
        public static void Seed(PhotoBrowserContext context) {            
            var tenant = context.Tenants.Single(x => x.Name == "Default");
            
            context.Users.AddOrUpdate(x => x.Username, new User()
            {
                Username = "system",
                Password = new EncryptionService().TransformPassword("system"),
                TenantId = tenant.Id
            });

            context.SaveChanges();
        }
    }
}
