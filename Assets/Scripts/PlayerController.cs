using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStats playerStats;

    public int base_damage;
    public int base_shotSpeed;
    public int base_firingSpeed;
    public int base_shots;
    public int base_health;
    public int base_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        playerStats.stats.Damage = base_damage;
        playerStats.stats.ShotSpeed = base_shotSpeed;
        playerStats.stats.FiringSpeed = base_firingSpeed;
        playerStats.stats.Shots = base_shots;
        playerStats.stats.Health = base_health;
        playerStats.stats.MoveSpeed = base_moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursor.visible)
		{
            MovePlayer();
		}
    }

    void MovePlayer()
	{
        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        transform.Translate(translation, Space.World);
	}

}
