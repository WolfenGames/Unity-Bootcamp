using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform equipPosition;

    private IWeapon weapon;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (weapon != null) {
            bool attack;
            if (weapon.automatic)
                attack = Input.GetKey("Attack");
            else
                attack = Input.GetKeyDown("Attack");
            bool throwWeapon = Input.GetKeyDown("Throw");

            if (attack)
                weapon.Attack();
            if (throwWeapon)
                Throw(-transform.up * 1000);
        }
    }

    public void Throw(Vector3 throwVelocity)
    {
        weapon.trans.position = transform.position;
        weapon.trans.parent = null;
        weapon.rb2d.GetComponent<BoxCollider2D>().enabled = true;
        weapon.rb2d.AddForce(throwVelocity);
        weapon.infiniteAmmo = true;
        weapon = null;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        bool pickup = Input.GetKeyDown("Pickup");

        IWeapon newWeapon = collider.gameObject.GetComponent<IWeapon>();

        if (pickup) {
            if (weapon != null) {
                Throw(new Vector3(0, 0, 0));
            }

            weapon = newWeapon;
            weapon.trans.parent = equipPosition;
            weapon.trans.localPosition = new Vector3(0, 0, 0);
            weapon.trans.rotation = transform.rotation;
            weapon.rb2d.GetComponent<BoxCollider2D>().enabled = false;
            weapon.rb2d.velocity = new Vector3(0, 0, 0);
            weapon.infiniteAmmo = false;
        }
    }
}
