using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	[HideInInspector]
	public float speed, range;

	Vector3 startPos;
	Vector3 currPos;
	private void OnEnable()
	{
		startPos = transform.position;
	}

	private void Update()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);
		currPos = transform.position;

		if (Vector3.Distance(startPos, currPos) >= range)
		{
			Destroy(gameObject);
		}

	}
}
