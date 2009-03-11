using System;
using FluentNHibernate.Mapping;
using FluentNHibernate.Conventions;

namespace FluentNHibernate.Conventions.Defaults
{
    public class NullableEnumerationPropertyConvention : IPropertyConvention
    {
        public bool Accept(IProperty target)
        {
            var type = target.PropertyType;

            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) && type.GetGenericArguments()[0].IsEnum;
        }

        public void Apply(IProperty target)
        {
            var enumerationType = target.PropertyType.GetGenericArguments()[0];
            var mapperType = typeof(GenericEnumMapper<>).MakeGenericType(enumerationType);

            target.CustomTypeIs(mapperType);
            target.Nullable();
        }
    }
}