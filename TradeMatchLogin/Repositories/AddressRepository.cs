using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories;

public class AddressRepository : IDataRepository<Address, int>
{
    private readonly TradeMatchContext _context;

    public AddressRepository(TradeMatchContext context)
    {
        _context = context;
    }

    public Address Get(int id)
    {
        return _context.Address.Find(id);
    }

    public IEnumerable<Address> GetAll()
    {
        return _context.Address.ToList();
    }

    public int Add(Address address)
    {
        _context.Address.Add(address);
        _context.SaveChanges();

        return address.AddressID;
    }

    public int Delete(int id)
    {
        _context.Address.Remove(_context.Address.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Address address)
    {
        _context.Update(address);
        _context.SaveChanges();

        return id;
    }

    IEnumerable<Address> IDataRepository<Address, int>.GetAll()
    {
        throw new NotImplementedException();
    }

    Address IDataRepository<Address, int>.Get(int id)
    {
        throw new NotImplementedException();
    }

}
