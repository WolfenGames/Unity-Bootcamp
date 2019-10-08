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
	enum	StateOfBird
	{
		None,
		Flying,
		Falling,
		hasRotated,
		hasNotRotated
	}

	StateOfBird				FallState;
	StateOfBird				RotateState;
	public static bool		dided = false;
	public GameObject[]		SpawnedPipes;
    // Start is called before the first frame update
    void Start()
    {
		FallState = StateOfBird.Falling;
		RotateState = StateOfBird.hasNotRotated;
        rot = new Vector3(0, 0, -45);
    }

    // Update is called once per frame
    void Update()
    {
		if (!dided)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				newpos = new Vector3(0, 20 * Time.deltaTime, 0);
				RotateState = StateOfBird.hasRotated;
			}
			else
			{
				newpos = new Vector3(0, -2 * Time.deltaTime, 0);
				RotateState = StateOfBird.hasNotRotated;
			}
			if (birdie.transform.position.y >= -3f)
				birdie.transform.Translate(newpos);
			else
				dided = true;
			foreach(GameObject pipe in SpawnedPipes)
			{
				if (
					pipe.transform.localPosition.x == birdie.transform.localPosition.x &&
					// pipe.transform.localPosition.x - 0.8f >= birdie.transform.localPosition.x &&
					(birdie.transform.localPosition.y >= 1.76f || birdie.transform.localPosition.y <= -1.5f)
				)
					dided = true;
			}
		}
		// switch(RotateState)
		// {
		// 	case StateOfBird.hasNotRotated:
		// 		rot = new Vector3(0, 0, -45f  * Time.deltaTime);
		// 		if (birdie.transform.localRotation.z <= -0.3)
		// 			rot = Vector3.zero;
		// 		break;
		// 	case StateOfBird.hasRotated:
		// 		if (birdie.transform.localRotation.z <= 0)
		// 			rot = new Vector3(0, 0, 90 * Time.deltaTime);
		// 		else
		// 			rot = Vector3.zero;
		// 		break;
		// }
		// Debug.Log(RotateState + "	<v3>:" + rot);
		// birdie.transform.Rotate(rot);
    }
}
