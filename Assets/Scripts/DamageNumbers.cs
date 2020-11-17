using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbers : MonoBehaviour
{
    public float duration;
    public string dispalyText;
    public Color color;

	Text text;

	float startTime;
	
	private void OnEnable()
	{
		text = GetComponent<Text>();
		text.color = color;
		text.text = dispalyText;

		startTime = Time.time;
	}


	// Update is called once per frame
	void Update()
    {
		transform.Translate(Vector3.up * Time.deltaTime);

		float alpha = (Time.time - startTime) / duration;
		Color col = text.color;

		col.a = alpha;

		text.color = col;

    }
}
