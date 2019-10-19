using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Emeny : MonoBehaviour
{
	Animator		animator;
	NavMeshAgent	navMeshAgent;
	GameObject		target;
	public float	 Health;
	bool			attacking;
	float			distance;
    // Start is called before the first frame update
    void Start()
    {
		Health = 3;
        target = GameObject.FindGameObjectWithTag("Player");
		navMeshAgent = this.GetComponent<NavMeshAgent>();
		animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Health > 0)
		{
			if (Vector3.Distance(this.transform.position, target.transform.position) < 14)
				navMeshAgent.SetDestination(target.transform.position);

			attacking = (Vector3.Distance(this.transform.position, target.transform.position) < 2.3f);
			if (attacking)
				target.transform.GetComponent<Player>().AttackPlayer(1 * Time.deltaTime);
		}
		UpdateAnimator();
    }

	private void UpdateAnimator() {
		animator.SetBool("Attacking", attacking);
		animator.SetFloat("Health", Health - 1);
		animator.SetFloat("Distance", (attacking) ? 0 : navMeshAgent.remainingDistance);
	}

	public bool AmIDead()
	{
		if (Health < 0)
		{
			StartCoroutine("DIEMOFO");
			return true;
		}
		return false;
	}

	IEnumerator	DIEMOFO()
	{
		this.GetComponent<NavMeshAgent>().enabled = false;
		int			i = 0;
		yield return new WaitForSeconds(3);
		while( i < 15 )
		{
			float		y = this.transform.position.y;
			this.transform.position = new Vector3(this.transform.position.x, y - 1 * Time.deltaTime, this.transform.position.z);
			i++;
		}
		yield return new WaitForSeconds(5);
		GameObject.Destroy(this.gameObject);
	}

	public void Attack(float dmg)
	{
		this.Health -= dmg;
	}
}
