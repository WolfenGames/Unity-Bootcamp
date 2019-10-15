using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    AudioSource source;
    public GameObject gunPrefab;
    public GameObject fistPrefab;
	public AudioClip DryFire;
    
    PlayerMovement p;
    public Transform body;

    public float fireRate = 6.0f;
    float cooldown;
    public int ammo;
	List<GameObject>		enemies;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        p = GetComponent<PlayerMovement>();
        body = transform.GetChild(0);
        var throwGun = GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
		Display.display.ammo = ammo;
		if (Display.display.dead == true)
			return;
		enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        cooldown -= Time.deltaTime;
        shootBullets();
        throwWeapon();
    }

    public void shootBullets()
    {
		if (Input.GetMouseButtonDown(0) && ammo <= 0)
		{
			source.clip = DryFire;
			source.Play();
		}
        if (Input.GetMouseButton(0))
        {
            if(cooldown < 0.0f && ammo > 0)
            {
                if (!bulletPrefab)
                {
                    bulletPrefab = fistPrefab;
                    fireRate = 0.5f;
                }
                if (!bulletPrefab.GetComponent<Bullet>().isInfinite)
                    ammo--;
                source.clip = bulletPrefab.GetComponent<Bullet>().clip;
                source.Play();
                cooldown = fireRate;
                GameObject bulletObject = Instantiate(bulletPrefab);
				bulletObject.GetComponent<Bullet>().canPass = new string[]{"Player", "Gun"};
                bulletObject.transform.position = transform.position;
                bulletObject.transform.rotation = body.transform.rotation;
                // subtract ammo
            }
			foreach(GameObject go in enemies)
				go.GetComponent<EnemyAI>().oShit();
        }
    }

    void throwWeapon()
    {   
       
        
        if(Input.GetMouseButton(1))
        {
            if (p.gunobj)
            {
                GameObject bulletObject = Instantiate(gunPrefab);
				bulletObject.GetComponent<Bullet>().canPass = new string[]{"Player", "Floor", "Gun"};
                bulletObject.transform.position = transform.position;
                bulletObject.transform.rotation = body.transform.rotation;
                bulletObject.GetComponentInChildren<SpriteRenderer>().sprite = p.gunobj.GetComponent<SpriteRenderer>().sprite;
                GameObject.Destroy(p.gunobj);
                p.gunobj = null;
                if (p.WeaponSlot.transform.childCount > 0)
                    GameObject.Destroy(p.WeaponSlot.transform.GetChild(0).gameObject);
                bulletPrefab = null;
            }
            //GameObject.Destroy(WeaponSlot.transform.GetChild(0).gameObject);
        }
    }
}
