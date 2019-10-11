using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
	[SerializeField]
	List<GameObject>		units;
    RaycastHit2D			hit2D;
	bool					ctrl, alt;
	Vector2					startvec, endvec;
	float					timeforHit, timeSinceLast;
	[SerializeField]
	Image					rectal_thing;

	Vector2					analThing;
	// Start is called before the first frame update
    void Start()
    {
		timeforHit = 3;
		timeSinceLast = 0;
		ctrl = false;
        units = new List<GameObject>();
		rectal_thing.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLast += Time.deltaTime;
		if (timeSinceLast > timeforHit)
		{
			timeSinceLast = 0;
			endvec = startvec;
		}
		ctrl = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
		alt = (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.Space));
		if (Input.GetMouseButtonDown(0))
		{
			rectal_thing.gameObject.SetActive(true);
			startvec = Camera.main.ScreenToWorldPoint(Input.mousePosition).toVec2();
			analThing = Camera.main.ScreenToViewportPoint(Input.mousePosition) * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
		}
		if (Input.GetMouseButton(0))
		{
			endvec = Camera.main.ScreenToWorldPoint(Input.mousePosition).toVec2();
			Vector2 one = analThing;// * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			Vector2 two = Camera.main.ScreenToViewportPoint(Input.mousePosition).toVec2() * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);// * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			if (one.x > two.x)
			{
				float t = one.x;
				one.x = two.x;
				two.x = t;
			}
			if (one.y > two.y)
			{
				float y = one.y;
				one.y = two.y;
				two.y = y;
			}
			rectal_thing.GetComponent<RectTransform>().anchoredPosition = one;
			rectal_thing.GetComponent<RectTransform>().sizeDelta = (two - one);
			List<Collider2D> col = Physics2D.OverlapAreaAll(startvec, endvec).ToList().Where(x => x?.gameObject?.transform?.parent?.gameObject?.GetComponent<PlayerController>() != null ).ToList();
			if (col.Count > 0)
			{
				rectal_thing.color = Color.green;
			}
			else
			{
				rectal_thing.color = Color.white;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			List<Collider2D> col = Physics2D.OverlapAreaAll(startvec, endvec).ToList().Where(x => x?.gameObject?.transform?.parent?.gameObject?.tag == "Player" ).ToList();
			if (col.Count > 0)
			{
				// bool firstRun = true;
				if (!ctrl)
					units.Clear();
				foreach (Collider2D item in col)
				{
					GameObject go = item.transform.parent.gameObject;
					if (ctrl)
					{
						if (!units.Contains(go))
							units.Add(go);
					}
					else
					if (alt)
					{
						if (units.Contains(go))
							units.Remove(go);
					}
					else
					if (!ctrl && !alt)
						units.Add(go);
				}
			}else
			{
				RaycastHit2D x = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * Mathf.Infinity);
				
				foreach (GameObject go in units)
				{
					go.GetComponent<PlayerController>().SetDestinationVector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).toVec2(), ((x.transform?.gameObject.tag == "Attackable") ? x.transform.gameObject : null));//.transform.position.toVec2());
				}
			}
			rectal_thing.gameObject.SetActive(false);
			rectal_thing.color = Color.white;
		}
		if (Input.GetMouseButton(1))
		{
			units.Clear();
		}
	}
}
