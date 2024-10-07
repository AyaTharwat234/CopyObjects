# Dummy Object Revit Plugin

This Revit plugin allows users to select any 3D element from the model and create a "dummy" object that replicates the original element’s geometry. The plugin supports copying complex geometries, such as solids, meshes, and instances.
## ## Sample

![CopyObjects]([https://github.com/AyaTharwat234/CopyObjects/blob/master/copyobj.png])




## Features

- **Select and Duplicate Geometry**: Select any Revit element (walls, floors, doors, etc.) and duplicate its geometry into a new "dummy" object.
- **Supports Complex Geometry**: Handles Revit elements with nested geometry instances, including solids, meshes, and geometry from families.
- **Integrated with Revit UI**: The plugin adds a custom ribbon to Revit’s UI with a button to launch the command.
  
## Requirements

- **Revit Version**: The plugin is tested with Revit 2021 and later.
- **.NET Framework**: Requires .NET Framework 4.8.
- **Revit API**: Ensure that `RevitAPI.dll` and `RevitAPIUI.dll` are referenced in the project.

## Installation

1. Clone or download the repository:
   ```bash
   git clone https://github.com/your-username/dummy-object-revit-plugin.git
