using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject head;
	private Rigidbody rigidbody;
	public float	speeed;
	float	x, y;
	float rotY, currRotY;
	float yspeed = 12;
    void Start()
    {
		rigidbody = this.GetComponentInChildren<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void FixedUpdate() {
		Move();
		Look();	
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0))
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
			this.GetComponentInChildren<SkietDieBliksem>().Shoot();
		}	
	}

	void Look()
	{
		Vector2 look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
		rotY += look.y * yspeed;
		head.transform.rotation = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
	}

	void Move()
	{
		transform.Translate(transform.forward * Input.GetAxis("Vertical") * speeed * Time.deltaTime, Space.World);
		transform.Rotate(transform.rotation.x, Input.GetAxis("Horizontal"), transform.rotation.z, Space.World);
	}
}
