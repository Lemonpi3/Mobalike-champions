using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public SpellUI spellUI;
    public StatsUI statsUI;

    
    public Slider manabarUI;
    public Slider healthBarUI;
    public Slider xpBarUI;

    public GameObject onCharStatusBars;
    public Transform onCharUIparent;

    StatusBars statusBars;

    private void Start() {
        GameObject statusBarGO = Instantiate<GameObject>(onCharStatusBars,onCharUIparent.position,Quaternion.identity,onCharUIparent);
        statusBars = statusBarGO.GetComponent<StatusBars>();
        statusBars.stats = GetComponent<CharecterStats>();
        statusBars.UIhealthBar = healthBarUI;
        statusBars.UImanaBar = manabarUI;
        statusBars.UIXPBar = xpBarUI;
    }

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
            UpdateSpellsDescriptions(spells);
        }
    }

    public void UpdateStats(float[] statAmounts){
        for (int i = 0; i < statAmounts.Length; i++)
        {
            statsUI.UpdateStatsUI(statAmounts[i],i);
        }
    }

    public void UpdateStatusBars(){
        statusBars.UpdateStatus();
    }

    public void UpdateXPUI(){
        statusBars.UpdateXP();
    }

    public void UpdateSpellsDescriptions(Spell[] spells){

        CharecterStats stats = GetComponent<CharecterStats>();
        for (int i = 0; i < spells.Length; i++)
        {
            spellUI.description[i].text = spells[i].GetDescription(stats);
        }
    }
}
