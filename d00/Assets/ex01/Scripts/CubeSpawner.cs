using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	public GameObject[] 		buttons;
	public GameObject[] 		spawners;

	private GameObject			currRail;
	private float				timeTillSpawn;
    // Start is called before the first frame update
    int							buttonIndex;
	void Start()
    {
		Random.InitState(Random.Range(0, ~(1 << 31)));
		timeTillSpawn = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        timeTillSpawn -= Time.deltaTime;

		if (timeTillSpawn <= 1)
		{
			DoRailThings();
			timeTillSpawn = Random.Range(1f, 3f);
		}
    }

	void	DoRailThings()
	{
		buttonIndex = Random.Range(0, 3);
		currRail = spawners[buttonIndex];
		Vector3 newPos = currRail.transform.position;
		GameObject.Instantiate(buttons[buttonIndex], newPos, Quaternion.identity);
	}
}
