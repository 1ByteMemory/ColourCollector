using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{

    Stats minStats = new Stats();
    PropertyInfo[] minProperties;

    Stats baseStats = new Stats();
    PropertyInfo[] baseProperties;

    // Start is called before the first frame update
    void Start()
    {

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
                newStats.SetVariable(properties[i].Name, minValue);
			}
		}

        return newStats;
    }

}
