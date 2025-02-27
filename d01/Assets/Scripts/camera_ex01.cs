﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_ex01 : MonoBehaviour
{
	Vector3		playerPos;
	[SerializeField]
	float		lerpSpeed;
    // Start is called before the first frame update
	[SerializeField]
	public string	levelName;

    void Start()
    {
        playerPos = new Vector3();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (playerScript_ex01.activeSelf)
		{
			playerPos.x = playerScript_ex01.activeSelf.transform.position.x;
			playerPos.y = playerScript_ex01.activeSelf.transform.position.y;
			playerPos.z = -3;
			Camera.main.transform.transform.position = Vector3.Lerp(Camera.main.transform.position, playerPos, lerpSpeed * Time.deltaTime);
		}
    }
}
