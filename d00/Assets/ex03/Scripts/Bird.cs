using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	[SerializeField]
	private	GameObject		birdie;
	[SerializeField]
	private	Vector3			newpos;
	private Vector3			rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = new Vector3(0, 0, -45);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			newpos = new Vector3(0, 20 * Time.deltaTime, 0);
		}
		else
		{
			newpos = new Vector3(0, -1 * Time.deltaTime, 0);
		}
		if (birdie.transform.position.y >= -3.5f)
			birdie.transform.Translate(newpos);
    }
}
