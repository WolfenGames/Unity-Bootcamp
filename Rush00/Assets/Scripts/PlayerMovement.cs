using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	AudioSource source;
	public AudioClip pickup;
	public AudioClip win;
    Rigidbody rb;
    Shoot shoot;
    public GameObject WeaponSlot;
    Animator anim;
    public GameObject gunobj;
    public bool isMoving = false;
    public bool canPickUp = false;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        shoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		bool was = Display.display.win;
		Display.display.win = GameObject.FindGameObjectsWithTag("Enemy")?.Length < 1;
		if (!was && Display.display.win)
		{
			Camera.main.GetComponent<AudioSource>().PlayOneShot(win);
		}
		if (Display.display.dead == true)
			return;
        movement();
		//canPickUp = Input.GetKey(KeyCode.E);
    }

    void OnTriggerStay(Collider col)
    {
		if (Display.display.dead == true)
		{
			rb.velocity = Vector3.zero;
			return;
		}
        if (col.gameObject.tag == "Gun")
        if (Input.GetKeyDown(KeyCode.E))
        {
			if (gunobj)
            	gunobj.GetComponent<GunScript>().ammo = shoot.ammo;
            if (gunobj)
            {
                gunobj.transform.position = transform.position;
                gunobj.SetActive(true);

            }
            if (WeaponSlot.transform.childCount > 0)
                GameObject.Destroy(WeaponSlot.transform.GetChild(0).gameObject);
            var gun = col.GetComponent<GunScript>().attachWeapon;
            var gunInstance = GameObject.Instantiate(gun, WeaponSlot.transform);
            var stats = col.GetComponent<GunScript>();
            shoot.ammo = stats.ammo;
            shoot.fireRate = stats.rateOfFire;

            shoot.bulletPrefab = col.GetComponent<GunScript>().bulletPrefab;
            gunInstance.transform.parent = WeaponSlot.transform;

            gunobj = col.gameObject;
            gunobj.SetActive(false);
			source.PlayOneShot(pickup);
        }  
           
    }
    void movement()
    {
		rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        anim.SetBool("walking", rb.velocity.magnitude > 0.1);
    }

}
