using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour {
    private GameObject _shootSpawn = null;

    List<int> _missileID;
    int _currentMissileID = 0;

    float _currentDelay = 0.0f;

    Transform _transform;

    public GameObject ShootSpawn { set { _shootSpawn = value; } }
    public Transform ShipTransform { set { _transform = value; } }

    public MissileManager()
    {
        _missileID = new List<int>();
    }

    //A modifier !!!
    public void UpdateShoot()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            if (_missileID.Count > 0 && _currentDelay > GameManager.Instance.FindWeaponByID(_missileID[_currentMissileID]).Delay)
            {
                Shoot();
                _currentDelay = 0.0f;
            }
        }
        _currentDelay += Time.deltaTime;
    }

    void Shoot()
    {
        if (_shootSpawn != null)
        {
            Quaternion r = _transform.rotation;
            GameObject p = Instantiate(GameManager.Instance.FindWeaponByID(_missileID[_currentMissileID]).MissileObject, _shootSpawn.transform.position, _shootSpawn.transform.rotation) as GameObject;
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * GameManager.Instance.FindWeaponByID(_missileID[_currentMissileID]).Speed, ForceMode.Impulse);
        }
    }

    public void AddMissile(int id)
    {
        _missileID.Add(id);
    }

}
