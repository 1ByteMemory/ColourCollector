using System.Reflection;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stats stats;
	public Stats startingStats;

	Stats minStats = new Stats();
	PropertyInfo[] minProperties;

	private void Start()
	{
		minProperties = minStats.GetAllVariables();
		foreach (var info in minProperties)
		{
			minStats.SetVariable(info.Name, 1);

		}
	}


	public void ApplyModifiers(Stats statsToAdd)
	{
		//stats = startingStats;

        PropertyInfo[] startingInfo = startingStats.GetAllVariables();
        PropertyInfo[] statsInfo = statsToAdd.GetAllVariables();
		minProperties = minStats.GetAllVariables();

		for (int i = 0; i < startingInfo.Length; i++)
		{

			int playerValue = (int)startingInfo[i].GetValue(startingStats);
			int valueToAdd = (int)statsInfo[i].GetValue(statsToAdd);
			int minValue = (int)minProperties[i].GetValue(minStats);


			int newValue = playerValue + valueToAdd;
			if (newValue < minValue) newValue = minValue;

			stats.SetVariable(startingInfo[i].Name, newValue);
		}
	}
}
