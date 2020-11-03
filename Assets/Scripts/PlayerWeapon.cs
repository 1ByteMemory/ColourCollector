using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Stats stats;

    string _name = "Damage";
    //public Stats[] ColorModifiers;


    // Start is called before the first frame update
    void Start()
    {
        Stats.SetVariable(stats, _name, 10);
        Debug.Log(Stats.GetValue(stats, _name).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
