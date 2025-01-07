using WebApp.Domain.Entites;
using WebApp.Application.Common.Interfaces;
using WebApp.Infrastructure.Data;

namespace WebApp.Infrastructure.Repository
{
    public class VillaRepo : Repository<Villa>,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepo(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        /*public void Add(Villa entity)
        { 
            _db.Add(entity);
        }

        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>();
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(property);
                }
            }
            return query.ToList();
        }

        public void Remove(Villa entity)
        {
            _db.Remove(entity);
        }*/

        

        public void Update(Villa entity)
        {
            _db.Villas.Update(entity);
        }
    }
}