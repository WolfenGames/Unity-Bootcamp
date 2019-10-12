using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public GameObject		myObjToSpawn;
	public GameObject		sprite;

	public Text[]			fields;
	GameObject				go;
	static RaycastHit2D		raycastHit2D;
	Color					color;
	public Image			fly;
	public Sprite[]			flyNoFly;
	public GameObject		spr;
	Color					defColor;
    // Start is called before the first frame update
	
	void Start()
	{
		// myObjToSpawn.GetComponentInChildren<towerScript>().enabled = false;
		fields[0].text = string.Format("{0}/{1} s",1, myObjToSpawn.GetComponentInChildren<towerScript>().fireRate.ToString());
		fields[1].text = myObjToSpawn.GetComponentInChildren<towerScript>().energy.ToString();
		fields[2].text = myObjToSpawn.GetComponentInChildren<towerScript>().range.ToString();
		fields[3].text = myObjToSpawn.GetComponentInChildren<towerScript>().damage.ToString();
		if (myObjToSpawn.GetComponentInChildren<towerScript>().type == towerScript.Type.gatling || myObjToSpawn.GetComponentInChildren<towerScript>().type == towerScript.Type.rocket)
			fly.sprite = flyNoFly[0];
		else
			fly.sprite = flyNoFly[1];
			defColor = spr.GetComponent<Image>().color;
	}
	
	void OnBeginDrag(PointerEventData eventData)
	{
		go = GameObject.Instantiate(go, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
	}

	private void Update() {
		raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, Mathf.Infinity);
		if (PauseMenu.pm.isPaused() || gameManager.gm.playerEnergy < myObjToSpawn.GetComponent<towerScript>().energy)
		{
			if (go)
			{
				GameObject.Destroy(go);	
				go = null;
			}
		}
		if (gameManager.gm.playerEnergy > myObjToSpawn.GetComponent<towerScript>().energy)
			spr.GetComponent<Image>().color = defColor;
		else
			spr.GetComponent<Image>().color = Color.gray;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (gameManager.gm.playerEnergy > (gameManager.gm.playerEnergy - myObjToSpawn.GetComponent<towerScript>().energy))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (go)
			{
				go.transform.position = new Vector3(pos.x, pos.y, 5);
				if (raycastHit2D.transform?.tag == null || raycastHit2D.transform.tag != "empty")
				{
					go.transform.GetComponent<SpriteRenderer>().color = Color.red;
				}
				else
					go.transform.GetComponent<SpriteRenderer>().color = color;
			}
		}else
		{
			GameObject.Destroy(go);
			go = null;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!PauseMenu.pm.isPaused() && gameManager.gm.playerEnergy > myObjToSpawn.GetComponent<towerScript>().energy)
		{
			if (raycastHit2D.transform?.tag != null && raycastHit2D.transform.tag != "tower" && raycastHit2D.transform.tag == "empty")
			{
				GameObject.Destroy(go);
				go = GameObject.Instantiate(myObjToSpawn);
				gameManager.gm.playerEnergy -= go.GetComponent<towerScript>().energy;
				go.transform.position = new Vector3(raycastHit2D.transform.position.x, raycastHit2D.transform.position.y, 1);
				go.transform.parent = raycastHit2D.transform;
				go = null;
				// go.GetComponent<towerScript>().enabled = true;
			}
			else
			{
				GameObject.Destroy(go);
			}
		}
		else
		{
			GameObject.Destroy(go);
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (!PauseMenu.pm.isPaused() && (gameManager.gm.playerEnergy >= myObjToSpawn.GetComponent<towerScript>().energy))
		{
			// Debug.Log(eventData);
			go = GameObject.Instantiate(sprite, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
			go.transform.GetChild(0).localScale = Vector3.one * myObjToSpawn.GetComponent<towerScript>().range * 2;
			color = go.GetComponent<SpriteRenderer>().color;
		}
	}
}
