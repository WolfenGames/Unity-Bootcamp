using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
	public GameObject 	balloon;
	public GameObject	textDisplay;
	public float		balloonTime = 120f;
	public int			shrinkRate = 4;
	public int			growRate = 8;

	public float 		breath = 10f;
	
	private bool		dieded;

	private string		consoleString = "Balloon life time: ";
    // Start is called before the first frame update
    void Start()
    {
        dieded = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!dieded)
			BalloonLogic();
		else
			DisplayDiededScreen();
    }

	void	BalloonLogic()
	{
		if (balloonTime >= 0)
		{
			balloonTime -= Time.deltaTime;

			if (Input.GetKeyDown(KeyCode.Space) && breath >= 0.3f)
			{
				balloon.transform.localScale += new Vector3(growRate * Time.deltaTime, growRate * Time.deltaTime, 1);
				breath -= (breath >= 0.3f) ? 20 * Time.deltaTime: 0;
			}else
			{
				balloon.transform.localScale -= (balloon.transform.localScale.x > 1f && balloon.transform.localScale.y > 1f) ? new Vector3(shrinkRate * Time.deltaTime, shrinkRate * Time.deltaTime, 1) : Vector3.zero;
				breath += (breath < 4) ? 1.5f * Time.deltaTime: 0;
			}
			if (balloon.transform.localScale.x >= 10 && balloon.transform.localScale.y >= 10)
			{
				GameObject.Destroy(balloon);
				dieded = true;
			}
		}
		else
			dieded = true;
	}

	void	DisplayDiededScreen()
	{
		Debug.Log(consoleString + Mathf.RoundToInt(balloonTime) + "s");
	}
}
