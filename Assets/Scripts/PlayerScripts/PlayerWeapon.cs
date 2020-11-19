using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    PlayerController player;
    Stats stats;

    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
		player = GetComponent<PlayerController>();
        stats = player.playerStats.stats;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible) return;

        if (Input.GetButtonDown("Fire1"))
		{
            weapon.damage = stats.Damage;
            weapon.travelSpeed = stats.ShotSpeed;
            weapon.firingSpeed = stats.FiringSpeed;
            weapon.projectilesPerShot = stats.Shots;
            weapon.Fire(transform.position, Vector2.up);
		}
    }
}
