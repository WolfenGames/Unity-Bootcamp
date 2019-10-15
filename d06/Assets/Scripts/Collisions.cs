using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.transform?.tag != null && other.transform.tag == "Lazors")
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
