using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController :  MonoBehaviour, Unit
{
	Vector2				dest;
	Animator			animator;
	AudioSource			sound;
	[SerializeField]
	AudioClip[]			Clips;
	public float health { get; set; }
	public float moveSpeed { get; set; }
	public float damage { get; set; }
	public bool selectable { get; set; }
	public bool selected { get; set; }

	// Start is called before the first frame update
	void Start()
    {
		animator = this.GetComponentInChildren<Animator>();
		dest = this.transform.position.toVec2();
		sound = this.GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
    }

	public void SetDestinationVector2(Vector2 vector)
	{
		Random.InitState(Random.Range(0, ~(1 << 31)));
		try
		{
			sound.clip = Clips[Random.Range(0, Clips.Length)];
			sound.Play();
		} catch (System.Exception){}
		dest = vector;
		animator.SetBool("Walking", true);
	}

	void FixedUpdate()
	{
		
		if (Vector2.Distance(this.transform.position.toVec2(), dest) <= 0.5f)
			animator.SetBool("Walking", false);
		Vector2 thing = dest - this.transform.position.toVec2();
		thing.Normalize();
		animator.SetFloat("DirX", thing.x);
		animator.SetFloat("DirY", thing.y);
		Vector2 newVec = Vector2.MoveTowards(this.transform.position.toVec2(), dest, 3 * Time.fixedDeltaTime);
		this.transform.position = new Vector3(newVec.x, newVec.y, this.transform.position.z);
	}

	public void Attack(GameObject gameObject)
	{
		gameObject.GetComponent<Unit>().TakeDamage(this.damage);
	}

	public void TakeDamage(float x)
	{
		if (health - x > 0)
		{
			health -= x;
		}
		else
			Die();
	}

	public void Die()
	{
		animator.SetBool("DIE", true);
	}
}