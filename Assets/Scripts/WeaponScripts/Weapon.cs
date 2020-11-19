using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public GameObject projectile;
    public float range;
    public float bulletsDensity = 1;

    [HideInInspector]
    public float travelSpeed, firingSpeed;
    [HideInInspector]
    public int damage, projectilesPerShot;

    public void Fire(Vector2 origin, Vector2 direction)
	{
        BulletSpread(new Vector2(0, 0), origin, direction);

		for (int i = 0; i < projectilesPerShot; i++)
		{
            GameObject bullet = GameObject.Instantiate(projectile, origin, new Quaternion());
            Projectile proj = bullet.GetComponent<Projectile>();

            proj.speed = travelSpeed;
            proj.range = range;

		}
	}

    /// <summary>
    /// Returns a set of points local to a transform for raycasting to
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public Vector2[] BulletSpread(Vector2 offset, Vector2 origin, Vector2 direction)
    {
        Vector2[] directions = new Vector2[projectilesPerShot];
        Vector2 half = new Vector2
        {
            x = projectilesPerShot == 1 ? 0 : projectilesPerShot * 0.5f - 0.5f,
        };

        int index = 0;
        for (int x = 0; x < projectilesPerShot; x++)
        {
            // Get point position
            Vector2 position = new Vector2(x - half.x, 0) / bulletsDensity;

            // Convert to transforms rotation
            Quaternion rotation = Quaternion.Euler(direction);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            position = m.MultiplyPoint3x4(position);

            // Apply offset
            position.x += offset.x;
            position.y += offset.y;

            // Set in front of transform at const distence
            position += origin + direction * 5;

            directions[index] = (position - origin).normalized;
            index++;
        }
        return directions;
    }
}
