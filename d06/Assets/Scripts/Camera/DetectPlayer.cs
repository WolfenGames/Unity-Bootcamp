using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
	public void doTestForClose(GameObject go)
	{
		Transform	goTransform = go.transform;
		var v3 = new Vector3(go.transform.position.x, 1, go.transform.position.z);
		Vector3 copy_trans = this.transform.forward;
		copy_trans.Normalize();  
		var dir = (v3 - transform.position);
		dir.Normalize();
		float		dot = Vector3.Dot(this.transform.forward, dir);
		RaycastHit	raycastHit;
		Physics.Raycast(this.transform.position, dir, out raycastHit, Mathf.Infinity);
		float dist = Vector3.Distance(v3, this.transform.position);
		bool inDist = (Vector3.Distance(v3, this.transform.position) > 0.5f && Vector3.Distance(v3, this.transform.position) < 20 && dot >= 0.8f);
		if (raycastHit.transform?.tag != null && raycastHit.transform.tag == "Player" &&  inDist)
			Detection.det.Dectected();
	}
}
