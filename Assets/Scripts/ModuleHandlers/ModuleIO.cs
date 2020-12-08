using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ModuleIO
{
    /// <summary>
    /// Reads a multistroke gesture from an XML file
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>A list of modules</returns>
    public static ModuleContainer ReadModule(string fileName)
    {
        XmlDocument doc = new XmlDocument();
        
        // Loads the file as an xmlfile
        doc.LoadXml(fileName);


        Module[] modules;
        List<string> statNames;
        List<int> modifiers;
        string[] moduleNames;

        // Attempts to read the file
        try
        {
            // Access all the Modules
            string ModulePath = "Modules/ModuleName";
            var moduleNodes = doc.SelectNodes(ModulePath);

            modules = new Module[moduleNodes.Count];

            // Access the stats in ethe modules
            string statPath = "Modules/ModuleName/Stat";
            var statNodes = doc.SelectNodes(statPath);

            moduleNames = new string[moduleNodes.Count];

            // Iterate through each module
			for (int i = 0; i < moduleNodes.Count; i++)
			{
                moduleNames[i] = moduleNodes[i].Attributes["Name"].Value;
			    
                statNames = new List<string>();
                modifiers = new List<int>();

                // Iterate through each node in the module
                foreach (XmlNode node in moduleNodes)
			    {
                    // Make sure we access the correct module and its contents
                    if (node.Attributes["Name"].Value == moduleNames[i])
					{
                        // add its contents to a list
                        foreach (XmlNode item in node)
						{
                            statNames.Add(item.Attributes["stat"].Value);
                            modifiers.Add(int.Parse(item.Attributes["modifier"].Value));
						}
                        break;
					}
			    }
                // Convert the color from a hex string to a Color struct
                ColorUtility.TryParseHtmlString(moduleNodes[i].Attributes["Color"].Value, out Color col);
                
                // Add a new module with all the info from the current selected node
				modules[i] = new Module(moduleNames[i], col, statNames.ToArray(), modifiers.ToArray());
			}
        }
        finally
        {
            if (doc != null)
                doc.RemoveAll();
        }
        // return the list of modules
        return new ModuleContainer(modules);
    }
}
