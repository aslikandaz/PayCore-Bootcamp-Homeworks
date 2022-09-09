using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayCore_H3.Model;

namespace PayCore_H3.Context
{
    public interface IMapperSession
    {
        //Vehicle ve Container nesneleri için gerekli metotlar tanımlanıyor
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save(Vehicle entity);
        void Update(Vehicle entity);
        void Delete(Vehicle entity);
        void Save(Container entity);
        void Update(Container entity);
        void Delete(Container entity);
        IQueryable<Vehicle> Vehicles { get; }
        IQueryable<Container> Containers { get; }
        
    }
}
