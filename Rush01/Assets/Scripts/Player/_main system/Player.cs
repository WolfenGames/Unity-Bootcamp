using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Stats
{
	Camera 	                cam;
	GameObject 				target;
	Interactable			focusedInteractable;
	GameObject				Equiped;

	public static Player	p;
	
    private CharacterLocomotion movement;
	[HideInInspector] public AnimationManager animationManager;
	
    RaycastHit	hit;

	public LayerMask mask;
	[HideInInspector] public Transform equipSlot;

	void Awake()
	{
		if (p == null)
			p = this;
	}

	void Start()
	{
		movement = GetComponent<CharacterLocomotion>();
		animationManager = GetComponent<AnimationManager>();
		cam = Camera.main;
		equipSlot = animationManager.animator.GetBoneTransform(HumanBodyBones.RightHand).transform.GetChild(0);
		
		SetUpMyStats();
	}

	void SetUpMyStats()
	{
		//HP = 100;
		level = 1;
		expectedExp = newRange();
		gainedExp = 1;

		//Player Base Stats
		STR = 5;
		CON = 5;
		AGI = 10;

		if (SkillTree.skill != null)
			SkillTree.skill.points = 0;
		
		for (int i = 0; i < level * 5; i++)
        {
            int chance = UnityEngine.Random.Range(0, 3);
            switch(chance)
            {
                case 0:
                    this.STR += 1;
                break;

                case 1:
                    this.CON += 1;
                break;

                case 2:
                    this.AGI += 1;
                break;
            }
        }
		UpdateVals();
	}
	void Update()
	{
		if (HP > 0)
		{
			if (UIController.Instance.guiIsOpen || MagicSystem.Instance.castingSpell)
				return ;

			if (equipSlot.childCount > 0)
			{
				var temp = equipSlot.GetChild(0).GetComponent<CollectableItems>();
				isEquiped = true;
				weapon = temp;
			}
			else
			{
				isEquiped = false;
				weapon = null;
			}

			if (Input.GetMouseButton(0))
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100, mask))
				{
					if (focusedInteractable != null)
					{
						RemoveFocus();
					}

					movement.StopFollowingTarget();
					movement.MoveToPoint(hit.point);
				}
			}
			
			if (Input.GetMouseButton(1))
			{
                if (focusedInteractable != null)
                    RemoveFocus();

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100))
				{
					Interactable target = hit.collider.gameObject.GetComponent<Interactable>();

					if (target != null)
					{
						SetFocus(target);
					}
				}
			}
		}
		else
		{
            isDead = true;
			animationManager.isDead();
		}

	}
	public void SetFocus(Interactable newFocus)
	{
		if (newFocus != focusedInteractable)
		{
			if (focusedInteractable != null)
				focusedInteractable.Defocus();
			newFocus.Focus(transform);
			focusedInteractable = newFocus;
			movement.FollowTarget(focusedInteractable);
		}
		focusedInteractable = newFocus;
	}
	public void RemoveFocus()
	{
        if (focusedInteractable != null)
    		focusedInteractable.Defocus();
		focusedInteractable = null;
		movement.StopFollowingTarget();
	}

	public void Heal()
	{
		StartCoroutine(Healing());
	}

	IEnumerator Healing()
	{
		int fuck = Mathf.FloorToInt((30f/100f) * this.MAXHP);

		for (int i = 0; i < fuck; i++)
		{
			this.HP++;
			this.HP = Mathf.Clamp(this.HP, 0, this.MAXHP);
			yield return new WaitForSeconds(0.2f);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (((1 << col.gameObject.layer) & LayerMask.GetMask("Teleporter")) != 0)
		{
			Teleporter temp = col.gameObject.GetComponentInParent<Teleporter>();

			movement.agent.Warp(temp.TeleporterOut.position);
			movement.agent.ResetPath();
		}
	}
}
