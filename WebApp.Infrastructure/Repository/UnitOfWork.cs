using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Common.Interfaces;
using WebApp.Infrastructure.Data;

namespace WebApp.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVillaRepository Villa {get;private set;}
       public IVillaNumberRepository VillaNumber {get;private set;}
        public IAmenityRepository Amenity {get;private set;}
        public IVillaNumberRepository villaNumber => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepo(_db); 
            Amenity= new AmenityRepository(_db);
            VillaNumber= new VillaNumberRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
