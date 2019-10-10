using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	List<GameObject>	actives;
	public GameObject	Ref_prefab;
	public Transform	SpawnPoint;
	float				timeToSpawn, timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
		timeToSpawn = 10f;
		actives = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
		actives = GameObject.FindGameObjectsWithTag("Player").ToList();
        timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > timeToSpawn)
		{
			if (Ref_prefab)
				GameObject.Instantiate(Ref_prefab, SpawnPoint.position, Quaternion.identity);
			timeSinceLastSpawn = 0;
		}
    }
}
