using NHibernate;
using PayCore_H3.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_H3.Context
{
    public class MapperSession : IMapperSession
    {
        //Vehicle ve Container nesneleri için tanımlanan metotlar ımplement ediliyor
        private readonly ISession session;
        private ITransaction transaction;
        public MapperSession(ISession session)
        {
            this.session = session;
        }


        public IQueryable<Vehicle> Vehicles =>session.Query<Vehicle>();
        public IQueryable<Container> Containers => session.Query<Container>();


        public void BeginTransaction()
        {
            transaction = session.BeginTransaction();
        }

        public void CloseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Delete(Vehicle entity)
        {
            session.Delete(entity);
        }

        public void Delete(Container entity)
        {
            session.Delete(entity);
        }

       

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Save(Vehicle entity)
        {
            session.Save(entity);
        }

        public void Save(Container entity)
        {
            session.Save(entity);
        }

        public void Update(Vehicle entity)
        {
            session.Update(entity);
        }

        public void Update(Container entity)
        {
            session.Update(entity);
        }
    }

}
