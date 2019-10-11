using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour, Unit
{
	public float health { get; set; }
	public float oldHealth { get; set; }
	public float moveSpeed { get; set; }
	public float damage { get; set; }
	public bool selectable { get; set; }
	public bool selected { get; set; }
	public GameObject target { get; set; }
	public string myType { get ; set; }

	Animator			animator;

	List<GameObject>	targets;

	public void Attack(GameObject gameObject)
	{
		Debug.Log(myType + " [" + gameObject.GetComponent<Unit>().health + "/" + gameObject.GetComponent<Unit>().oldHealth + "]HP has been attacked");
		gameObject.GetComponent<PlayerController>().TakeDamage(this.damage);
	}

	public void Die()
	{
	}

	public void SetDestinationVector2(Vector2 vector)
	{
	}

	public void TakeDamage(float x)
	{
		if (health - x > 0)
			health -= x;
		else
			Die();
	}

	// Start is called before the first frame update
	void Start()
    {
		animator = this.GetComponent<Animator>();
        health = 200;
		oldHealth = health;
		damage = 1f;
		myType = "Orc Unit";
    }

    // Update is called once per frame
    void Update()
    {
		float	minDist = ~(1 << 31);
		targets = GameObject.FindGameObjectsWithTag("Player").ToList();
		if (target)
		{
			if (Vector2.Distance(this.transform.position.toVec2(), target.transform.position) <= 0.5f)
				animator.SetBool("Walking", false);
			Vector2 thing = target.transform.position.toVec2() - this.transform.position.toVec2();
			thing.Normalize();
			animator.SetFloat("DirX", thing.x);
			animator.SetFloat("DirY", thing.y);
		}
		foreach (GameObject item in targets)
		{
			if (Vector2.Distance(item.transform.position.toVec2(), this.transform.position.toVec2()) < minDist)
			{
				minDist = Vector2.Distance(item.transform.position.toVec2(), this.transform.position.toVec2());
				target = item;
			}
		}
		if (target && minDist < 1.3f)
		{
			animator.SetBool("Walking", false);
			// target.GetComponent<PlayerController>().TakeDamage(this.damage);
			Attack(target);
			animator.SetBool("Attacking", true);
		}else
		{
			animator.SetBool("Attacking", false);
			animator.SetBool("Waling", true);
			if (target)
			{
				Vector2 next = Vector2.MoveTowards(this.transform.position.toVec2(), target.transform.position.toVec2(), 1f * Time.deltaTime);
				this.transform.position = new Vector3(next.x, next.y, this.transform.position.z);
			}
		}
    }
}
