using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    PlayerController player;
    Stats stats;
    Vector2 aim;

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

        aim = new Vector2(Input.GetAxisRaw("AimX"), Input.GetAxisRaw("AimY"));

        if (aim.x != 0 || aim.y != 0)
		{
            weapon.damage = stats.Damage;
            weapon.travelSpeed = stats.ShotSpeed;
            weapon.firingSpeed = stats.FiringSpeed;
            weapon.projectilesPerShot = stats.Shots;
            weapon.Fire(transform.position, aim);
		}
    }
}
