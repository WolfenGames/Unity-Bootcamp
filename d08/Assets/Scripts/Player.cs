using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	GameObject target;
	public static Player p;
	public float 		distance;
	public float 		health;
	public bool			attacking;
	public bool			beingAttacked;
	private Animator	animator;

	public float		gainedExp;
	public float		expectedExp;
	public int			level;

	public NavMeshAgent naveMeshAgent;
	RaycastHit	hit;

	public void GainXp()
	{
		if (gainedExp >= expectedExp)
		{
			level++;
			gainedExp = 0;
			expectedExp = newRange();
		}
	}

	int	newRange()
	{
		float	newx = (this.level * 1000  + Mathf.Pow((this.level - 1), 2) * 450);
		return Mathf.FloorToInt(newx);
	}

	void OnAwake()
	{
		p = this;
	}

	void Start()
	{
		naveMeshAgent = this.GetComponent<NavMeshAgent>();
		animator = this.GetComponent<Animator>();
		health = 100;
		level = 0;
		expectedExp = newRange();
		gainedExp = 1;
	}

	void UpdateAnimator()
	{
		animator.SetBool("Attacking", attacking);
		animator.SetFloat("Health", health);
		animator.SetFloat("Distance", (attacking) ? 0 : distance);
	}

	void SetDestination()
	{
		if (Input.GetMouseButton(1))
		{
			naveMeshAgent.SetDestination(hit.point);
		}
	}

	void Update()
	{
		if (health > 0)
		{
			sendRay();
			SetDestination();
			distance = naveMeshAgent.remainingDistance;
			Attack();
			if (target)
			{
				attacking = target;
				target.GetComponent<Emeny>().Attack(1 * Time.deltaTime);
				if (target.GetComponent<Emeny>().AmIDead())
					target = null;
			}
		}
		UpdateAnimator();
	}

	void	sendRay()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out hit, Mathf.Infinity);
	}

	void Attack()
	{
		attacking = (Input.GetMouseButton(0));
		if (attacking && hit.transform?.tag != null && hit.transform.tag == "Emeny")
		{
			target = hit.transform.gameObject;
			naveMeshAgent.SetDestination(target.transform.position);
		}
	}

	public void AttackPlayer(float dmg)
	{
		health -= dmg;
	}
}
