using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera_ex00 : MonoBehaviour
{
	Vector3		playerPos;
	[SerializeField]
	float		lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector3();
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad5))
			SceneManager.LoadScene("ex01");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (playerScript_ex00.activeSelf)
		{
			playerPos.x = playerScript_ex00.activeSelf.transform.position.x;
			playerPos.y = playerScript_ex00.activeSelf.transform.position.y;
			playerPos.z = -3;
			Camera.main.transform.transform.position = Vector3.Lerp(Camera.main.transform.position, playerPos, lerpSpeed * Time.deltaTime);
		}
    }
}
