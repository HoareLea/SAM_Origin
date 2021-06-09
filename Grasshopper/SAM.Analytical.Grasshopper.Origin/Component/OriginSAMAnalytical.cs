using Grasshopper.Kernel;
using SAM.Analytical.Grasshopper.Origin.Properties;
using SAM.Core;
using SAM.Core.Grasshopper;
using System;
using System.Collections.Generic;

namespace SAM.Analytical.Grasshopper.Origin
{
    public class OriginSAMAnalytical : GH_SAMVariableOutputParameterComponent
    {
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid => new Guid("ba90ef03-01e2-41b2-b941-67472dd341b7");

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
        public OriginSAMAnalytical()
          : base("Origin.SAMAnalytical", "Origin.SAMAnalytical",
              "Import SAM Analytical Object from Origin Platform",
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
                List<GH_SAMParam> result = new List<GH_SAMParam>();

                global::Grasshopper.Kernel.Parameters.Param_Boolean param_Boolean = new global::Grasshopper.Kernel.Parameters.Param_Boolean() { Name = "_run", NickName = "_run", Description = "Run", Access = GH_ParamAccess.item };
                param_Boolean.SetPersistentData(false);
                result.Add(new GH_SAMParam(param_Boolean, ParamVisibility.Binding));

                return result.ToArray();
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override GH_SAMParam[] Outputs
        {
            get
            {
                List<GH_SAMParam> result = new List<GH_SAMParam>();
                result.Add(new GH_SAMParam(new global::Grasshopper.Kernel.Parameters.Param_GenericObject() { Name = "analyticals", NickName = "analyticals", Description = "SAM Analytical Objects", Access = GH_ParamAccess.list }, ParamVisibility.Binding));
                return result.ToArray();
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
            int index = -1;


            List<IJSAMObject> jSAMObjects = Analytical.Origin.Convert.ToSAM();
            
            index = Params.IndexOfOutputParam("analyticals");
            if(index != -1)
            {
                dataAccess.SetDataList(index, jSAMObjects);
            }

        }
    }
}