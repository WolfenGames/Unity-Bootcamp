using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	public static DoorManager dm;

	private void Start() {
		dm = this;
	}

	public void OpenDoor()
	{
		GameObject.FindGameObjectsWithTag("Door").ToList().ForEach(x => GameObject.Destroy(x, 1));
	}
}
