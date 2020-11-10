using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject addStatsPage;
    public GameObject modifierPage;
    public GameObject modifierList;
    public GameObject[] addedModifires = new GameObject[4];

    public Button addStatButton;
    public float addStatButtonDistance = 50;
    public Sprite panelSprite;
    public Text text;
    public Canvas main;

    [Header("Base Stats")]
    Stats playerStats = new Stats();
    PropertyInfo[] minStats;

    Module[] loadedModules;
    public RectTransform UIModuleHolder;


    // Start is called before the first frame update
    void Start()
    {
        minStats = playerStats.GetMinVariables();
        loadedModules = ModuleLoader.LoadModules();

		foreach (PropertyInfo stat in minStats)
		{
            playerStats.SetVariable(stat.Name, 1);

            text.text += stat.Name + ": " + playerStats.GetValue(stat.Name) + "\n";

        }


        InisiatePanel();
    }


    public void InisiatePanel()
	{
        float posYOffset = -30;
        float prevPos = 0;
        foreach (Module item in loadedModules)
		{
            GameObject button = Instantiate(addStatButton.gameObject, UIModuleHolder.transform);
            RectTransform buttonRect = button.GetComponent<RectTransform>();

            buttonRect.anchoredPosition = new Vector2(0, prevPos + posYOffset);
            prevPos = buttonRect.anchoredPosition.y;

            Text text = button.GetComponentInChildren<Text>();
            text.text = item.moduleName;
            
            button.GetComponentInChildren<Image>().color = item.color;

            button.GetComponent<Button>().onClick.AddListener(delegate { AddModule(item); });
        }
    }


    public void AddModule(Module module)
	{
        
	}

}
