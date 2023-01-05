using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepository;
using NZWalks.API.Repositories.Repository;

namespace NZWalks.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly NZWalksDbContext _db;

        //private List<User> Users = new List<User>()
        //{
        //new User()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = "Read Only",
        //        LastName = "User",
        //        Email = "readonly@user.com",
        //        Username = "readonly@user.com",
        //        Password = "Readonly@user",
        //        Roles = new List<string> { "reader" }
        //    },

        //new User()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = "Read Write",
        //        LastName = "User",
        //        Email = "readwrite@user.com",
        //        Username = "readwrite@user.com",
        //        Password = "Readwrite@user",
        //        Roles = new List<string> { "reader", "writer" }
        //    }
        //};

        public UserRepository(NZWalksDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Username.Equals(username)
            && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await _db.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                foreach (var userRole in userRoles)
                {
                    user.Roles = new List<string>();
                    var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);

                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}