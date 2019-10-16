using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
	public static Detection 	det;
	public float				detectionVal;
	public float				decrement;
	public float				decRate;
	public List<GameObject>		CameraScene;
	RaycastHit					rayCastHit;
	Color						og, red;
    // Start is called before the first frame update
    void Start()
    {
		red = Color.red;
		// og = GameObject.FindGameObjectWithTag("DetectionBar").GetComponent<Slider>().image.color;
		og = Color.blue;
        det = this;
		decRate = 6;
		decrement = 12;
		CameraScene = GameObject.FindGameObjectsWithTag("detectPlayer").ToList();
    }

    // Update is called once per frame
    void Update()
    {
		foreach (GameObject go in CameraScene)
			go.GetComponent<DetectPlayer>().doTestForClose(this.gameObject);
		if (detectionVal > 75)
		{
			GameObject.FindGameObjectWithTag("DetectionBar").GetComponentsInChildren<Image>()[1].color = red;
			GameObject.FindGameObjectsWithTag("Alarms").ToList().ForEach(x => x.GetComponent<AudioSource>().PlayOneShot(x.GetComponent<AudioSource>().clip));
		}
		else
		{
			foreach (GameObject alarm in GameObject.FindGameObjectsWithTag("Alarms").ToList())
			{
				if (!alarm.GetComponent<AudioSource>().isPlaying)
				{
					alarm.GetComponent<AudioSource>().Stop();
				}
			}
			GameObject.FindGameObjectWithTag("DetectionBar").GetComponentsInChildren<Image>()[1].color = og;
		}
        if (detectionVal > 100)
			GameManager.game.Lose();
		detectionVal -= (decrement + decRate) * Time.deltaTime;
		detectionVal = Mathf.Clamp(detectionVal, 0, 100);
		GameObject.FindGameObjectWithTag("DetectionBar").GetComponent<Slider>().value = detectionVal;
    }

	public void Dectected()
	{
		detectionVal += (30 * Time.deltaTime);
	}

	public void ChangeDecRate(bool detectionRate)
	{
		decRate += (detectionRate) ? -5 : 5;
	}
}
