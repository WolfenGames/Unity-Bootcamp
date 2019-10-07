using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	static	int			press = 1;
	static	int			success = 1;
	static float		tot = 0;
	GameObject			me;
	public KeyCode		code;
	private GameObject	myObject;
	public GameObject	vLine;
	private	float		fallSpeed;

    void Start()
    {
		me = this.gameObject;
		fallSpeed = Random.Range(3, 7);
    }
    
	// Update is called once per frame
    void FixedUpdate()
    {
		if (this.transform.position.x != -100)
		{
			me.transform.localPosition -= new Vector3(0, (fallSpeed * Time.fixedDeltaTime), 0);
			if (Input.GetKeyDown(code))
			{
				press++;
				if (me.transform.position.y >= (vLine.transform.position.y - 1.5f) && me.transform.position.y <= (vLine.transform.position.y + 1f))
				{
					success++;
					GameObject.Destroy(me);
				}else if (me.transform.position.y > (vLine.transform.position.y + 1f) && me.transform.position.y <= (vLine.transform.position.y + 2f))
					GameObject.Destroy(me);
			}
			if (me.transform.localPosition.y <= -4)
			{
				GameObject.Destroy(me);
				press++;
			}
		}
    }

	void OnDestroy()
	{
		tot = (success/(float)press) * 100.0f;
		Debug.Log("Precision : " + tot);
	}
}
