using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 direction;
    Camera  camera;
    Rigidbody2D body;
    public Vector3 display;
    // Start is called before the first frame update
    void Start()
    {
		//body = this.GetComponent<Rigidbody2D>();
		camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		if (Display.display.dead == true)
			return;
        rotatebody();
    }

    private void rotatebody()
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
        transform.localRotation = Quaternion.Euler(0, -transform.rotation.eulerAngles.z, 0);
    }
}
