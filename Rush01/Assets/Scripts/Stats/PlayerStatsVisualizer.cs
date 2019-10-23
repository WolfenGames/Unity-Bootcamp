using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsVisualizer : MonoBehaviour
{
	public GameObject	AGI, STR, CON, DMG;

    // Update is called once per frame
    void Update()
    {
		AGI.GetComponent<TextMeshProUGUI>().text = string.Format("[AGI]::	<color=#2E2EFE>[{0}]</color>", Player.p.AGI); // ft_strjoin(s1, s1 ....); free(...); 
        STR.GetComponent<TextMeshProUGUI>().text = string.Format("[STR]::	<color=#FF0040>[{0}]</color>", Player.p.STR);
        CON.GetComponent<TextMeshProUGUI>().text = string.Format("[CON]::	<color=#F7FE2E>[{0}]</color>", Player.p.CON);
		DMG.GetComponent<TextMeshProUGUI>().text = string.Format("[DMG]::	<color=#FE642E>[{0}</color>/<color=#FF0000>{1}]</color>", Player.p.MINDMG, Player.p.MAXDMG);
    }
}
