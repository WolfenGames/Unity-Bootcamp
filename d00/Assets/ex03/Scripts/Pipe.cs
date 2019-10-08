using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	public GameObject[]			SpawnedPipes;
    Vector3						dirthing;
	float						movespeed;
	static float				timeSinceStart = 0;
	// Start is called before the first frame update
    void Start()
    {
		movespeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
		if (Bird.dided == false)
		{
			timeSinceStart += Time.deltaTime;
			foreach(GameObject obj in SpawnedPipes)
			{
				dirthing = new Vector3(-movespeed * Time.deltaTime, 0, 0);
				obj.transform.Translate(dirthing);
				if (obj.transform.position.x <= -10f)
				{
					Vector3 newpos = new Vector3(10, 0, 0);
					obj.transform.position = newpos;
				}
			}
			if (timeSinceStart >= 10)
			{
				movespeed += 10;
				timeSinceStart = 0;
			}
		}
    }
}
