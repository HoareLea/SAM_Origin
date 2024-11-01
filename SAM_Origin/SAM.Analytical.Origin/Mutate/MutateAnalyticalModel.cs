using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Origin.Data;

namespace Origin.Data.SAM.Analytical
{
    public static class MutateDistributionExtensions
    {
        /// <summary>
        /// Creates a SAM Analytical Model
        /// </summary>
        /// <param name="om"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static async Task<MutationResult> CreateSAMAnalyticalModels(this IOriginMutate om, OriginEntity[] entities)
        {
            return await om.Create(entities, "createSAMAnalyticalModels", "sAMAnalyticalModelInput", "sAMAnalyticalModels");
        }
    }
}