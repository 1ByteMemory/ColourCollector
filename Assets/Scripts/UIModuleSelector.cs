using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UIModuleSelector : MonoBehaviour
{

    public GameObject addModuleButton;
    public PlayerStats playerStats;
    public Text displayText;
    public Text statsText;

    [Header("UI Objects")]
    public GameObject loadedModulesViewer;
    public GameObject modulesToAddViewer;

    [Header("Module Handlers")]
    Module[] moduleList;
    Module[] loadedModules;
    public int maxModules;
    int slotsTaken;


    [Header("Stats")]


    PlayerStats player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        displayText.text = "";
        loadedModules = ModuleLoader.LoadModules();
        moduleList = new Module[maxModules];

        DisplayModifiedStats();

        DisplayLoadedModules();
        UpdateViewer();
    }

    public void AddModule(Module moduleToAdd)
	{
        if (slotsTaken < moduleList.Length)
        {
			for (int i = 0; i < moduleList.Length; i++)
			{
                if (moduleList[i] == null || moduleList[i].moduleName == "NULL")
				{
                    moduleList[i] = moduleToAdd;
                    break;
				}
			}
            slotsTaken++;

            UpdateViewer();
            DisplayModifiers();
        }
    }

    public void RemoveModule(int index)
	{
        slotsTaken--;
        moduleList[index] = null;

        UpdateViewer();
        DisplayModifiers();
    }
    void DisplayModifiers()
	{
        Dictionary<string, int> displayModifiers = new Dictionary<string, int>();

        foreach (Module module in moduleList)
        {
            if (module != null)
            {
                foreach (var key in module.statModifiers)
                {
                    if (displayModifiers.ContainsKey(key.Key))
                    {
                        displayModifiers[key.Key] += key.Value;
                    }
                    else
                    {
                        displayModifiers.Add(key.Key, key.Value);
                    }
                }
            }
        }

        // Display Modifications
        displayText.text = "";
        foreach (var item in displayModifiers)
        {
            if (item.Key != "NULL")
            {
                string positive = item.Value < 0 ? "" : "+";
                displayText.text += string.Format("{0}{1} {2}\n", positive, item.Value, item.Key);
            }
        }
    }

    void UpdateViewer()
	{

        // ------------------------------------ //
        // Display the module button
        // ------------------------------------ //

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

            if (moduleList[i] != null && moduleList[i].moduleName != "NULL")
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

        DisplayModifiedStats();
	}

    void DisplayModifiedStats()
	{
        statsText.text = "";
        foreach (var item in player.stats.GetAllVariables())
        {
            int value = (int)item.GetValue(player.stats);
            string positive = value < 0 ? "" : "+";
            statsText.text += string.Format("{0}{1} {2}\n", positive, value, item.Name);
        }
    }

    Stats ApplyModules(Module[] moduleList)
    {
        Stats newStats = new Stats();

        // Apply the modifiers for each module
        for (int i = 0; i < moduleList.Length; i++)
        {
            if (moduleList[i] == null)
            {
				moduleList[i] = new Module();
                moduleList[i].moduleName = "NULL";
                moduleList[i].statModifiers.Add("NULL", 0);
            }

            foreach (var modifier in moduleList[i].statModifiers)
            {
				try
				{
                    int newValue = (int)newStats.GetValue(modifier.Key);
                    newValue += modifier.Value;

                    newStats.SetVariable(modifier.Key, newValue);
				}
				catch (System.NullReferenceException)
				{
                    if (modifier.Key == "NULL") continue;

                    Debug.LogError("\"" + modifier.Key + "\" Variable does not exist.");
					throw;
				}
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
