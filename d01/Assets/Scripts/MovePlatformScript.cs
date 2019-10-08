using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour
{
	Vector3	StartPos;
	public Vector3	EndPos;
	public float		moveSpeed;
	enum	Dir
	{
		none,
		left,
		right,
		up,
		down
	}
	[SerializeField]
	Dir	myDir = Dir.none;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (this.transform.localPosition != EndPos && myDir == Dir.left)
			this.transform.localPosition = Vector3.Lerp(this.transform.position, EndPos , moveSpeed * Time.fixedDeltaTime);
		if (this.transform.localPosition != EndPos && myDir == Dir.right)
			this.transform.localPosition = Vector3.Lerp(this.transform.position, EndPos , -moveSpeed * Time.fixedDeltaTime);
		if (this.transform.localPosition != EndPos && myDir == Dir.up)
			this.transform.localPosition = Vector3.Lerp(this.transform.position, EndPos , moveSpeed * Time.fixedDeltaTime);
		if (this.transform.localPosition != EndPos && myDir == Dir.down)
			this.transform.localPosition = Vector3.Lerp(this.transform.position, EndPos , -moveSpeed * Time.fixedDeltaTime);
    }
}
