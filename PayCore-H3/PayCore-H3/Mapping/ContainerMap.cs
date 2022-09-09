using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore_H3.Model;

namespace PayCore_H3.Mapping
{
    public class ContainerMap: ClassMapping<Container>
    {
        // veritabanındaki container tablosu ile proje içerisindeki container nesnesinin eşlenmesi
        public ContainerMap()
        {

            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.ContainerName, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.Latitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.NotNullable(true);
            });
            Property(b => b.Longitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.NotNullable(true);
            });

            Property(x => x.VehicleId, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("vehicleid");
                
            });

            Table("container");
        }
    }
}
