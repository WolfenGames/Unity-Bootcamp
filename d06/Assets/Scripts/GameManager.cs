using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager game;
	bool			winLost = false;
	float 			timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        game = this;
    }

	public void Win()
	{
		winLost = true;
		Time.timeScale = 0;
		GameObject.FindGameObjectWithTag("WinLose").GetComponent<Text>().enabled = true;
		GameObject.FindGameObjectWithTag("WinLose").GetComponent<Text>().text = "Yay you won!\nEnter to try again.";
	}

	void Update()
	{
		if (winLost)
		{
			timeElapsed += 0.33f;
			if (Input.GetKey(KeyCode.Return) && timeElapsed > 1)
			{
				Time.timeScale = 1f;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	public void Lose()
	{
		winLost = true;
		Time.timeScale = 0;
		GameObject.FindGameObjectWithTag("WinLose").GetComponent<Text>().enabled = true;
		GameObject.FindGameObjectWithTag("WinLose").GetComponent<Text>().text = "aww you lost!\nEnter to try again.";
	}
}
