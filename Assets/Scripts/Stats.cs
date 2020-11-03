using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Stats
{
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


	public static object GetValue(object stats, string variable)
	{
		Type type = stats.GetType();
		PropertyInfo info = type.GetProperty(variable, BindingFlags.Public | BindingFlags.Instance);
		
		return info.GetValue(stats);
	}

	public static void SetVariable(object stats, string variable, int value)
	{
		Type type = stats.GetType();
		PropertyInfo info = type.GetProperty(variable, BindingFlags.Public | BindingFlags.Instance);

		info.SetValue(stats, value);

	}

}


public class Module
{
	public string name;
	public Dictionary<string, int> Modifiers;

	


}