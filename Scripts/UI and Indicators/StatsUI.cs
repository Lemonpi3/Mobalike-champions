using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text[] statText;

    public void UpdateStatsUI(float statAmount,int statIndex){
        statText[statIndex].text = statAmount.ToString();
    }
}
