using UnityEngine;

public class SpellTooltip : MonoBehaviour
{   
    public void ShowTooltip(){
        gameObject.SetActive(true);
    } 

    public void HideTooltip(){
        gameObject.SetActive(false);
    }
}
