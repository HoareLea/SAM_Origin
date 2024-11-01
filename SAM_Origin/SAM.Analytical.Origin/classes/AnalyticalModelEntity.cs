using System;
using SAM.Analytical;
using Origin.Data.Introspection;
using System.Data;
using SAM.Core;

namespace Origin.Data.MappedEntities.SAM
{
    public partial class AnalyticalModelEntity : MappedEntity<AnalyticalModel>
    {
        public AnalyticalModelEntity(AnalyticalModel element) : base(element)
        {
            Id = element.Guid.ToString();
        }

        [OriginMappedField("Name")]
        public class Name : MappedEntityField<string, AnalyticalModel>
        {
            public Name(AnalyticalModel elem) : base(elem)
            {
            }
            public Name() : base(null)
            {
            }

            public override string Value { get => _elem.Name; set => _elem.Name = value; }

            public override string GetValue(AnalyticalModel native)
            {
                return native.Name;
            }

            public override void SetValue(AnalyticalModel native, string value)
            {
                native.Name = value;
            }

        }


        [OriginMappedField("JSONdata")]
        public class JSONdata : MappedEntityField<string, AnalyticalModel>
        {
            public JSONdata(AnalyticalModel elem) : base(elem)
            {
            }

            public JSONdata() : base(null)
            {
            }

            public override string Value { get => _elem.ToJObject().ToString(); set => throw new ReadOnlyException(); }

            public override void SetValue(AnalyticalModel native, string value)
            {
                throw new ReadOnlyException();
            }

            public override string GetValue(AnalyticalModel native)
            {
                return native.ToJObject().ToString();
            }

            public string GetValue(OriginEntity remote)
            {
                remote.TryGetFieldValue(OriginFieldName, out string value);
                return value;
            }
        }

    }
}