using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjects : MonoBehaviour
{
	public static PlayerObjects playerObjects;
	Dictionary<string, bool> objectives = new Dictionary<string, bool>();
	public GameObject	prefab;
	public GameObject	parentThing;

	private void Start() {
		playerObjects = this;
		objectives.Add("(optional) Disable Lazers", true);
		objectives.Add("Get Papers", false);
		objectives.Add("Aquire Keycard", true);
		objectives.Add("Open Door", false);
		objectives.Add("Don't Get Detected", true);
		foreach(string obj in objectives.Keys)
		{
			GameObject go = GameObject.Instantiate(prefab);
			bool x;
			objectives.TryGetValue(obj, out x); 
			if (x)
			{
				prefab.GetComponent<Text>().text = obj;
				go.transform.parent = parentThing.transform;  
			}
		}
	}

	public void UpDateList()
	{
		foreach(Transform go in parentThing.transform)
			GameObject.Destroy(go.gameObject);
		foreach(string obj in objectives.Keys)
		{
			GameObject go = GameObject.Instantiate(prefab);
			bool x;
			objectives.TryGetValue(obj, out x); 
			if (x)
			{
				prefab.GetComponent<Text>().text = obj;
				go.transform.parent = parentThing.transform;  
			}
		}
	}

	private bool _hasKeyCard = false;
	public void HasKeyCard(bool val)
	{
		_hasKeyCard = val;
		objectives["Aquire Keycard"] = false;
		objectives["Open Door"] = true;
		UpDateList();
	}

	public void OpenDoor()
	{
		if (_hasKeyCard)
		{
			objectives["Open Door"] = false;
			objectives["Get Papers"] = true;
			UpDateList();
		}
	}

	public bool HasKeyCard()
	{
		return _hasKeyCard;
	}
}
