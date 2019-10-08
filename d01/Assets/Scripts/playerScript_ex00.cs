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
	float						jumpForce;

	KeyCode	Claire  			= KeyCode.Alpha1;
	KeyCode	John				= KeyCode.Alpha2;
	KeyCode	Thomas				= KeyCode.Alpha3;

	[SerializeField]
	float	MovSpeed;
	[SerializeField]
	bool	canJump;
    // Start is called before the first frame update
    void Start()
    {
		canJump = true;
		combo = new Dictionary<KeyCode, int>();
        combo.Add(Claire, 0);
		combo.Add(Thomas, 1);
		combo.Add(John, 2);
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject obj in players)
		{
			obj.GetComponent<Rigidbody2D>().mass = ~(1 << 31);
			obj.GetComponent<Rigidbody2D>().sharedMaterial = frictionless;
		}
		activeSelf = players[0];
		GetPlayer(Thomas);
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
		if (Input.GetKeyDown(KeyCode.Space) && CanJumpFun())
		{
			activeSelf2d.velocity = new Vector2(activeSelf2d.velocity.x, jumpForce);
		}
	}

	void	SwitchChars()
	{
		if (Input.GetKeyDown(Claire))
		{
			GetPlayer(Claire);
			jumpForce = 2f;
		}
		if (Input.GetKeyDown(John))
		{
			GetPlayer(John);
			jumpForce = 4f;
		}
		if (Input.GetKeyDown(Thomas))
		{
			GetPlayer(Thomas);
			jumpForce = 3f;
		}
	}

	bool	CanJumpFun()
	{
		Vector2 x = new Vector2(
			activeSelf.transform.position.x - activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x, 0.1f
		);
		return (Physics2D.Raycast(x,
									Vector2.right, 
									2.1f * activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x));
	}

	void	GetPlayer(KeyCode key)
	{
		int		val;
		combo.TryGetValue(key, out val);
		if (activeSelf2d)
		{
			activeSelf2d.sharedMaterial = frictionless;
			activeSelf2d.mass = ~(1 << 31);
			activeSelf2d.velocity = new Vector2(0, activeSelf2d.velocity.y);
		}
		activeSelf = players[val];
		activeSelf2d = activeSelf.GetComponent<Rigidbody2D>();
		activeSelf2d.mass = 1;
		// activeSelf2d.sharedMaterial = null;
	}
}
