using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories
{ 

    public class UserRepository : AGenericRepository<User, Guid>, IUserRepository

    {
        public UserRepository(TradeMatchContext context) : base(context) { }


        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<User> AddAsync(User entity)
        {
            return await base.AddAsync(entity);
        }

        public override async Task<User> DeleteAsync(Guid key)
        {
            return await base.DeleteAsync(key);
        }

        public override async Task<User> FindAsync(Guid key)
        {
            return await base.FindAsync(key);
        }

        public override async Task<User> UpdateAsync(Guid key, User entity)
        {
            return await base.UpdateAsync(key, entity);
        }
    }
}
