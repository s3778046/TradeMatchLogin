using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories
{

    public class AddressRepository : AGenericRepository<Address, Guid>, IAddressRepository

{
        public AddressRepository(TradeMatchContext context) : base(context) { }


        public override async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Address> AddAsync(Address entity)
        {
            return await base.AddAsync(entity);
        }

        public override async Task<Address> DeleteAsync(Guid key)
        {
            return await base.DeleteAsync(key);
        }

        public override async Task<Address> FindAsync(Guid key)
        {
            return await base.FindAsync(key);
        }

        public override async Task<Address> UpdateAsync(Guid key, Address entity)
        {
            return await base.UpdateAsync(key, entity);
        }
    }
}
