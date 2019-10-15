using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeSpan;

    private float timer;
    private Rigidbody rb;
    [System.NonSerialized]
    public GameObject characterFiredThis; //Keep track of who fired this projectile, this way we can prevent it from being destroyed when it touches us.... or doing any damage to ourselves

    void Start()
    {
        timer = lifeSpan;
        rb = GetComponent<Rigidbody>();
        //Use delta time to make it frame dependant and less jump/stuttery when games have low fps.
        rb.velocity = new Vector3(transform.forward.x, 0, transform.forward.z) * speed * Time.deltaTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Don't destory the projectile it it hits the person who spawned it.
        //Don't destory the projectile if it hist the Weapon Manager
        if (collider.gameObject != characterFiredThis && collider.GetComponent<WeaponManager>() == null)
            GameObject.Destroy(gameObject);
    }
}
