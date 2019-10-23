using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSkillBtn : MonoBehaviour, IPointerClickHandler
{
    public enum AssosiatedSkill
    {
        AGI,
        CON,
        STR,
    }

    public AssosiatedSkill assosiatedSkill;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (assosiatedSkill == AssosiatedSkill.AGI)
                Player.p.IncSTR();

            if (assosiatedSkill == AssosiatedSkill.STR)
                Player.p.IncSTR();

            if (assosiatedSkill == AssosiatedSkill.CON)
                Player.p.IncCON();

            Player.p.UpdateVals();
        }
    }
}
