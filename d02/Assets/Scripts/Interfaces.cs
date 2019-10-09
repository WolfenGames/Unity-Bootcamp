using UnityEngine;

public interface Unit {
	float		health { get; set; }
	float		moveSpeed { get; set; }
	float		damage { get; set; }
	bool		selectable { get; set; }
	bool		selected { get; set; }
	void SetDestinationVector2(Vector2 vector);
	void Attack(GameObject gameObject);
	void TakeDamage(float x);
	void Die();
}