using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkietDieBliksem : MonoBehaviour
{
	public
	GameObject	hit;
	RaycastHit	thing;
	bool 		player;
	float		timeShoot;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.tag == "Player";
    }

	public void Shoot()
	{
		Physics.Raycast(this.transform.position + this.transform.forward * 1.8f, this.transform.forward, out thing, 199);
		this.GetComponentInParent<AudioSource>().PlayOneShot(this.GetComponentInParent<AudioSource>().clip);
		this.transform.GetComponentInChildren<ParticleSystem>().Play();
		GameObject.Instantiate(hit, thing.point, Quaternion.identity);
		if (thing.transform?.tag != null && thing.transform.tag != "Terrain")
		{
			GameObject.Destroy(thing.transform.gameObject);
		}
	}
    // Update is called once per frame
    void Update()
    {
		if (!player)
		{
			timeShoot += Time.deltaTime;
			if (timeShoot > 1)
			{
				timeShoot = 0;
				Shoot();
			}
		}
    }
}
