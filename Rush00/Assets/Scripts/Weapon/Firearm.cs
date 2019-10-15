using UnityEngine;

public class Firearm : MonoBehaviour, IWeapon
{
    public bool automatic { get; set; }
    public bool automaticFireRate => automatic;
    public float fireRate;
    public int ammoCapacity;
    public GameObject projectile;
    public Transform projectileSpawn;
    public float range { get; set; }
    public float firearmRange;
    public bool infiniteAmmo { get; set; }
    public Rigidbody2D rb2d {
        get {
            return GetComponent<Rigidbody2D>();
        }
    }
    public Transform trans {
        get {
            return GetComponent<Transform>();
        }
    }

    private float timer;

    void Start() {
        timer = fireRate;
        range = firearmRange;
        infiniteAmmo = true;
    }

    public void Attack() {
        if (infiniteAmmo || ammoCapacity > 0) {
            if (automatic)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    //Store the player who shot the bullet in the class, that way the bullet we shot wont kill us and get destroyed.
                    GameObject projectileObj = GameObject.Instantiate(projectile, projectileSpawn.position + new Vector3(0, 0.6f, 0), transform.rotation);
                    projectileObj.GetComponent<Projectile>().characterFiredThis = this.gameObject;
                    timer = fireRate;
                }
            }
            else {
                //Store the player who shot the bullet in the class, that way the bullet we shot wont kill us and get destroyed.
                GameObject projectileObj = GameObject.Instantiate(projectile, projectileSpawn.position + new Vector3(0, 0.6f, 0), transform.rotation);
                projectileObj.GetComponent<Projectile>().characterFiredThis = this.gameObject;
            }
            if (!infiniteAmmo)
                ammoCapacity--;
        }
    }
}
