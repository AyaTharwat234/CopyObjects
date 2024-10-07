using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Windows.Media.Imaging;


namespace DummyObject
{
   
   
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a ribbon tab (if it doesn't exist)
            try
            {
                application.CreateRibbonTab("MyCustomTab");
            }
            catch (Exception ex)
            {
                // The tab might already exist, so you can ignore this error
            }

            // Create a ribbon panel in that tab
            RibbonPanel panel = application.CreateRibbonPanel("MyCustomTab", "MyCommands");

            // Define the button to add to the panel
            PushButtonData buttonData = new PushButtonData(
                "CopyElementAsDummy",
                "Copy Element As Dummy",
                @"C:\Path\To\Your\DLL.dll", // Path to the DLL containing the external command
                "DummyObject.CopyElementAsDummy" // Fully qualified namespace and class name of your external command
            );

            // Set an image (optional) for the button
            buttonData.LargeImage = new BitmapImage(new Uri("pack://application:,,,/YourAssemblyName;component/Resources/CopyElementAsDummy.png"));

            // Add the command to the panel
            PushButton pushButton = panel.AddItem(buttonData) as PushButton;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // Clean up any resources here if necessary
            return Result.Succeeded;
        }
    }

}
