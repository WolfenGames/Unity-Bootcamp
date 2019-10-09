using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	[SerializeField]
	List<GameObject>		units;
    RaycastHit2D			hit2D;
	bool					ctrl, alt;
	Vector2					startvec, endvec;
	float					timeforHit, timeSinceLast;
	// Start is called before the first frame update
    void Start()
    {
		timeforHit = 3;
		timeSinceLast = 0;
		ctrl = false;
        units = new List<GameObject>();
    }

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawCube((startvec + endvec) / 2, startvec - endvec);	
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

		hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * Mathf.Infinity);
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	startvec = hit2D.transform.position.toVec2();
		// 	endvec = startvec;
		// }
        // if (Input.GetMouseButtonUp(0))
		// 	endvec = hit2D.transform.position.toVec2();

		// RaycastHit2D[] xy = Physics2D.BoxCastAll((startvec + endvec) / 2, startvec - endvec, 0f, Camera.main.transform.forward * Mathf.Infinity);
		// foreach (RaycastHit2D item in xy)
		// {
		// 	Debug.Log(item.transform.parent.name);
		// }
		// xy = xy.Where( x => x.transform.parent.GetComponent<PlayerController>() != null).ToArray();
		

		// Debug.Log(startvec + " " + endvec);
		ctrl = (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
		alt = (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.Space));
		if (Input.GetMouseButtonDown(0))
		{
			if (hit2D.transform?.parent?.GetComponent<Unit>() != null)
			{
				if (alt)
				{
					if (units.Contains(hit2D.transform.parent.gameObject))
						units.Remove(hit2D.transform.parent.gameObject);
				}
				else if (!ctrl)
				{
					units.Clear();
					units.Add(hit2D.transform.parent.gameObject);
				}
				else if (!units.Contains(hit2D.transform.parent.gameObject))
					units.Add(hit2D.transform.parent.gameObject);

			}else if (!ctrl)
				units.Clear();
		}
		if (Input.GetMouseButtonDown(1))
		{
			foreach (GameObject go in units)
				go.GetComponent<PlayerController>().SetDestinationVector2(hit2D.transform.position.toVec2());
		}
    }
}
