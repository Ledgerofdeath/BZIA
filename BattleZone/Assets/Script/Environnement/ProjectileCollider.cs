using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour {
    public GameObject _explosion = null;
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosion, transform.position, _explosion.transform.rotation);
        Destroy(this.gameObject);
    }
}
