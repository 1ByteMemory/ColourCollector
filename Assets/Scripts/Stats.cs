using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Stats
{
	#region Stats

	private int damage;
    private int shotSpeed;
    private int firingSpeed;
    private int shots;
    private int health;
    private int moveSpeed;
	public int Damage
	{
		get { return damage; }
		set { damage = value; }
	}
	public int ShotSpeed
	{
		get { return shotSpeed; }
		set { shotSpeed = value; }
	}
	public int FiringSpeed
	{
		get { return firingSpeed; }
		set { firingSpeed = value; }
	}
	public int Shots
	{
		get { return shots; }
		set { shots = value; }
	}
	public int Health
	{
		get { return health; }
		set { health = value; }
	}
	public int MoveSpeed
	{
		get { return moveSpeed; }
		set { moveSpeed = value; }
	}

	#endregion

	#region Minimum stats

	private int minDamage;
	private int minShotSpeed;
	private int minFiringSpeed;
	private int minShots;
	private int minHealth;
	private int minMoveSpeed;

	public int MinDamage
	{
		get { return minDamage; }
		set { minDamage = value; }
	}
	public int MinShotSpeed
	{
		get { return minShotSpeed; }
		set { minShotSpeed = value; }
	}
	public int MinFiringSpeed
	{
		get { return minFiringSpeed; }
		set { minFiringSpeed = value; }
	}
	public int MinShots
	{
		get { return minShots; }
		set { minShots = value; }
	}
	public int MinHealth
	{
		get { return minHealth; }
		set { minHealth = value; }
	}
	public int MinMoveSpeed
	{
		get { return minMoveSpeed; }
		set { minMoveSpeed = value; }
	}
	
	#endregion



	public PropertyInfo[] GetMinVariables()
	{
		Type type = GetType();
		PropertyInfo[] info = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		PropertyInfo[] minVars = new PropertyInfo[info.Length / 2];
		
		for (int i = 0; i < minVars.Length; i++)
		{
			// 6 is where the min variables start
			minVars[i] = info[i + 6];
		}

		return minVars;
	}
	public PropertyInfo[] GetStatVariables()
	{
		System.Type type = GetType();
		PropertyInfo[] info = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		PropertyInfo[] minVars = new PropertyInfo[info.Length / 2];

		// 6 is where the min variables start
		for (int i = 0; i < minVars.Length; i++)
		{
			minVars[i] = info[i];
		}
		return minVars;
	}
	public PropertyInfo[] GetAllVariables()
	{
		Type type = GetType();
		PropertyInfo[] info = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		
		return info;
	}


	public object GetValue(string variable)
	{
		Type type = GetType();
		PropertyInfo info = type.GetProperty(variable, BindingFlags.Public | BindingFlags.Instance);
		
		return info.GetValue(this);
	}

	public void SetVariable(string variable, int value)
	{
		Type type = GetType();
		PropertyInfo info = type.GetProperty(variable, BindingFlags.Public | BindingFlags.Instance);

		info.SetValue(this, value);
	}
}

public class ModuleContainer
{
	public Module[] modules;

	public ModuleContainer(Module[] modules)
	{
		this.modules = modules;
	}
}


public class Module
{
	public string moduleName = "";
	public Color color = Color.white;
	public Dictionary<string, int> statModifiers = new Dictionary<string, int>();

	public Module (string moduleName, Color color, string[] statName, int[] statModifier)
	{
		this.moduleName = moduleName;
		this.color = color;
		for (int i = 0; i < statName.Length; i++)
		{
			statModifiers.Add(statName[i], statModifier[i]);
		}
	}

	public Module (Module module)
	{
		moduleName = module.moduleName;
		color = module.color;
		statModifiers = module.statModifiers;
	}

	public Module()
	{

	}
}