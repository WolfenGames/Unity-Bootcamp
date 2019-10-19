using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	Transform		otransform;
	public Transform	player;
    // Start is called before the first frame update
    void Start()
    {
        this.otransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z - 3);
		this.transform.eulerAngles = new Vector3(70, 0, 0);//otransform.localRotation;
    }
}
