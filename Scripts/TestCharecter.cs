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
            indicatorsSpell[0].RotateIndicator(position);
            if(Input.GetMouseButtonDown(0)){
                Ability1();
                SpellCasted(0);
                DisableOtherIndicators(9);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            ShowSpellIndicator(1);
            if(Input.GetMouseButtonDown(1)){
                Ability2();
                SpellCasted(1);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            ShowSpellIndicator(2);
            if(Input.GetMouseButtonDown(2)){
                Ability3();
                SpellCasted(2);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            ShowSpellIndicator(3);
            if(Input.GetMouseButtonDown(3)){
                Ability4();
                SpellCasted(3);
            }
        }

        UpdateFunctions();
    }
}
