using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa {  get; }
        IVillaNumberRepository villaNumber { get; }
        IAmenityRepository Amenity { get; }
        void Save();

    }
}
