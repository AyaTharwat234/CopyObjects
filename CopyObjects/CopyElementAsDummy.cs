
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using System.Collections.Generic;
using System.Xml.Linq;


namespace DummyObject
{
    [Transaction(TransactionMode.Manual)]
    public class CopyElementAsDummy : IExternalCommand
    {
                public Result Execute(
              ExternalCommandData commandData,
              ref string message,
              ElementSet elements)
                {
            // Get the current Revit document
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            // Prompt user to select an element in the model
            Selection sel = uiDoc.Selection;
            Reference pickedRef = sel.PickObject(ObjectType.Element, "Select an element to copy as a dummy");
            Element selectedElement = doc.GetElement(pickedRef);

            // Retrieve the geometry of the selected element
            Options geomOptions = new Options
            {
                ComputeReferences = true,
                IncludeNonVisibleObjects = true
            };
            GeometryElement geomElement = selectedElement.get_Geometry(geomOptions);

            if (geomElement == null)
            {
                TaskDialog.Show("Error", "Could not retrieve geometry from the selected element.");
                return Result.Failed;
            }

            // Create a transaction to perform the dummy creation
            using (Transaction trans = new Transaction(doc, "Create Dummy Object"))
            {
                trans.Start();

                // Create a DirectShape to copy the geometry into a new element
                DirectShape dummyShape = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));

                // Collect all geometry from the selected element
                List<GeometryObject> geometryObjects = new List<GeometryObject>();
                foreach (GeometryObject geomObj in geomElement)
                {
                    if (geomObj is GeometryInstance instance)
                    {
                        // Retrieve geometry from instances
                        GeometryElement instanceGeometry = instance.GetInstanceGeometry();
                        foreach (GeometryObject innerGeom in instanceGeometry)
                        {
                            if (innerGeom is Solid || innerGeom is Mesh)
                            {
                                geometryObjects.Add(innerGeom);
                            }
                        }
                    }
                    else if (geomObj is Solid || geomObj is Mesh)
                    {
                        geometryObjects.Add(geomObj);
                    }
                }

                // Set the shape if we have valid geometry
                if (geometryObjects.Count > 0)
                {
                    dummyShape.SetShape(geometryObjects);
                }
                else
                {
                    TaskDialog.Show("Error", "No valid geometry found to create a dummy.");
                    trans.RollBack();
                    return Result.Failed;
                }

                trans.Commit();
            }

            return Result.Succeeded;
        }

  
    }

}
