using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StatsController : MonoBehaviour
{

    Stats minStats = new Stats();
    PropertyInfo[] minProperties;

    // Start is called before the first frame update
    void Start()
    {
        minProperties = minStats.GetAllVariables();
		foreach (var info in minProperties)
		{
            minStats.SetVariable(info.Name, 1);
		}
    }

    public Stats ApplyModules(Module[] moduleList)
    {
        Stats newStats = new Stats();
        
        // Apply the modifiers for each module
        foreach (Module module in moduleList)
        {
            foreach (var modifier in module.statModifiers)
            {
                int newValue = (int)newStats.GetValue(modifier.Key);
                newValue += modifier.Value;

                newStats.SetVariable(modifier.Key, newValue);
            }
        }

        minProperties = minStats.GetAllVariables();
        PropertyInfo[] properties = newStats.GetAllVariables();

        // If any property is below the minimum stats, set to the minimum
        for (int i = 0; i < properties.Length; i++)
		{
			int value = (int)properties[i].GetValue(newStats);
            int minValue = (int)minProperties[i].GetValue(minStats);

            if (value < minValue)
			{
                newStats.SetVariable(properties[i].Name, 0);
			}
		}

        return newStats;
    }

}
