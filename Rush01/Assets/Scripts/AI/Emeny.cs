using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Emeny : Interactable
{
    public enum e_Type
    {
        COMMON,
        CAPTAINS,
        LEGENDARY,
        BOSS
    }
    public e_Type myType;

    public int XP_lowerLimit, XP_upperLimit;
    Player playerManager;
    AnimationManager animationManager;
    private bool XPadded = false;

    public float DIEMOFO_EndPosY = 3;
    public float DIEMOFO_SinkMultiplier = 0.25f;
    private bool isScared = false;

    void Start()
    {
    	level = Player.p.level;
		expectedExp = newRange();
		playerManager = Player.p;
        animationManager = GetComponent<AnimationManager>();

        switch(myType)
        {
            case e_Type.COMMON: // standard
                this.CON = 4;
                this.STR = 2;
                this.AGI = 2;
                break;
            case e_Type.CAPTAINS: // * 2
                this.CON = 4;
                this.STR = 5;
                this.AGI = 5;
                break;
            case e_Type.LEGENDARY: // STR 20, level base * 2; AGI = 40; CON Player.p.level * 3;
                this.level = level * 2;
                this.CON = level * 3;
                this.STR = 20;
                this.AGI = 40;
                break;
            case e_Type.BOSS: //Boss -> 5
                this.level = 30;
                this.CON = 50;
                this.STR = 20;
                this.AGI = 10;
                break;
        }

        // 30 -> 30 * 5 = 150
        for (int i = 0; i < level; i++)
        {
            for (int x = 0; x < level * 5; x++)
            {
                int chance = UnityEngine.Random.Range(0, 3);
                switch(chance)
                {
                    case 0:
                        this.STR += 1;
                    break;

                    case 1:
                        if (myType == e_Type.BOSS)
                        {
                            x--;
                            break;
                        }
                        this.CON += 1;
                    break;

                    case 2:
                        this.AGI += 1;
                    break;
                }
            }
        }
        UpdateVals();
        StartCoroutine(RunFromFear());
    }

	public override void Interact()
	{
		base.Interact();
        //Attack Enemy
        

        if (HP > 0 && !isDead)
        {
            CharacterCombat playerCombat = playerManager.GetComponent<CharacterCombat>();

            if (playerCombat != null)
            {
                if (!isDead)
                {
                    if (Player.p.isEquiped)
                    {
                        float TotalDamage = (Player.p.weapon.MAXDMG * Player.p.level) + UnityEngine.Random.Range(Player.p.MINDMG, Player.p.MAXDMG);
                        playerCombat.Attack(this, TotalDamage);
                        return ;
                    }
                    playerCombat.Attack(this);

                }
            }
            if (!inRange)
            {
                playerCombat.ResetCooldown();
            }
        }
        
        if (isDead && !XPadded)
        {
            animationManager.isDead();
            AmIDead();
        }
	}

	public bool AmIDead()
	{
        Debug.Log("IS Dead: " + name);
        XPadded = true;
        UnityEngine.Random.InitState(UnityEngine.Random.Range(1<<31, ~1 << 31));
        Player.p.GainXp(level * Mathf.FloorToInt(UnityEngine.Random.Range(XP_lowerLimit, XP_upperLimit)));
        StartCoroutine(DIEMOFO());
        return true;
	}

	IEnumerator	DIEMOFO()
	{

		float endPosY = transform.position.y - DIEMOFO_EndPosY;
        float currentposY = transform.position.y;

        yield return new WaitForSeconds(5);
   
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        while (currentposY >= endPosY)
        {
            currentposY -= Time.deltaTime * DIEMOFO_SinkMultiplier;
            transform.position = new Vector3(transform.position.x, currentposY, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        GameObject.Destroy(gameObject);
        StopAllCoroutines();
        yield return new WaitForEndOfFrame();
	}

    void OnTriggerEnter(Collider col)
    {
        if (((1 << col.gameObject.layer) & LayerMask.GetMask("Magic")) != 0)
        {
            if (HP > 0)
            {
                //Determine the spell
                if (col.gameObject.tag == "Fear")
                    isScared = true;

                Player.p.SetFocus(this);
                CharacterCombat playerCombat = playerManager.GetComponent<CharacterCombat>();
                float TotalDamage = col.gameObject.GetComponent<Stats>().MAXDMG * Player.p.level;
                playerCombat.Attack(this, TotalDamage);


            }
            
            if (HP <= 0)
            {
                isDead = true;
                animationManager.isDead();
                AmIDead();
            }
        }
    }

    IEnumerator RunFromFear()
    {
        float runTimer = 3;
        var cachedRadius = GetComponent<EnemyController>().attackRadius;

        while (true)
        {

            if (isScared == true)
            {
                runTimer -= Time.deltaTime;
                GetComponent<EnemyController>().attackRadius = 0;
                GetComponent<CharacterLocomotion>().Fear();

                if (runTimer <= 0)
                {
                    GetComponent<EnemyController>().attackRadius = cachedRadius;
                    isScared = false;
                    runTimer = 3;
                }
            }

            //GetComponentInChildren<NavMeshAgent>().destination = Player.p.transform;
            yield return new WaitForEndOfFrame();
        }
    }
}
