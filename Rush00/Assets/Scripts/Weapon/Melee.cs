using UnityEngine;

public class Melee : MonoBehaviour, IWeapon
{
    public bool automatic { get; set; }
    public float range { get; set; }
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

    public void Attack() {
        // melee attack logic
    }
}
