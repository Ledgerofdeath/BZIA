using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour {
    [SerializeField]
    int _weaponID = 0;

    void OnCollisionEnter(Collision collision)
    {
        WeaponData weapon = GameManager.Instance.FindWeaponByID(_weaponID);
        Instantiate(weapon.Explosion, transform.position, weapon.Explosion.transform.rotation);
        ShipManager ship = collision.gameObject.GetComponent<ShipManager>();
        if (ship != null)
            ship.GetDammage(GameManager.Instance.FindWeaponByID(_weaponID).Damage);
        Destroy(this.gameObject);
    }
}
