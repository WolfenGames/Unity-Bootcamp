using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	[SerializeField]
	private GameObject[]		SpawnedPipes;
	private	float				timeToSpawn;
    Vector3						dirthing;
	// Start is called before the first frame update
    void Start()
    {
		timeToSpawn = 4f;
    }

    // Update is called once per frame
    void Update()
    {
		foreach(GameObject obj in SpawnedPipes)
		{
			dirthing = new Vector3(-3 * Time.deltaTime, 0, 0);
			obj.transform.Translate(dirthing);
			if (obj.transform.position.x <= -10f)
			{
				Vector3 newpos = new Vector3(10, 0, 0);
				obj.transform.position = newpos;
			}
		}
    }
}
