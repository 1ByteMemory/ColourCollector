using System.Reflection;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stats stats;

    public void ApplyModifiers(Stats statsToChange)
	{
        PropertyInfo[] playerInfo = stats.GetAllVariables();
        PropertyInfo[] statsInfo = new Stats().GetAllVariables();

		for (int i = 0; i < playerInfo.Length; i++)
		{
            int playerValue = (int)playerInfo[i].GetValue(stats);
			int valueToAdd = (int)statsInfo[i].GetValue(statsToChange);

            stats.SetVariable(playerInfo[i].Name, playerValue + valueToAdd);
		}
	}
}
