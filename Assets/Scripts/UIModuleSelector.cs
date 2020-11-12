using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModuleSelector : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //moduleContainer = ModuleLoader.LoadModules();

    }

    // Update is called once per frame
    void Update()
    {
        
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

}
