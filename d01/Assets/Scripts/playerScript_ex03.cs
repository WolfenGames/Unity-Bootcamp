﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex03 : MonoBehaviour
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
	static bool[]	exits =  new bool[3] { false, false, false };
	bool	won;

	[SerializeField]
	public static 	bool	canTransition;

    // Start is called before the first frame update
    void Start()
    {
		won = false;
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
		if (!won)
		{
			if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
				restartCurrentScene();
			SwitchChars();
			won = (exits[0] && exits[1] && exits[2]);
		}else
		{
			foreach(GameObject obj in players)
			{
				obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			Debug.Log("WON!!!");
			GotoNextLevel();
		}
    }

	void restartCurrentScene(){ 
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void GotoNextLevel()
	{
		if (canTransition)
			SceneManager.LoadScene(Camera.main.GetComponent<camera_ex02>().levelName);
	}

	void FixedUpdate()
	{
		if (!won)
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
			if (!CanJumpFun()) activeSelf.transform.parent = null;
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
			jumpForce = 4.6f;
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
			activeSelf2d.velocity = new Vector2(0, activeSelf2d.velocity.y);
			activeSelf2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		}
		activeSelf = players[val];
		activeSelf2d = activeSelf.GetComponent<Rigidbody2D>();
		activeSelf2d.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void	OnCollisionEnter2D(Collision2D col)
	{
		if (activeSelf && col?.transform?.name != null)
		{
			Vector2 x = new Vector2(
			activeSelf.transform.position.x - activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x, activeSelf.transform.position.y - 0.03f);
			RaycastHit2D hit =  Physics2D.Raycast(x,
										Vector2.right, 
										2 * activeSelf2d.GetComponentInChildren<Collider2D>().bounds.extents.x);
			if (hit && hit.transform.gameObject == col?.transform?.gameObject) activeSelf.transform.parent = col.transform;
		}
	}

	void	OnTriggerEnter2D(Collider2D collision)
    {
		if (activeSelf)
		{
			switch (collision.transform.tag)
			{
				case "BlueExit":
					if (activeSelf.name == "Claire")
						exits[2] = true;
					break;
				case "RedExit":
					if (activeSelf.name == "Thomas")
						exits[0] = true;
					break;
				case "YellowExit":
					if (activeSelf.name == "John")
						exits[1] = true;
					break;
				default:
					break;
			}
			if (collision?.transform?.parent?.tag == "TeleportIn")
				activeSelf.transform.position = collision.transform.parent.GetComponent<Teleport>().Destination.position;
		}
    }

	void	OnTriggerExit2D(Collider2D collision)
    {
		if (activeSelf)
		{
			switch (collision.transform.tag)
			{
				case "BlueExit":
					if (activeSelf.name == "Claire")
						exits[2] = false;
					break;
				case "RedExit":
					if (activeSelf.name == "Thomas")
						exits[0] = false;
					break;
				case "YellowExit":
					if (activeSelf.name == "John")
						exits[1] = false;
					break;
				default:
					break;
			}
		}
    }
}
