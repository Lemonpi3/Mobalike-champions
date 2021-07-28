using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public SpellUI spellUI;


    /// <summary>
    /// Loads Icons and Indicators
    /// </summary>
    public void LoadCharecterIcons(Spell[] spells){
        for(var i = 0; i < spellUI.abilityIconsFull.Length; i++){
            if(spellUI.abilityIconsFull[i]== null){
                continue;
            }
            spellUI.abilityIconsFull[i].sprite = spells[i].icon;
            spellUI.abilityIconsShaded[i].sprite = spells[i].icon;
        }
    }
}
