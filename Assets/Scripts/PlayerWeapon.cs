using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{

    public Text text;
    public int index;

    Module[] moduleContianer;
    
    // Start is called before the first frame update
    void Start()
    {
        moduleContianer = ModuleLoader.LoadModules();

        if (moduleContianer == null) return;
        
        foreach (Module item in moduleContianer)
        {
            text.text += item.moduleName + "\n";
        }



    }

    // Update is called once per frame
    void Update()
    {

        //text.text = contianer.ToString();
    }
}
