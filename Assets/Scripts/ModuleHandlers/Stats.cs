using System;
using UnityEngine;
using System.Reflection;

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



	//////////////////////////////////////////////
	///                                        ///
	///   Methods to get properties or values  ///
	///			through a string               ///
	///                                        ///
	//////////////////////////////////////////////

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
		
		return info != null ? info.GetValue(this) : null;
	}

	public void SetVariable(string variable, int value)
	{
		Type type = GetType();
		PropertyInfo info = type.GetProperty(variable, BindingFlags.Public | BindingFlags.Instance);

		if (info != null) info.SetValue(this, value);
	}
}
