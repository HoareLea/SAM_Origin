using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.Origin
{
    public static partial class Convert
    {
        public static List<IJSAMObject> ToSAM()
        {
            string json = null;

            //TODO: Add http request here

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return Core.Convert.ToSAM(json);

        }
    }
}