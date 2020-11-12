using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleSelector : MonoBehaviour
{
    public GameObject addModuleButton;
    public PlayerStats playerStats;

    Stats stats;
    Module[] moduleList;
    Module[] loadedModules;
    StatsController statsController = new StatsController();
    public int maxModules;
    int currIndex;
    int slotsTaken;

    // Start is called before the first frame update
    void Start()
    {
        loadedModules = ModuleLoader.LoadModules();
        moduleList = new Module[maxModules];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddModule(Module moduleToAdd)
	{
        if (slotsTaken < moduleList.Length)
		{
            moduleList[currIndex] = moduleToAdd;

            slotsTaken++;
		}
	}

    public void RemoveModule(int index)
	{
        slotsTaken--;
        moduleList[index] = null;

	}

    public void SetCurrentIndex(int index)
	{
        currIndex = index;
	}

    public void ApplyModules()
	{
        playerStats.ApplyModifiers(statsController.ApplyModules(moduleList));
	}



    public void InisiatePanel()
    {
        float posYOffset = -30;
        float prevPos = 0;
        foreach (Module item in loadedModules)
        {
            GameObject button = Instantiate(addModuleButton.gameObject, transform);
            RectTransform buttonRect = button.GetComponent<RectTransform>();

            buttonRect.anchoredPosition = new Vector2(0, prevPos + posYOffset);
            prevPos = buttonRect.anchoredPosition.y;

            Text text = button.GetComponentInChildren<Text>();
            text.text = item.moduleName;

            button.GetComponentInChildren<Image>().color = item.color;

            button.GetComponent<Button>().onClick.AddListener(delegate { AddModule(item); });
        }
    }

}
