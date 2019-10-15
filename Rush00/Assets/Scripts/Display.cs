using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Display : MonoBehaviour
{
	public AudioClip hit;
	static public Display display;
	public bool dead;
	public bool win;
	public Text YouDied;
	float cd = 3.0f;
	public string next;
	public Text Ammo;
	public int ammo;
	public GameObject healthbar;
	public int health = 9;
    // Start is called before the first frame update
    void Start()
    {
		if (!display)
        	display = this;
    }

    // Update is called once per frame
    void Update()
    {
		YouDied.text = win ? "You Win" : (dead ? "Mission Failed" : "");
		if (win)
		{
			cd -= Time.deltaTime;
			if (cd < 0)
				SceneManager.LoadScene(next);
		}
		if (dead)
		{
			cd -= Time.deltaTime;
			if (cd < 0)
				SceneManager.LoadScene("menu");
		}
		for (int i = 0; i < 9; i++)
		{
			healthbar.transform.GetChild(i).gameObject.SetActive(i < health);
		}
		Ammo.text = ammo.ToString();
    }
	public void PlayHit()
	{
		Camera.main.GetComponent<AudioSource>().PlayOneShot(hit);
	}
}
