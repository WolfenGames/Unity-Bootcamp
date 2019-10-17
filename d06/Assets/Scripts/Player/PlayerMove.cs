using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	public float		minclamp, maxclamp;
	float				currRotX, currRotY;
	// Rigidbody		rigidbody;
	CharacterController	cc;
	public float	speed, xspeed, yspeed;
	float			x, y;
	float			rotX, rotY, rotXVel, rotYVel;

	public float	smoothDamp;
    // Start is called before the first frame update
    void Start()
    {
		cc = this.transform.GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		// currRotX = this.transform.rotation.x;
		// currRotY = this.transform.rotation.y;
    }

	void	Look()
	{
		Vector2 look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		rotX -= look.x * xspeed;
		rotY += look.y * yspeed;

		rotX = Mathf.Clamp(rotX, minclamp, maxclamp);
		currRotX = Mathf.SmoothDamp(currRotX, rotX, ref rotXVel, smoothDamp);
		currRotY = Mathf.SmoothDamp(currRotY, rotY, ref rotYVel, smoothDamp);
		Camera.main.transform.rotation = Quaternion.Euler(currRotX, currRotY,  0);
		transform.rotation = Quaternion.Euler(0, currRotY + 90, 0);
	}

	void Move()
	{
		Vector2 mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Vector3 a = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(mov.x , 0, mov.y) * (speed * Time.deltaTime);
		cc.Move(a);
	}

	private void Update() {
		
		if (Time.timeScale != 0)
		{
			Look();
			Move();
		}
	}
}
