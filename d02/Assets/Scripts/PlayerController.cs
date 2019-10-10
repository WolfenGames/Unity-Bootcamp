using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController :  MonoBehaviour, Unit
{
	Vector2				dest;
	Animator			animator;
	AudioSource			sound;
	[SerializeField]
	AudioClip[]			Clips;
	RaycastHit2D		frontLine;
	public float 		health { get; set; }
	public float 		moveSpeed { get; set; }
	public float 		damage { get; set; }
	public bool 		selectable { get; set; }
	public bool 		selected { get; set; }
	public GameObject 	target { get; set; }

	Vector2 thing;
	List<GameObject>	units;
	float				sleep;
	float				timeOfCollision;
	// Start is called before the first frame update
	void Start()
    {
		timeOfCollision = 0;
		health = 100;
		damage = 4;
		animator = this.GetComponentInChildren<Animator>();
		dest = this.transform.position.toVec2();
		sound = this.GetComponent<AudioSource>();
		thing = dest - this.transform.position.toVec2();
		units = new List<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
    }

	public void SetDestinationVector2(Vector2 vector)
	{
		timeOfCollision = 0;
		target = null;
		if (!animator.GetBool("Die"))
		{
			Random.InitState(Random.Range(0, ~(1 << 31)));
			try
			{
				sound.clip = Clips[Random.Range(0, 4)];
				sound.Play();
			} catch (System.Exception){}
			dest = vector;
			animator.SetBool("Walking", true);
		}
	}

	public void SetDestinationVector2(Vector2 vector, GameObject target)
	{
		timeOfCollision = 0;
		if (!target)
		{
			SetDestinationVector2(vector);
			return;
		}
		if (!animator.GetBool("Die"))
		{
			this.target = target;
			Random.InitState(Random.Range(0, ~(1 << 31)));
			try
			{
				sound.clip = Clips[Random.Range(0, 4)];
				sound.Play();
			} catch (System.Exception){}
			dest = vector;
			animator.SetBool("Walking", true);
		}
	}

	void FixedUpdate()
	{
		sleep -= Time.deltaTime;
		if (animator && !animator.GetBool("Die") && sleep <= 0 && timeOfCollision <= 3)
		{
			if (Vector2.Distance(this.transform.position.toVec2(), dest) <= 0.5f)
				animator.SetBool("Walking", false);
			Vector2 thing = dest - this.transform.position.toVec2();
			thing.Normalize();
			animator.SetFloat("DirX", thing.x);
			animator.SetFloat("DirY", thing.y);
			if (target && Mathf.Abs((target.transform.position - this.transform.position).magnitude) <= (target.GetComponentInChildren<BoxCollider2D>().bounds.size.magnitude/2) + 0.4f)
			{
				animator.SetBool("Attacking", true);
				animator.SetBool("Walking", false);
				try
				{
					target.GetComponent<PlayerController>().TakeDamage(this.damage);
					if (target.GetComponent<PlayerController>().isDead())
						target = null;
				} catch (System.Exception)
				{
					Building fish = target.GetComponent<Building>();
					if (fish && fish.GetGoodOrBad())
					{
						fish.TakeDamage(this.damage);
						if (!target)
							target = null;
					}
					else
						target = null;
				}
			}else
			{
				// animator.SetBool("Walking", true);
				animator.SetBool("Attacking", false);
				Vector2 newVec = Vector2.MoveTowards(this.transform.position.toVec2(), dest, 3f * Time.fixedDeltaTime);
				this.transform.position = new Vector3(newVec.x, newVec.y, this.transform.position.z);
			}
		}else
		{
			if (animator)
				animator.SetBool("Walking", false);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(this.transform.position, dest);
	}

	public void Attack(GameObject gameObject)
	{
		gameObject.GetComponent<Unit>().TakeDamage(this.damage);
	}

	public void TakeDamage(float x)
	{
		if (health - x > 0)
			health -= x;
		else
			Die();
	}

	public bool isMoving()
	{
		return (this.transform.position.toVec2() != dest);
	}

	public bool isDead()
	{
		return animator.GetBool("Die");
	}

	public void Die()
	{
		sound.clip = Clips[4];
		sound.Play();
		animator.SetBool("Die", true);
		GameObject.Destroy(this.gameObject, 3f);
	}

	private void OnCollisionStay2D(Collision2D col) {
		
		if(col.transform.GetComponentInParent(typeof(PlayerController)))
			timeOfCollision += Time.deltaTime;
	}

	private void OnCollisionExit2D(Collision2D other) {
		timeOfCollision = 0;	
	}

	void OnCollisionEnter2D(Collision2D col)
    {
		if(col.transform.GetComponentInParent(typeof(PlayerController)))
		{
			dest = col.transform.position;
			timeOfCollision += Time.deltaTime;
		}
		if (col.collider.GetType() == typeof(EdgeCollider2D))
			dest = this.transform.position.toVec2();
    }
}