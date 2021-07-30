using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharecter : CharecterBase
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1)){
            ShowSpellIndicator(0);
            indicatorsSpell[0].RotateIndicator();
            if(Input.GetMouseButtonDown(0)){
                Ability1();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            ShowSpellIndicator(1);
            if(Input.GetMouseButtonDown(1)){
                Ability2();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            ShowSpellIndicator(2);
            if(Input.GetMouseButtonDown(2)){
                Ability3();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            ShowSpellIndicator(3);
            if(Input.GetMouseButtonDown(3)){
                Ability4();
            }
        }

        UpdateFunctions();
    }

    public override void Ability1()
    {
        base.Ability1();
        Instantiate<GameObject>(spells[0].proyectile,spellSpawnpoint.position,Quaternion.identity,particlesParent);
    }
}
