using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
	public KeyCode	keyToPress;
	public bool		start;
	public string	SceneToLoad;

	int			x,y;

	[SerializeField]
	GameObject[]	LevelSelectorThing;
	public	  Image	LevelSelectorImg;
	private void Start() {
		LevelSelectorThing = GameObject.FindGameObjectsWithTag("LevelThing");
		foreach(GameObject go in LevelSelectorThing)
		{
			go.GetComponentInChildren<Image>().color = Color.gray;
		}
	}
	
    void Update()
    {
		x = PlayerPrefsManager.ppm.Get_i("X");
		y = PlayerPrefsManager.ppm.Get_i("Y");
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			x--;
		if (Input.GetKeyDown(KeyCode.RightArrow))
			x++;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			y--;
		if (Input.GetKeyDown(KeyCode.DownArrow))
			y++;
		if (y > 2) y = 0;
		if (y < 0) y = 2;

		if (x > 3) x = 0;
		if (x < 0) x = 3;
		if (start)
		{
			if (Input.GetKeyDown(keyToPress))
			{
				SceneManager.LoadScene(SceneToLoad);
			}
		}
		else
		{
			GameObject go = LevelSelectorThing[4 * y + x];

			foreach(GameObject go2 in LevelSelectorThing)
			{
				go2.GetComponentInChildren<Image>().color = Color.gray;
			}
			if (go)
			{
				go.GetComponentInChildren<Image>().color = Color.white;
				PlayerPrefsManager.ppm.Add("SelectedLevel", go?.transform?.GetChild(0)?.GetComponentInChildren<Text>()?.text);
			}
		}
		PlayerPrefsManager.ppm.Add("X", x);
		PlayerPrefsManager.ppm.Add("Y", y);
    }
}
