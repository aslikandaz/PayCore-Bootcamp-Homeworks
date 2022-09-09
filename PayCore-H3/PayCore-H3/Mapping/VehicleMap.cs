using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore_H3.Model;

namespace PayCore_H3.Mapping
{
    public class VehicleMap: ClassMapping<Vehicle>
    {
        // veritabanındaki vehicle tablosu ile proje içerisindeki vehicle nesnesinin eşlenmesi
        public VehicleMap()
        {
            Id(x=>x.Id, x => // id alanı tipi, kolonu ve varsayılan değerinin tanımlanması
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.VehicleName, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.VehiclePlate, x =>
            {
                x.Length(14);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Table("vehicle");
        }
    }
}
