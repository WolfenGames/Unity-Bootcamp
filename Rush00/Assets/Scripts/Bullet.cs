using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public AudioClip lose;
	public AudioClip[] dead;
	public AudioClip clip;
	public float	lifeTime;
    public float 	speed;
	float			tot;
	public bool isInfinite = false;
	[SerializeField]
	public string[] canPass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Display.display.dead == true)
			return;
		tot += Time.deltaTime;
		lifeTime -= Time.deltaTime;
        transform.position += transform.forward * (speed * Time.deltaTime);
		if (lifeTime < 0)
			GameObject.Destroy(this.gameObject);
    }
	private void OnTriggerEnter(Collider other) {
		foreach(string s in canPass)
		{
			if (other.tag == s)
				return;
		}
		Debug.Log(other.transform.tag);
		Display.display.PlayHit();
		GameObject.Destroy(this.gameObject);
		if (other.transform.gameObject.tag == "Enemy")
		{
			Camera.main.GetComponent<AudioSource>().PlayOneShot(dead[Random.Range(0, dead.Length - 1)]);
			GameObject.Destroy(other.transform.gameObject);
		}
		if (other.transform.gameObject.tag == "Player")
		{
			Display.display.health--;
			if (Display.display.health <= 0)
			{
				Camera.main.GetComponent<AudioSource>().PlayOneShot(dead[Random.Range(0, dead.Length - 1)]);
				Camera.main.GetComponent<AudioSource>().PlayOneShot(lose);
				Display.display.dead = true;
			}
		}
	}
}
