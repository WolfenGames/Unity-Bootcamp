using UnityEngine;

public interface Unit {
	[SerializeField]
	float		health { get; set; }
	float		oldHealth {get;set;}
	float		moveSpeed { get; set; }
	float		damage { get; set; }
	bool		selectable { get; set; }
	bool		selected { get; set; }
	string		myType { get; set; }
	GameObject	target { get; set; }
	void SetDestinationVector2(Vector2 vector);
	void Attack(GameObject gameObject);
	void TakeDamage(float x);
	void Die();
}