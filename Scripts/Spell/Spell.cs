using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells/New Spell", order = 0)]
public class Spell : ScriptableObject {

    //Spell Stats
    public string spellName = "New Spell";
    public SpellType spellType;

    public float coolDown = 4;
    public int spellRankCurrent = 0;
    public int spellRankMax = 4;

    public float manaCost = 1;

    public float range = 10;
    public float radius = 1;

    public float damageBase = 0;
    public float attackScaling = 1;
    public float spellPowerScaling = 1;
    public float[] damageRank;

    
    
    //Icons and proyectile
    public Sprite icon;
    public GameObject indicator;    
    public GameObject proyectile;

    public float CalculateFinalDamage(CharecterStats casterStats){
        return(damageBase + damageRank[spellRankCurrent] + casterStats.attackDamage * attackScaling + casterStats.spellPower * spellPowerScaling);
    }

    public Indicator LoadIndicator(Transform indicatorParentTransform){
        GameObject loadedIndicator = Instantiate(indicator,indicatorParentTransform.position,Quaternion.identity,indicatorParentTransform);
        loadedIndicator.GetComponent<Indicator>().ScaleIndicator(spellType,range,radius);
        return loadedIndicator.GetComponent<Indicator>();
    }
}

public enum SpellType{
    Skillshot,TargetCircle,AoEonCharecter,Cone,Self,Other
}