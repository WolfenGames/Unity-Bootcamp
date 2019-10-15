using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    GameObject player;
    public bool followPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(followPlayer == true)
           cameraFollowPlayer(); 
    }

    public void setCamToPlayer(bool value)
    {
        followPlayer = value;
    }

    void cameraFollowPlayer()
    {
		Vector3 pos = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        Vector3 newPosition = Vector3.Lerp(this.transform.position, pos, 3 * Time.fixedDeltaTime);
        this.transform.position = newPosition;
    }
}
