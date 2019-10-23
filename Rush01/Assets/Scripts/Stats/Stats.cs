using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats: MonoBehaviour
{
    [HideInInspector] public int STR;
    [HideInInspector] public int AGI;
    [HideInInspector] public int CON;

    [HideInInspector] public float MAXHP;
    [HideInInspector] public float MANAMAX;
    public float HP;
    public float MANA;

    public int ARMOR;
    public int MINDMG;
    public int MAXDMG;
    public float gainedExp;
    public float expectedExp;
    public int level;
    public bool isCollectable = false;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isEquiped = false;
    [HideInInspector] public Stats weapon;

    public void GainXp(int val)
    {
        gainedExp += val;
        if (gainedExp >= expectedExp)
        {
            level++;
            if (SkillTree.skill != null)
                SkillTree.skill.Inc();
            this.HP = this.MAXHP = (5 * this.CON);
            this.MANA = this.MANAMAX = 100;
            gainedExp = 0;
            expectedExp = newRange();
        }
    }

    public int newRange()
    {
        float newx = (this.level * 1000 + Mathf.Pow((this.level - 1), 2) * 450);
        return Mathf.FloorToInt(newx);
    }

    public void IncSTR()
    {
        if (SkillTree.skill.points > 0)
        {
            SkillTree.skill.points--;
            STR++;
        }
    }

    public void IncAGI()
    {
        if (SkillTree.skill.points > 0)
        {
            SkillTree.skill.points--;
            AGI++;
        }
    }

    public void IncCON()
    {
        if (SkillTree.skill.points > 0)
        {
            SkillTree.skill.points--;
            CON++;
        }
    }

    public void UpdateVals(bool t)
    {
        this.MINDMG = this.STR / 2;
        this.MAXDMG = this.MINDMG + 4;
    }

    public void UpdateVals()
    {
		this.HP = this.MAXHP = (5 * this.CON);
        this.MINDMG = this.STR / 2;
        this.MAXDMG = this.MINDMG + 4;
    }

    public void TakeDamage(float damage)
    {
        if (HP >= 0)
            HP -= damage;
    }
}
