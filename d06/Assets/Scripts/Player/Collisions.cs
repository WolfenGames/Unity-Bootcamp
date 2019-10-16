using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
	private void OnCollisionEnter(Collision other) {
		Debug.Log(other.transform?.tag);
	}
	private void OnTriggerExit(Collider other) {
		if (other.transform?.tag != null && other.transform.tag == "LowerDetection")
			Detection.det.ChangeDecRate(false);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.transform?.tag != null )
		{
			if (other.transform.tag == "Switcheroo")
			{
				if (PlayerObjects.playerObjects.HasKeyCard())
				{
					DoorManager.dm.OpenDoor();
					PlayerObjects.playerObjects.OpenDoor();
				}
			}
			if (other.transform.tag == "Papers")
			{
				Debug.Log("WIN");
			}
			if (other.transform.tag == "KeyCard")
				PlayerObjects.playerObjects.HasKeyCard(true);
			if (other.transform.tag == "LowerDetection")
				Detection.det.ChangeDecRate(true);
			if (other.transform.tag == "Lazors")
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
