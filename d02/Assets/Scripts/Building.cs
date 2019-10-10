using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, Unit
{
	public float health { get; set; }
	public float moveSpeed { get; set; }
	public float damage { get; set; }
	public bool selectable { get; set; }
	public bool selected { get; set; }
	public GameObject target { get; set; }

	public float my_set_health;
	public bool		goodOrBad;

	public bool		GetGoodOrBad()
	{
		return goodOrBad;
	}

	public void Attack(GameObject gameObject)
	{
		
	}

	public void Die()
	{
		GameObject.Destroy(this.gameObject);
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
		health = my_set_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
