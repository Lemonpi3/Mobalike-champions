using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterStats : MonoBehaviour
{
    [Header("Charecter Stats")]
    public string team = "Neutral";
    public float healthMax = 100;
    public float healthCurrent = 100;
    public float healthRegen = 1;

    public float manaMax = 100;
    public float manaCurrent = 100;
    public float manaRegen = 5;

    public TypeOfAttack typeOfAttack;
    public float attackRange = 3; // ~3 melee , 4+ ranged
    public GameObject autoAttackproyectile;

    public float attackDamage = 5;
    public float spellPower = 0;
    public float moveSpeed = 10;
    public float attackSpeed = 1;

    [Header("Charecter Level and LevelUP amounts")]
    public int levelCurrent = 0;
    public int xpToLevel = 100;
    public int xpCurrent = 0;
    public int xpDropedOnDead = 50;

    public float healthOnLevelUp = 10;
    public float manaOnLevelUp = 10;
    public float attackDamageOnLevelUp = 1;
    public float spellPowerOnLevelUp = 10;
    
    void Start(){
        gameObject.layer = LayerMask.NameToLayer(team);
        UpdateStats();
    }

    void Update(){
        
        if (manaCurrent < manaMax)
        {
            RegenMana(manaRegen, 999, true);
        }
        if (healthCurrent < healthMax)
        {
            RegenHealth(healthRegen, 999, true);
        }
    }

    public void UpdateStats(){
        healthMax = healthMax + (healthOnLevelUp * levelCurrent);
        manaMax = manaMax + (manaOnLevelUp * levelCurrent);
        attackDamage = attackDamage + (attackDamageOnLevelUp * levelCurrent);
        spellPower = spellPower + (spellPowerOnLevelUp * levelCurrent);
    }

     public void TakeDamage(int amount, string attacker = null){
        healthCurrent -= amount;
        if(healthCurrent <= 0){
            Die();
        }
    }

    public void RegenHealth(float amount, int duration, bool isPasiveRegen = false)
    {
        if (duration > 0)
        {
            healthCurrent += amount * Time.deltaTime;
            duration -= 1;
            if (healthCurrent > healthMax)
            {
                healthCurrent = healthMax;
                if (isPasiveRegen)
                {
                    duration = 0;
                }
            }
        }
    }
    
    public void RegenMana(float amount, int duration, bool isPasiveRegen = false)
    {
        if (duration > 0)
        {
            manaCurrent += amount * Time.deltaTime;
            duration -= 1;
            if (manaCurrent > manaMax)
            {
                manaCurrent = manaMax;
                if (isPasiveRegen)
                {
                    duration = 0;
                }
            }
            if (manaCurrent < 0)
            {
                manaCurrent = 0;
            }
        }
    }

    public void Die(CharecterStats killer = null){
        if(killer != null){
            Debug.Log(name + " has being slain by " + killer.gameObject.name);
            killer.GetXp(xpDropedOnDead);
        }
        Destroy(gameObject);
    }

    public void GetXp(int xp)
    {
        print("+" + xp + " XP");
        xpCurrent += xp;
        if (xpCurrent >= xpToLevel)
        {
            int excessXp = xpCurrent - xpToLevel;
            LevelUp(excessXp);
        }
    }

    public void LevelUp(int excessXp)
    {
        print("Level Up to: " + (levelCurrent + 1));
        xpCurrent = excessXp;
        levelCurrent++;
        xpToLevel = (xpToLevel * xpToLevel) / 4;
        UpdateStats();
    }
}
public enum TypeOfAttack
{
    Melee,Range
}
