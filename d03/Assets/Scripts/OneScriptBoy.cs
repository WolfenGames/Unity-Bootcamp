using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class OneScriptBoy : MonoBehaviour
{
	public void StartGame()
	{
		try
		{
			SceneManager.LoadScene("ex01");
		} catch (System.Exception)
		{
			Debug.Log(" I have no level.. EY!!");
		}
	}

	public void Quit()
	{
		EditorApplication.isPlaying = false;
		Application.Quit();
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
