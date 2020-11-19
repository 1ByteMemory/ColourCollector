using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

	public Color heal = Color.green;
	public Color hert = Color.red;

	public GameObject damageNumbers;
	DamageNumbers dmgNums;

	public void TakeDamage(int dmg)
	{
		health -= dmg;

		GameObject nums = Instantiate(damageNumbers, transform.position, new Quaternion());
		dmgNums = nums.GetComponentInChildren<DamageNumbers>();
		if (dmgNums != null)
		{
			dmgNums.dispalyText = dmg.ToString();
			dmgNums.color = hert;
		}
	}
}
