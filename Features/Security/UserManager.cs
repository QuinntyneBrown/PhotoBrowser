using PhotoBrowser.Model;
using System.Threading.Tasks;
using System.Security.Principal;
using PhotoBrowser.Data;
using System.Data.Entity;

namespace PhotoBrowser.Features.Security
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(IPrincipal user);
    }

    public class UserManager : IUserManager
    {
        public UserManager(IPhotoBrowserContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(IPrincipal user) => await _context
            .Users
            .Include(x=>x.Tenant)
            .SingleAsync(x => x.Username == user.Identity.Name);

        protected readonly IPhotoBrowserContext _context;
    }
}
