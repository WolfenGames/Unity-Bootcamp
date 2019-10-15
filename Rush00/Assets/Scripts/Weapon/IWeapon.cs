using UnityEngine;

public interface IWeapon {
    Rigidbody2D rb2d { get; }

    bool automatic { get; set; }

    bool infiniteAmmo { get; set; }

    float range { get; set; }

    Transform trans { get; }

    void Attack();
}