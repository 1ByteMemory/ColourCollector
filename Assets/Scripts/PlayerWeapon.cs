using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject addStatsPage;
    public Button addStatButton;
    public float addStatButtonDistance = 50;
    public Sprite panelSprite;
    public Text text;
    public Canvas main;

    [Header("Base Stats")]
    Stats playerStats = new Stats();
    PropertyInfo[] minStats;

    // Start is called before the first frame update
    void Start()
    {
        minStats = playerStats.GetMinVariables();

		foreach (PropertyInfo stat in minStats)
		{
            playerStats.SetVariable(stat.Name, 1);

            text.text += stat.Name + ": " + playerStats.GetValue(stat.Name) + "\n";

        }


        InisiatePanel();
    }


    public void InisiatePanel()
	{
        GameObject panel = Instantiate(new GameObject(), addStatsPage.transform);

        RectTransform rect = panel.AddComponent<RectTransform>();
        panel.AddComponent<CanvasRenderer>();
        Image img = panel.AddComponent<Image>();

        rect.sizeDelta = new Vector2(0, 0);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.anchoredPosition = new Vector2(0, -40);

        img.sprite = panelSprite;
        img.type = Image.Type.Sliced;
        img.fillCenter = true;
        img.pixelsPerUnitMultiplier = 1;

        float spacing = -10;

		foreach (var item in minStats)
		{
            GameObject button = Instantiate(addStatButton.gameObject, panel.transform);
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            
            buttonRect.sizeDelta = new Vector2(-10, 5);
            buttonRect.anchorMin = new Vector2(0, 0);
            buttonRect.anchorMax = new Vector2(1, 1);
            buttonRect.anchoredPosition = new Vector2(0, -10);
            buttonRect.localPosition = new Vector3(0, spacing);
            spacing -= addStatButtonDistance;

		}



	}


}
