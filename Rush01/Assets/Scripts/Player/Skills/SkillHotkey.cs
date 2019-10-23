using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillHotkey : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum SkillNumber
    {
        Skill_1,//active
        Skill_2,//active
        Skill_3,//self casted active
        Skill_4,//self casted active
        Skill_5,//passive
        Skill_6,//passive
    }
    public SkillNumber skillNumber;
    public enum SpellType
    {
        Projectile,
        SelfCast,
        Passive
    }
    public int unlockLevel;
    public SpellType spellType;
    public GameObject particleMagicPrefab;
    public float coolDownTime;
    private float currentCoolDownTime;
    public KeyCode assignedKey = KeyCode.None;
    private readonly WaitForEndOfFrame wait = new WaitForEndOfFrame();
    private Image cooldownImg;
    private TMP_Text cooldownText;
    private bool castingSpell = false;
    private Button objButton;
	[TextArea]
	public string	description;
    private Image baseImage;

    public void Start()
    {
        baseImage = GetComponent<Image>();

        if (spellType == SpellType.Passive)
            return;

        if (transform.childCount > 0)
        {
            cooldownImg = transform.GetChild(0).GetComponent<Image>();
            cooldownText = transform.GetChild(1).GetComponent<TMP_Text>();
            objButton = GetComponent<Button>();
            cooldownText.enabled = false;
            StartCoroutine(Cooldown());
        }
    }

    void LateUpdate()
    {
        if (Player.p.level < unlockLevel)
        {
            if (objButton != null)
                objButton.interactable = false;
            else
                baseImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
            return;
        }
        else
        {
            if (objButton != null)
                objButton.interactable = true;
            else
                baseImage.color = new Color(1, 1, 1, 1);
        }

        if (spellType == SpellType.Passive)
        {
            //apply passive ability here!

            if (skillNumber == SkillNumber.Skill_5)
                Passive1();
            if (skillNumber == SkillNumber.Skill_6)
                Passive2();
            return;
        }

        if (Input.GetKeyDown(assignedKey) && currentCoolDownTime <= 0)
        {
            ExecuteEvents.Execute<IPointerDownHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }

        if (Input.GetKeyUp(assignedKey) && currentCoolDownTime <= 0)
        {
            ExecuteEvents.Execute<IPointerUpHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);

            if (spellType == SpellType.Projectile)
                ExecuteEvents.Execute<IPointerClickHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            else
            {
				if (skillNumber == SkillNumber.Skill_3)
					StartCoroutine(BuffPlayer());
                GameObject castOnSelf = GameObject.Instantiate(particleMagicPrefab, Player.p.transform);//Cast on player
                castOnSelf.transform.position = Player.p.transform.position;
                currentCoolDownTime = coolDownTime;
                cooldownText.enabled = true;
                GameObject.Destroy(castOnSelf, 5);
            }
        }

        if (MagicSystem.Instance.castingSpell == true && castingSpell == true)
        {
            var mousePos = MagicController.Instance.AimSpell();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Change to the attack spell
                castingSpell = MagicSystem.Instance.castingSpell = false;
                currentCoolDownTime = coolDownTime;
                cooldownText.enabled = true;
                GameObject.Destroy(GameObject.Instantiate(particleMagicPrefab, mousePos, Quaternion.identity), 5);//Cast at mouse
                MagicController.Instance.CancelSpell();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                castingSpell = MagicSystem.Instance.castingSpell = false;
                MagicController.Instance.CancelSpell();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(description))
            UI_Tips.Instance.ShowTooltop(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Tips.Instance.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (spellType == SpellType.Passive)
            return;

        castingSpell = MagicSystem.Instance.castingSpell = true;
    }

	IEnumerator BuffPlayer()
	{
		int	oSTR = Player.p.STR, oAGI = Player.p.AGI;
		Player.p.STR += 15;
		Player.p.AGI += 15;
		for (int i = 0; i < 7; i++)
		{
			Player.p.STR--;
			Player.p.AGI--;
			Player.p.UpdateVals(true);
			yield return new WaitForSeconds(1);
		}
		Player.p.STR = oSTR;
		Player.p.AGI = oAGI;
	}

    IEnumerator Cooldown()
    {
        while (true)
        {
            if (currentCoolDownTime > 0)
            {
                currentCoolDownTime -= Time.deltaTime;
                cooldownImg.fillAmount = (currentCoolDownTime / coolDownTime);
                cooldownText.text = Mathf.FloorToInt(currentCoolDownTime).ToString();

                if (currentCoolDownTime <= 0)
                    cooldownText.enabled = false;
            }

            yield return wait;
        }
    }

    public float passiveTimer = 5, currentPassiveTimer = 5;
    public void Passive1()
    {
        if (currentPassiveTimer > 0)
            currentPassiveTimer -= Time.deltaTime;

        if (currentPassiveTimer <= 0)
        {
            if (Player.p.HP < Player.p.MAXHP)
            {
                Player.p.HP++;

                if (Player.p.HP > Player.p.MAXHP)
                    Player.p.HP = Player.p.MAXHP;
            }

            currentPassiveTimer = passiveTimer;
        }
    }
    
    bool passive2State = false;
    public void Passive2()
    {
        if (currentPassiveTimer > 0)
        {
            currentPassiveTimer -= Time.deltaTime;
        }

        if (currentPassiveTimer <= 0)
        {
            if (passive2State == false)
            {
                passive2State = true;
                Player.p.AGI += 3;
                Player.p.STR += 3;
                Player.p.CON += 3;
            }
            else
            {
                passive2State = false;
                Player.p.AGI -= 3;
                Player.p.STR -= 3;
                Player.p.CON -= 3;
            }

            currentPassiveTimer = passiveTimer;
        }            
    }
}
