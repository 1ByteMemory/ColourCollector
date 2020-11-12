using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleSelector : MonoBehaviour
{

    public GameObject addModuleButton;
    public PlayerStats playerStats;

    [Header("UI Objects")]
    public GameObject loadedModulesViewer;
    public GameObject modulesToAddViewer;

    [Header("Module Handlers")]
    Module[] moduleList;
    Module[] loadedModules;
    public int maxModules;
    int currIndex;
    int slotsTaken;


    [Header("Stats")]
    Stats minStats = new Stats();
    Stats modifiedStats;
    PropertyInfo[] minProperties;



    // Start is called before the first frame update
    void Start()
    {
        loadedModules = ModuleLoader.LoadModules();
        moduleList = new Module[maxModules];

        minProperties = minStats.GetAllVariables();
        foreach (var info in minProperties)
        {
            minStats.SetVariable(info.Name, 1);
        }

        DisplayLoadedModules();
        UpdateViewer();
    }

    public void AddModule(Module moduleToAdd)
	{
        if (slotsTaken < moduleList.Length)
        {
			for (int i = 0; i < moduleList.Length; i++)
			{
                if (moduleList[i] == null)
				{
                    moduleList[i] = moduleToAdd;
                    break;
				}
			}
            slotsTaken++;

            UpdateViewer();
        }
    }

    public void RemoveModule(int index)
	{
        slotsTaken--;
        moduleList[index] = null;

        UpdateViewer();
	}

    void UpdateViewer()
	{
        float prevPos = -15;

        RectTransform viewerRect = modulesToAddViewer.GetComponent<RectTransform>();

		for (int i = 0; i < viewerRect.childCount; i++)
		{
            Destroy(viewerRect.GetChild(i).gameObject);
		}

        RectTransform buttonRect = addModuleButton.GetComponent<RectTransform>();
        viewerRect.sizeDelta = new Vector2(0, (buttonRect.sizeDelta.y + 12f) * moduleList.Length);

        for (int i = 0; i < moduleList.Length; i++)
        {
            GameObject button = Instantiate(addModuleButton.gameObject, modulesToAddViewer.transform);
            buttonRect = button.GetComponent<RectTransform>();

            buttonRect.anchoredPosition += new Vector2(0, prevPos + 5);
            prevPos = buttonRect.anchoredPosition.y - buttonRect.sizeDelta.y;

            Text text = button.GetComponentInChildren<Text>();
            
            if (moduleList[i] != null)
			{
                text.text = moduleList[i].moduleName;
                button.GetComponentInChildren<Image>().color = moduleList[i].color;
                AddDelegate(button.GetComponent<Button>(), i);
			}
			else
			{
                text.text = "";
                button.GetComponentInChildren<Image>().color = Color.white * 0.5f;
            }
        }
    }

    void AddDelegate(Button button, int index)
	{
        button.onClick.AddListener(() => RemoveModule(index));
	}

    public void ApplyModules()
	{
        playerStats.ApplyModifiers(ApplyModules(moduleList));
	}

    Stats ApplyModules(Module[] moduleList)
    {
        Stats newStats = new Stats();

        // Apply the modifiers for each module
        foreach (Module module in moduleList)
        {
            foreach (var modifier in module.statModifiers)
            {
                int newValue = (int)newStats.GetValue(modifier.Key);
                newValue += modifier.Value;

                newStats.SetVariable(modifier.Key, newValue);
            }
        }

        minProperties = minStats.GetAllVariables();
        PropertyInfo[] properties = newStats.GetAllVariables();

        // If any property is below the minimum stats, set to the minimum
        for (int i = 0; i < properties.Length; i++)
        {
            int value = (int)properties[i].GetValue(newStats);
            int minValue = (int)minProperties[i].GetValue(minStats);

            if (value < minValue)
            {
                newStats.SetVariable(properties[i].Name, 0);
            }
        }

        return newStats;
    }

    public void DisplayLoadedModules()
    {
        float prevPos = -15;

        RectTransform viewerRect = loadedModulesViewer.GetComponent<RectTransform>();

        RectTransform buttonRect = addModuleButton.GetComponent<RectTransform>();
        viewerRect.sizeDelta = new Vector2(0, (buttonRect.sizeDelta.y + 12f) * loadedModules.Length);


        foreach (Module item in loadedModules)
        {
            GameObject button = Instantiate(addModuleButton.gameObject, loadedModulesViewer.transform);

            buttonRect = button.GetComponent<RectTransform>();

            buttonRect.anchoredPosition += new Vector2(0, prevPos + 5);
            prevPos = buttonRect.anchoredPosition.y - buttonRect.sizeDelta.y;

            Text text = button.GetComponentInChildren<Text>();
            text.text = item.moduleName;

            button.GetComponentInChildren<Image>().color = item.color;
            AddModuleDelagate(button.GetComponent<Button>(), item);
        }
    }

    void AddModuleDelagate(Button button, Module item)
	{
        button.onClick.AddListener(() => AddModule(item));
    }
}
