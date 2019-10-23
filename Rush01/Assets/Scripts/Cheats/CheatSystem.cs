using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatSystem : MonoBehaviour
{
    #region Singleton Access
    private static CheatSystem instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static CheatSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CheatSystem>();
            }

            return CheatSystem.instance;
        }
    }
    #endregion

    private bool godmode = false;
    private TMP_InputField inputField;
    public TMP_Text message;
    public bool showCheatPanel = false;
    private CanvasGroup canvasGroup;
    private bool isOpen = false;

    private void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();    
    }

    public void EnteredCommand(TMP_InputField inputField)
    {
        if (message.text.Length > 0)
            message.text += '\n';

        if (inputField.text == "iamgod")
        {
            godmode = !godmode;

            if (godmode == true)
                message.text += "You are the god!";
            else if (godmode == false)
                message.text += "You are no longer the god of this world mortal!";
        }
        else
        {
            message.text += "Unknown command";
        }

        if (godmode == false)
            Player.p.UpdateVals();

        inputField.text = "";
    }

    void Update()
    {
        if (godmode == true)
        {
            Player.p.MAXHP = Player.p.HP = Mathf.Infinity;
			Player.p.MINDMG = Player.p.MAXDMG = ~(1 << 31);
			SkillTree.skill.points += 200;
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isOpen = !isOpen;
            UIController.Instance.guiIsOpen = isOpen;
        }

        canvasGroup.alpha = (isOpen == true) ? 1 : 0;
        canvasGroup.blocksRaycasts = isOpen;
        canvasGroup.interactable = isOpen;
    }
}
