using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class OneScriptyBoy : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("ex01");
	}

	public void Quit()
	{
		EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
