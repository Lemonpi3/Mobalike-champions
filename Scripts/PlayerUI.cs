using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public SpellUI spellUI;

    public void LoadCharecterIcons(Spell[] spells){
        for(var i = 0; i < spellUI.abilityIconsFull.Length; i++){
            spellUI.abilityIconsFull[i].sprite = spells[i].icon;
            spellUI.abilityIconsShaded[i].sprite = spells[i].icon;
        }
    }
}
