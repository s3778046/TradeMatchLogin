using TradeMatchLogin.DataContext;
using TradeMatchLogin.Models;
using TradeMatchLogin.Repositories.Interface;

namespace TradeMatchLogin.Repositories;

public class UserRepository : IDataRepository<User, int>
{
    private readonly TradeMatchContext _context;

    public UserRepository(TradeMatchContext context)
    {
        _context = context;
    }

    public User Get(int id)
    {
        return _context.User.Find(id);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.User.ToList();
    }

    public int Add(User user)
    {
        _context.User.Add(user);
        _context.SaveChanges();

        return user.UserID;
    }

    public int Delete(int id)
    {
        _context.User.Remove(_context.User.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, User user)
    {
        _context.Update(user);
        _context.SaveChanges();

        return id;
    }

    IEnumerable<User> IDataRepository<User, int>.GetAll()
    {
        throw new NotImplementedException();
    }

    User IDataRepository<User, int>.Get(int id)
    {
        throw new NotImplementedException();
    }


}
