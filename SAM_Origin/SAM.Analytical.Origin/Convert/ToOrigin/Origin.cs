
using System;
using System.Threading.Tasks;
using System.Linq;
using Origin.Data.Client;
using Origin.Auth;
using Origin.Data.SAM.Analytical;
using Origin.Data;
using Origin.Data.AEC


namespace SAM.Analytical.Origin
{
    public static partial class Convert
    {
        public async static Task<bool> ToOrigin(this AnalyticalModel analyticalModel, string projectId)
        {
            string json = Core.Convert.ToString(analyticalModel);
            if(string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            // Get an instance of an Origin Client from the client factory
            var client = OriginClientProvider.Instance.GetClient(OriginScopesEnum.PROJECTS);

            if (client == null)
            {
                throw new Exception("No Origin Client found, ensure you have one set up first");
            }

            var mutate = client.Mutate;

            var connectProject = new ConnectOne<OriginEntity>();
            connectProject.connect.where.node.SetID(projectId);
           
            // Create/connect an Origin a building in project
            // Use the analytical model's name for the building name
            // Create/update levels in the building
            var buildingEntiy = new OriginEntity();
            buildingEntiy.AddOrUpdateField("Name", "SAM - " + analyticalModel.Name);
            buildingEntiy.AddOrUpdateField("Project", connectProject);

            var originAnalyticalModel = new OriginEntity();
            originAnalyticalModel.AddOrUpdateField("Name", analyticalModel.Name);
            originAnalyticalModel.AddOrUpdateField("Description", analyticalModel.Description);
            originAnalyticalModel.AddOrUpdateField("MaterialLibrary", analyticalModel.MaterialLibrary.ToJObject());
            originAnalyticalModel.AddOrUpdateField("ProfileLibrary", analyticalModel.ProfileLibrary.ToJObject());
            originAnalyticalModel.AddOrUpdateField("Location", analyticalModel.Location.ToJObject());
            originAnalyticalModel.AddOrUpdateField("AdjacencyCluster", analyticalModel.AdjacencyCluster.ToJObject());
            originAnalyticalModel.AddOrUpdateField("Project", connectProject);

            // connect building to model 
            var connectBuilding = new ConnectOneEdge<OriginEntity>();
            connectBuilding.connect.where.node.SetEntity(buildingEntiy);
            connectBuilding.connect.edge.Add("Ref", analyticalModel.Guid.ToString());

            analyticalModel.GetSpaces().ForEach(x => {
                var spaceEntity = new OriginEntity();
                spaceEntity.AddOrUpdateField("Name", x.Name);
                spaceEntity.AddOrUpdateField("Area", x.CalculatedArea(analyticalModel.AdjacencyCluster));
                spaceEntity.AddOrUpdateField("Building", connectBuilding);
            });

            // how to find levels?
         
            // Create/connect spaces in buiding
            // user ref relationship property to match the space to the SAM Space
            // Create/connect spaces to building


            // create the building in the project, will update the entity with an id so it can be connected (we're not using nested create here)
            var buildingResult = await mutate.CreateBuildings(new OriginEntity[] { buildingEntiy });
            var result = await mutate.CreateSAMAnalyticalModels(new OriginEntity[] { originAnalyticalModel });


            return result.NodesCreated > 0;

        }
    }
}