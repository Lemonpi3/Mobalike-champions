using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
    public CharecterStats stats;
    public Slider healthBar;
    public Slider manaBar;
    public Slider UIhealthBar;
    public Slider UImanaBar;
    public Slider UIXPBar;

    public Text levelIcon;
    // Start is called before the first frame update
    void Start()
    {
       UpdateStatus();
       UpdateXP();
    }

    // Update is called once per frame
    public void UpdateStatus()
    {
        healthBar.value = stats.healthCurrent / stats.healthMax;
        manaBar.value = stats.manaCurrent / stats.manaMax;
        UImanaBar.value = manaBar.value;
        UIhealthBar.value = healthBar.value;
    }

    public void UpdateXP(){
        levelIcon.text = (stats.levelCurrent + 1).ToString();
        UIXPBar.value = (float)stats.xpCurrent / stats.xpToLevel;
        Debug.Log(UIXPBar.value = stats.xpCurrent / stats.xpToLevel);
    }
}
