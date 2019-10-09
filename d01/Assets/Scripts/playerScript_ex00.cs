using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour
{
	Dictionary<KeyCode, int>	combo;
	public static GameObject	activeSelf;

	[SerializeField]
	GameObject[]				players;
	private	Rigidbody2D			activeSelf2d;

	[SerializeField]
	PhysicsMaterial2D			frictionless;

	[SerializeField]
	static float				jumpForce;

	KeyCode	Claire  			= KeyCode.Alpha3;
	KeyCode	John				= KeyCode.Alpha2;
	KeyCode	Thomas				= KeyCode.Alpha1;

	[SerializeField]
	float	MovSpeed;
    // Start is called before the first frame update
    void Start()
    {
		combo = new Dictionary<KeyCode, int>();
		combo.Add(Thomas, 0);
		combo.Add(John, 1);
        combo.Add(Claire, 2);
		// players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject obj in players)
		{
			obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			// obj.GetComponent<Rigidbody2D>().mass = ~(1 << 31);
			obj.GetComponent<Rigidbody2D>().sharedMaterial = frictionless;
		}
		activeSelf = players[0];
		GetPlayer(Thomas);
		jumpForce = 4.33f;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
			restartCurrentScene();
		SwitchChars();
    }

	void restartCurrentScene(){ 
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void FixedUpdate()
	{
		MovChars();
	}

	void	MovChars()
	{
		activeSelf2d.velocity = new Vector2(0, activeSelf2d.velocity.y);
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			activeSelf2d.velocity = new Vector2(-MovSpeed, activeSelf2d.velocity.y);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			activeSelf2d.velocity = new Vector2(MovSpeed, activeSelf2d.velocity.y);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (CanJumpFun()) activeSelf2d.velocity = new Vector2(activeSelf2d.velocity.x, jumpForce);
		}
	}

	void	SwitchChars()
	{
		if (Input.GetKeyDown(Claire))
		{
			jumpForce = 3.6f;
			GetPlayer(Claire);
		}
		if (Input.GetKeyDown(John))
		{
			jumpForce = 4.1f;
			GetPlayer(John);
		}
		if (Input.GetKeyDown(Thomas))
		{
			jumpForce = 4.33f;
			GetPlayer(Thomas);
		}
	}

	bool	CanJumpFun()
	{
		Vector2 x = new Vector2(
			activeSelf.transform.position.x - activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x, activeSelf.transform.position.y - 0.03f
		);
		return (Physics2D.Raycast(x,
									Vector2.right, 
									2 * activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x));
	}

	void	GetPlayer(KeyCode key)
	{
		int		val;
		combo.TryGetValue(key, out val);
		if (activeSelf2d)
		{
			activeSelf2d.sharedMaterial = frictionless;
			// activeSelf2d.mass = ~(1 << 31);
			activeSelf2d.velocity = new Vector2(0, activeSelf2d.velocity.y);
			activeSelf2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}
		activeSelf = players[val];
		activeSelf2d = activeSelf.GetComponent<Rigidbody2D>();
		activeSelf2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		// activeSelf2d.mass = 1;
	}
}
