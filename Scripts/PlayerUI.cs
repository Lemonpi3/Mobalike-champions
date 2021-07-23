using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public SpellUI spellUI;
    public Image[] abilityIndicators;
    public Image[] abilityRangeIndicators;

    /// <summary>
    /// Loads Icons and Indicators
    /// </summary>
    public void LoadCharecterIcons(Spell[] spells){
        for(var i = 0; i < spellUI.abilityIconsFull.Length; i++){
            spellUI.abilityIconsFull[i].sprite = spells[i].icon;
            spellUI.abilityIconsShaded[i].sprite = spells[i].icon;
        }
        for (int i = 0; i < abilityIndicators.Length; i++)
        {
            if(spells[i].spellIndicator !=null){
                abilityIndicators[i].sprite = spells[i].spellIndicator;
            }
            if(spells[i].rangeIndicator !=null){
                abilityRangeIndicators[i].sprite = spells[i].rangeIndicator;
            }else
             continue;
        }
    }

    public void ScaleIndicators(Spell[] spells)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if(abilityIndicators == null){
                Debug.LogWarning("Spell indicator not found in ability: " + i+1);
                continue;
            }

            switch (spells[i].spellType)
            {
                case SpellType.Skillshot:
                    abilityIndicators[i].GetComponentInParent<Canvas>().transform.localScale = new Vector3(spells[i].radius, 1,spells[i].range/2);
                    break;
                case SpellType.TargetCircle:
                    abilityIndicators[i].GetComponentInParent<Canvas>().transform.localScale = new Vector3(spells[i].radius, 1, spells[i].radius);
                    //abilityRangeIndicators[i].GetComponentInParent<Canvas>().transform.localScale = new Vector3(spells[i].range, 1, spells[i].range);
                    break;
                case SpellType.Self:
                    abilityIndicators[i].GetComponentInParent<Canvas>().transform.localScale = new Vector3(spells[i].radius, 1, spells[i].radius);
                    break;
                case SpellType.Cone:
                    //Scalecone
                    break;
                case SpellType.AoEonCharecter:
                    abilityIndicators[i].GetComponentInParent<Canvas>().transform.localScale = new Vector3(spells[i].radius, 1, spells[i].radius);
                    break;
                default:
                    break;
            }
        }
    }
}
