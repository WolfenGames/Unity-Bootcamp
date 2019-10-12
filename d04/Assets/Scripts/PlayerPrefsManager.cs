using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
	public Text[]						infoGraphics;

	public static PlayerPrefsManager 	ppm;

	private void Start() {
		ppm = this;
		infoGraphics[0].text = Get_i("Lives").ToString();
		infoGraphics[1].text = Get_i("Gold").ToString();
		infoGraphics[3].text = string.Format("BEST SCORE {0} PTS", Get_i("MaxScore").ToString());
	}

	private void Update() {
		infoGraphics[2].text = string.Format("{0}", Get_s("SelectedLevel").Length > 0 ? Get_s("SelectedLevel") : "No Level Selected");
	}

	public void Add(string name, int val) { PlayerPrefs.SetInt(name, val); }

	public void Add(string name, string val) { PlayerPrefs.SetString(name, val); }
	public void Add(string name, float val) { PlayerPrefs.SetFloat(name, val); }
	public void Add(string name, bool val) { PlayerPrefs.SetString(name, val.ToString()); }


	public float 	Get_f(string name){ return PlayerPrefs.GetFloat(name); }
	public int	 	Get_i(string name){ return PlayerPrefs.GetInt(name); }
	public bool 	Get_b(string name){ return bool.Parse(PlayerPrefs.GetString(name)); }
	public string 	Get_s(string name){ return PlayerPrefs.GetString(name); }
}
