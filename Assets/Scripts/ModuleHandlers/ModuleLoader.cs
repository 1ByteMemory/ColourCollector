using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLoader
{

    /// <summary>
    /// Loads training gesture samples from XML files
    /// </summary>
    /// <returns></returns>
    public static Module[] LoadModules()
    {
        List<ModuleContainer> container = new List<ModuleContainer>();
        List<Module> modules = new List<Module>();

        // Find all text assets in the resources folder
        Object[] folder = Resources.LoadAll("", typeof(TextAsset));

		for (int i = 0; i < folder.Length; i++)
		{
            if (folder[i].GetType() == typeof(TextAsset))
            {
                // get the binaries of the file
                TextAsset binary = (TextAsset)Resources.Load(folder[i].name);

				try
				{
                    // Attempt to add a list of modules
                    container.Add(ModuleIO.ReadModule(binary.text));
				}
                // Not all files in the resources folder can be loaded
				catch (System.NullReferenceException)
				{
                    Debug.LogWarning("Failed to load: " + folder[i].name);
					continue;
				}
				catch (System.Exception)
				{
                    Debug.LogWarning("Failed to load: " + folder[i].name);
                    continue;
				}
            }
        }

        // unpack the list of modules to return it.
		foreach (ModuleContainer item in container)
		{
			foreach (Module mod in item.modules)
			{
                modules.Add(mod);
			}
		}

        return modules.ToArray();
    }
}
