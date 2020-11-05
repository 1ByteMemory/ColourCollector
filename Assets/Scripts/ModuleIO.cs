using System.IO;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using System;

public class ModuleIO
{
    /// <summary>
    /// Reads a multistroke gesture from an XML file
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static ModuleContainer ReadModule(string fileName)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(fileName);


        Module[] modules;
        List<string> statNames;
        List<int> modifiers;
        string[] moduleNames;

        try
        {
            
            string ModulePath = "Modules/ModuleName";
            var moduleNodes = doc.SelectNodes(ModulePath);

            modules = new Module[moduleNodes.Count];


            string statPath = "Modules/ModuleName/Stat";
            var statNodes = doc.SelectNodes(statPath);

            moduleNames = new string[moduleNodes.Count];

			for (int i = 0; i < moduleNodes.Count; i++)
			{
                moduleNames[i] = moduleNodes[i].Attributes["Name"].Value;
			    
                statNames = new List<string>();
                modifiers = new List<int>();

                foreach (XmlNode node in moduleNodes)
			    {
                    if (node.Attributes["Name"].Value == moduleNames[i])
					{

                        foreach (XmlNode item in node)
						{
                            statNames.Add(item.Attributes["stat"].Value);
                            modifiers.Add(int.Parse(item.Attributes["modifier"].Value));
						}
                        break;
					}
			    }
				ColorUtility.TryParseHtmlString(moduleNodes[i].Attributes["Color"].Value, out Color col);
				modules[i] = new Module(moduleNames[i], col, statNames.ToArray(), modifiers.ToArray());
			}
        }
        finally
        {
            if (doc != null)
                doc.RemoveAll();
        }
        return new ModuleContainer(modules);
    }

    /*
    /// <summary>
    /// Writes a multistroke gesture to an XML file
    /// </summary>
    public static void WriteGesture(PDollarGestureRecognizer.Point[] points, string gestureName, string fileName)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            sw.WriteLine("<Gesture Name = \"{0}\">", gestureName);
            int currentStroke = -1;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].StrokeID != currentStroke)
                {
                    if (i > 0)
                        sw.WriteLine("\t</Stroke>");
                    sw.WriteLine("\t<Stroke>");
                    currentStroke = points[i].StrokeID;
                }

                sw.WriteLine("\t\t<Point X = \"{0}\" Y = \"{1}\" T = \"0\" Pressure = \"0\" />",
                    points[i].X, points[i].Y
                );
            }
            sw.WriteLine("\t</Stroke>");
            sw.WriteLine("</Gesture>");
        }
    }
    */
}
