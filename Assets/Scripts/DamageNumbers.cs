using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    public float duration;
    [HideInInspector]
	public string dispalyText;
    [HideInInspector]
    public Color color;

	TextMeshProUGUI text;

	float startTime;
	
	private void OnEnable()
	{
		text = GetComponentInChildren<TextMeshProUGUI>();
		startTime = Time.time;
	}


	// Update is called once per frame
	void Update()
    {
		transform.Translate(Vector3.up * Time.deltaTime);

		float alpha = (Time.time - startTime) / duration;
		Color col = color;
		
		col.a = 1 - alpha;

		text.color = col;
		text.text = dispalyText;

		if (alpha >= 1f) Destroy(gameObject);
    }
}
