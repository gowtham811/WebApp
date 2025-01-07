using WebApp.Domain.Entites;
using WebApp.Application.Common.Interfaces;
using WebApp.Infrastructure.Data;

namespace WebApp.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(VillaNumber entity)
        {
            _db.VillaNumbers.Update(entity);
        }
    }
}