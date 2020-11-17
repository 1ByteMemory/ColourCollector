using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float range;

    [HideInInspector]
    public float travelSpeed, firingSpeed;
    [HideInInspector]
    public int damage, projectilesPerShot;

    public void Fire()
	{
		for (int i = 0; i < projectilesPerShot; i++)
		{
            GameObject bullet = Instantiate(projectile, transform.position, new Quaternion());
            Projectile proj = bullet.GetComponent<Projectile>();

            proj.speed = travelSpeed;
            proj.range = range;

		}
	}
}
