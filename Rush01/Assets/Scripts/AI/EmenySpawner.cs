using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenySpawner : MonoBehaviour
{
	public GameObject[] emenies = new GameObject[2];
	GameObject	myref;
	GameObject Spawned;
    // Start is called before the first frame update
    void Start()
    {
		myref = emenies[Random.Range(0, emenies.Length)];
        // StartCoroutine(Spawning());
    }

	private void Update() {
		if (!Spawned)
			Spawned = GameObject.Instantiate(myref, this.transform.position, Quaternion.identity);
	}
}
