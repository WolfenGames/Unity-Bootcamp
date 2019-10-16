using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = this;
    }

	public void Win()
	{
		Debug.Log("YAY");
	}

	public void Lose()
	{
		Time.timeScale = 0;
	}
}
