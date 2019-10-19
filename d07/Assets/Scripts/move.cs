using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour
{
	int val;
	float		choice;
	public	GameObject[]	target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		choice -= Time.deltaTime;
		if (choice <= 0)
		{
			val = Random.Range(0, 2);
			choice = Random.Range(10, 15);
		}
		if (target[val])
			this.GetComponent<NavMeshAgent>().SetDestination(target[val].transform.position);
    }
}
