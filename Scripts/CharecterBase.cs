using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharecterBase : MonoBehaviour
{
    public string characterName = "DefaultCharecter";
    public Transform UI;

    public CharecterStats charecterStats;
    public PlayerUI playerUI;
    //Spell stuff
    public Spell[] spells;
    int selectedSpell;

    [HideInInspector] public Image[] iconShaded = new Image[4];
    [HideInInspector] public Image[] icon = new Image[4];
    [HideInInspector] public float[] coolDown = new float[4];
    [HideInInspector] public float[] maxRange = new float[4];
    [HideInInspector] public bool[] isCoolDown = new bool[4];
    [HideInInspector] public Ray ray;
    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public Vector3 position;
    [HideInInspector] public Image[] spellIndicator = new Image[4];
    [HideInInspector] public Image[] spellRangeIndicator = new Image[4];

    void Start()
    {
        gameObject.name = characterName;
        LoadAbilities();
        playerUI.ScaleIndicators(spells);
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        position = TrackMousePos();
        CoolDownTrack();

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Debug.Log(spellIndicator[0].enabled);
            ShowSpellIndicator(0);
            if(Input.GetMouseButtonDown(0)){
                Ability1();
                SpellCasted(0);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            ShowSpellIndicator(1);
            if(Input.GetMouseButtonDown(1)){
                Ability2();
                SpellCasted(1);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            ShowSpellIndicator(2);
            if(Input.GetMouseButtonDown(2)){
                Ability3();
                SpellCasted(2);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            ShowSpellIndicator(3);
            if(Input.GetMouseButtonDown(3)){
                Ability4();
                SpellCasted(3);
            }
        }
        
        RotateSkillshotIndicator(selectedSpell);
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
            if (i != index && spellIndicator[i] != null)
            {
                spellIndicator[i].enabled = false;

                if(spellRangeIndicator[i] != null){
                    spellRangeIndicator[i].enabled = false;
                }
            }
        }
    }

    void DisableIndicator(int index)
    {
        spellIndicator[index].enabled = false;
        spellRangeIndicator[index].enabled = false;
    }

    void LoadAbilities()
    {
        playerUI.LoadCharecterIcons(spells);

        for (int i = 0; i < spells.Length; i++)
        {
            Debug.Log("loaded" + i);
            coolDown[i] = spells[i].coolDown;
            maxRange[i] = spells[i].range;
            spellIndicator[i] = playerUI.abilityIndicators[i];
            spellRangeIndicator[i] = playerUI.abilityRangeIndicators[i];
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
        else return position;
    }

    public void ShowSpellIndicator(int abilityIndex){
        if (isCoolDown[abilityIndex] == false && charecterStats.manaCurrent >= spells[abilityIndex].manaCost)
        {
            selectedSpell = abilityIndex;
            spellIndicator[abilityIndex].enabled = true;
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

    public void RotateSkillshotIndicator(int abilityIndex){
        
        if(spellIndicator[abilityIndex].enabled==true){
            Quaternion transRot = Quaternion.LookRotation(position - transform.position + new Vector3(0.0001f,0.0001f,0.0001f));
            transRot.eulerAngles = new Vector3(0, 0, transRot.eulerAngles.z);
            spellIndicator[abilityIndex].rectTransform.rotation = Quaternion.Lerp(transRot, spellIndicator[abilityIndex].rectTransform.rotation, 0f);
        }
    }
}
