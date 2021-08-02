using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public SpellUI spellUI;
    public StatsUI statsUI;

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

    public void UpdateStats(float[] statAmounts){
        for (int i = 0; i < statAmounts.Length; i++)
        {
            statsUI.UpdateStatsUI(statAmounts[i],i);
        }
    }
}
