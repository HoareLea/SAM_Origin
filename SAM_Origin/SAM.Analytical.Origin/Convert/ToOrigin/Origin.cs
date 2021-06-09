
namespace SAM.Analytical.Origin
{
    public static partial class Convert
    {
        public static bool ToOrigin(this AnalyticalModel analyticalModel)
        {
            string json = Core.Convert.ToString(analyticalModel);
            if(string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            //TODO: Add http request here

            return true;

        }
    }
}