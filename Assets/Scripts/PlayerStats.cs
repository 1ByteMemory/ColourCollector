using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stats playerStats;

    public void ApplyModifiers(Stats statsToChange)
	{
        PropertyInfo[] playerInfo = playerStats.GetAllVariables();
        PropertyInfo[] statsInfo = new Stats().GetAllVariables();

		for (int i = 0; i < playerInfo.Length; i++)
		{
            int playerValue = (int)playerInfo[i].GetValue(playerStats);
			int valueToAdd = (int)statsInfo[i].GetValue(statsToChange);

            playerStats.SetVariable(playerInfo[i].Name, playerValue + valueToAdd);
		}
	}
}
