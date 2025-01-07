using WebApp.Domain.Entites;
using WebApp.Application.Common.Interfaces;
using WebApp.Infrastructure.Data;

namespace WebApp.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Amenity entity)
        {
            _db.Amenities.Update(entity);
        }
    }
}