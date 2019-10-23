using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	#region Singleton Access
    private static UIController instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIController>();
            }

            return UIController.instance;
        }
    }
    #endregion

	public bool 			guiIsOpen = false;
	public GameObject	    Stats;
	public GameObject	    Skills;
	public TextMeshProUGUI	HP, MANA;//, MAXHP, MAXMANA;
	string					hp_cache, mana_cache;
	public Slider		    s_HP, s_MANA;
	private bool 			isOpen = false;
	private CanvasGroup 	canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
		hp_cache = HP.text;
		mana_cache = MANA.text;
		canvasGroup = transform.GetChild(2).GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
		s_HP.value = Player.p.HP / Player.p.MAXHP;
		s_MANA.value = Player.p.MANA / Player.p.MANAMAX;

		HP.text = string.Format(hp_cache, Player.p.HP, Player.p.MAXHP);
		MANA.text = string.Format(mana_cache, Player.p.MANA, Player.p.MANAMAX);

        if (Input.GetKeyDown(KeyCode.Tab))
			Stats.SetActive(!Stats.activeSelf);

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen == true)
        {
            BringUpUIController();
            return;
        }

		if (Input.GetKeyDown(KeyCode.N))
		{
			if(UIController.Instance.guiIsOpen == false && isOpen == false || isOpen == true)
				BringUpUIController();
    	}
	}

	public void BringUpUIController()
    {
        bool newState = !canvasGroup.interactable;
        canvasGroup.interactable = newState;
        canvasGroup.blocksRaycasts = newState;
        canvasGroup.alpha = (newState == true) ? 1 : 0;
        UIController.Instance.guiIsOpen = newState;
		Skills.SetActive(!Skills.activeSelf);
        isOpen = !isOpen;
    }
}
