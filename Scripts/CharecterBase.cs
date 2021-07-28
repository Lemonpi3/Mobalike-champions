using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharecterBase : MonoBehaviour
{
    [Header("Charecter name, stats , spells")]

    public string characterName = "DefaultCharecter";
    public CharecterStats charecterStats;
    public Spell[] spells;

    [Header("Set up things")]
    public PlayerUI playerUI;
    public Transform UI;
    public Transform indicatorsParent;
    public Transform spellSpawnpoint;
    public Transform particlesParent;

    [HideInInspector] public Indicator[] indicatorsSpell = new Indicator[4];
    [HideInInspector] public Image[] iconShaded = new Image[4];
    [HideInInspector] public Image[] icon = new Image[4];
    [HideInInspector] public float[] coolDown = new float[4];
    [HideInInspector] public float[] maxRange = new float[4];
    [HideInInspector] public bool[] isCoolDown = new bool[4];
    [HideInInspector] public Ray ray;
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public Vector3 position;
    
    void Start()
    {
        LoadAbilities();
        gameObject.name = characterName;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public virtual void Ability1()
    {
        Debug.Log("Default charecter Ability 1");
    }

    public virtual void Ability2()
    { 
        Debug.Log("Default charecter Ability 2");
    }

    public virtual void Ability3()
    {
        Debug.Log("Default charecter Ability 3");
    }

    public virtual void Ability4()
    {
        Debug.Log("Default charecter Ability 4");
    }

    /// <summary>
    /// Can be placed anywhere as need it
    /// </summary>
    public virtual void PassiveAbility()
    {
        Debug.Log("Default charecter PassiveAbility");
    }

    void CoolDownTrack()
    {
        if (isCoolDown[0])
        {
            icon[0].fillAmount -= 1 / coolDown[0] * Time.deltaTime;
            DisableIndicator(0);

            if (icon[0].fillAmount <= 0)
            {
                icon[0].fillAmount = 0;
                isCoolDown[0] = false;
            }
        }
        if (isCoolDown[1])
        {
            icon[1].fillAmount -= 1 / coolDown[1] * Time.deltaTime;
            DisableIndicator(1);

            if (icon[1].fillAmount <= 0)
            {
                icon[1].fillAmount = 0;
                isCoolDown[1] = false;
            }
        }
        if (isCoolDown[2])
        {
            icon[2].fillAmount -= 1 / coolDown[2] * Time.deltaTime;
            DisableIndicator(2);

            if (icon[2].fillAmount <= 0)
            {
                icon[2].fillAmount = 0;
                isCoolDown[2] = false;
            }
        }
        if (isCoolDown[3])
        {
            icon[3].fillAmount -= 1 / coolDown[3] * Time.deltaTime;
            DisableIndicator(3);

            if (icon[3].fillAmount <= 0)
            {
                icon[3].fillAmount = 0;
                isCoolDown[3] = false;
            }
        }
    }

    public void DisableOtherIndicators(int index)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (i != index && indicatorsSpell[i] != null)
            {
                indicatorsSpell[i].gameObject.SetActive(false);
            }
        }
    }

    void DisableIndicator(int index)
    {
        indicatorsSpell[index].gameObject.SetActive(false);
    }

    void LoadAbilities()
    {
        playerUI.LoadCharecterIcons(spells);
        
        for (int i = 0; i < spells.Length; i++)
        {
            indicatorsSpell[i] = spells[i].LoadIndicator(indicatorsParent);
            coolDown[i] = spells[i].coolDown;
            icon[i] = playerUI.spellUI.abilityIconsFull[i];
            iconShaded[i] = playerUI.spellUI.abilityIconsShaded[i];
        }
        Debug.Log("Loaded Abilities!");
    }

    public Vector3 TrackMousePos(){
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 pos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            return pos;
        }
        else return Vector3.zero;
    }

    public void ShowSpellIndicator(int abilityIndex){
        if (isCoolDown[abilityIndex] == false && charecterStats.manaCurrent >= spells[abilityIndex].manaCost)
        {
            indicatorsSpell[abilityIndex].gameObject.SetActive(true);
            DisableOtherIndicators(abilityIndex);
        }
    }
    
    /// <summary>
    /// Disables active Indicator , discounts mana cost and puts spell on cooldown
    /// </summary>
    public void SpellCasted(int abilityIndex){
        charecterStats.manaCurrent -= spells[abilityIndex].manaCost;
        isCoolDown[abilityIndex] = true;
        icon[abilityIndex].fillAmount = 1;
        DisableIndicator(abilityIndex);
        Debug.Log("casted"+ spells[abilityIndex].name);
    }

    /// <summary>
    /// Base functions that have to be on Update function of the charecter
    /// </summary>
    public void UpdateFunctions(){
        if(Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1) || Input.GetKeyUp(KeyCode.Alpha1)||Input.GetKeyUp(KeyCode.Alpha2)||Input.GetKeyUp(KeyCode.Alpha3)||Input.GetKeyUp(KeyCode.Alpha4)){
            DisableOtherIndicators(9);
        }
        CoolDownTrack();
    }
}
