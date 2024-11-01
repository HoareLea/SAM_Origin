using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Origin;
using Origin.Auth;
using Origin.Auth.MSAL;
using Origin.Data.Client;
using Rhino.Runtime;
using SAM.Core;
using SAM.Core.Grasshopper;
using SAM.Core.Grasshopper.Origin.Properties;
using System;
using System.Collections.Generic;


namespace SAM.Core.Grasshopper.Origin
{
    internal class OriginClient : GH_SAMVariableOutputParameterComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("C1DBCCB7-2840-4A3C-9F0D-FA1D3607917E");

        /// <summary>
        /// The latest version of this component
        /// </summary>
        public override string LatestComponentVersion => "1.0.0";

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.SAM_Small;

        //private HashSet<ElementId> elementIds = new HashSet<ElementId>();

        /// <summary>
        /// Initializes a new instance of the SAM_point3D class.
        /// </summary>
        public OriginClient()
          : base("SAM.OriginClient", "SAM.OriginClient",
              "Connects to Origin",
              "SAM", "Origin")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override GH_SAMParam[] Inputs
        {
            get
            {
                // Register inputs for Origin Client instance
                // Optional environment name
                return new GH_SAMParam[]
                {
                    new GH_SAMParam(new Param_String() { Name = "_environment", NickName = "_environment", Description = "Environment Name", Access = GH_ParamAccess.item }, ParamVisibility.Binding)
                };
   
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override GH_SAMParam[] Outputs
        {
            get
            {
                // Register output for Origin Client instance
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="dataAccess">
        /// The DA object is used to retrieve from inputs and store in outputs.
        /// </param>
        protected override void SolveInstance(IGH_DataAccess dataAccess)
        {
            if (AuthProvider == null) createClient();

            var client = OriginClientProvider.Instance.GetClient(OriginScopesEnum.PROJECTS);

            // set the output to a client instance
            dataAccess.SetData(0, client);
        }

        static MSALAuthProvider AuthProvider;
        static void createClient()
        {
            AuthConfig authConfig = new AuthConfig();
            MSALAuthenticator msalAuthenticator = new MSALAuthenticator(authConfig);
            AuthHelper.SetAuthenticator(msalAuthenticator);
            AuthProvider = new MSALAuthProvider(new[] { OriginScopesEnum.PROJECTS }, IntPtr.Zero);
            OriginClientProvider.Instance.Register(OriginScopesEnum.PROJECTS, Env.AecAPI, AuthProvider);
        }
    }
}
