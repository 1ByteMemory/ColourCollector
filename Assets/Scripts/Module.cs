using System.Collections.Generic;
using UnityEngine;

public class Module
{
	public string moduleName = "";
	public Color color = Color.white;
	public Dictionary<string, int> statModifiers = new Dictionary<string, int>();

	public Module(string moduleName, Color color, string[] statName, int[] statModifier)
	{
		this.moduleName = moduleName;
		this.color = color;
		for (int i = 0; i < statName.Length; i++)
		{
			statModifiers.Add(statName[i], statModifier[i]);
		}
	}

	public Module(Module module)
	{
		moduleName = module.moduleName;
		color = module.color;
		statModifiers = module.statModifiers;
	}

	public Module()
	{

	}
}
