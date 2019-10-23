using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform TeleporterIn;
    public Transform TeleporterOut;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TeleporterIn.position, 3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(TeleporterOut.position, new Vector2(2, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
