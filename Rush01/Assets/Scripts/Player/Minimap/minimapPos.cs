using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapPos : MonoBehaviour
{
    private Transform trans;
    public Vector3 offset;

    void Start()
    {
        trans = transform;
    }

    void Update()
    {
        transform.position = Player.p.transform.position + offset;
    }
}
