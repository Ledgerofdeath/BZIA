using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour {

    public GameObject _projectile = null;
    public GameObject _shootSpawn = null;
    public GameObject _shipUp = null;

    public float _delay = 0.0f;
    float _currentDelay = 0.0f;

    public float _speed = 0.0f;

    public GameObject Projectile { set { _projectile = value; } }

    private void Update()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            if (_currentDelay > _delay)
            {
                Shoot();
                _currentDelay = 0.0f;
            }
        }
        _currentDelay += Time.deltaTime;
    }

    void Shoot()
    {
        if (_projectile != null && _shootSpawn != null)
        {
            Quaternion r = transform.rotation;
            GameObject p = Instantiate(_projectile, _shootSpawn.transform.position, _shootSpawn.transform.rotation) as GameObject;
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * _speed, ForceMode.Impulse);
        }
    }
}
