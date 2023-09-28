
namespace TradeMatchLogin.Repositories.Interface
{
    public interface IGenericRepository<Entity, Key> where Entity : class
    {

        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> FindAsync(Key key);
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> UpdateAsync(Key key, Entity entity);
        Task<Entity> DeleteAsync(Key key);
    }
}
