using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
	public static GUIManager gm;
	public float			time;
    // Start is called before the first frame update
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
    }
}
