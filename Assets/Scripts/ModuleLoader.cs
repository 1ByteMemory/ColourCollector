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

        Object[] folder = Resources.LoadAll("", typeof(TextAsset));

		for (int i = 0; i < folder.Length; i++)
		{
            if (folder[i].GetType() == typeof(TextAsset))
            {
                TextAsset binary = (TextAsset)Resources.Load(folder[i].name);

				try
				{
                    container.Add(ModuleIO.ReadModule(binary.text));
				}
				catch (System.NullReferenceException)
				{
                    Debug.LogWarning("Failed to load: " + folder[i].name);
					break;
				}
            }
        }

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
