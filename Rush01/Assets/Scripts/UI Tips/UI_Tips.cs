using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Tips : MonoBehaviour
{
    #region Singleton Access
    private static UI_Tips instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static UI_Tips Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UI_Tips>();
            }

            return UI_Tips.instance;
        }
    }
    #endregion

    private Image img;
    private TMP_Text text;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void ShowTooltop(string description)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        text.text = description;
    }

    public void HideTooltip()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    void Update()
    {
        if (canvasGroup.alpha == 1)
        {
            transform.position = Input.mousePosition;
        }
    }
}
