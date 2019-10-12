using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static PauseMenu pm;

	public GameObject		menuLayer;
	public GameObject		confirmLayer;
	public GameObject		gameLayer;
	bool 					paused = false;
    // Start is called before the first frame update
    void Start()
    {
		if (!pm)
			pm = this;
		gameLayer.SetActive(true);
		menuLayer.SetActive(false);
		confirmLayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!menuLayer.activeSelf)
			{
				Openmenu();
			}else
			{
				CloseMenu();
			}
		}
    }

	public void OpenConfirm()
	{
		menuLayer.SetActive(false);
		confirmLayer.SetActive(true);
	}

	public void Openmenu()
	{
		paused = true;
		gameManager.gm.pause(true);
		gameLayer.GetComponent<CanvasGroup>().interactable = false;
		menuLayer.SetActive(true);
	}

	public void CloseMenu()
	{
		paused = false;
		menuLayer.SetActive(false);
		gameLayer.GetComponent<CanvasGroup>().interactable = true;
		gameManager.gm.pause(false);
	}

	public void No()
	{
		confirmLayer.SetActive(false);
		menuLayer.SetActive(true);
	}

	public bool isPaused()
	{
		return paused;
	}

	public void Yes()
	{
		SceneManager.LoadScene("ex00");
	}
}
