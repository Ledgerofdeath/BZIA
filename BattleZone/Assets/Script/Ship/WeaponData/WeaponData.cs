using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : System.Object {
    public GameObject _explosion = null;
    public GameObject _missile = null;

    [SerializeField]
    private int _cost = 0;
    [SerializeField]
    float _delay = 0.0f;
    [SerializeField]
    float _speed = 0.0f;
    [SerializeField]
    int _id = 0;
    [SerializeField]
    int _damage = 0;


    public int Cost { get { return _cost; } }
    public float Delay { get { return _delay; } }
    public float Speed { get { return _speed; } }
    public GameObject MissileObject { get { return _missile; } }
    public GameObject Explosion { get { return _explosion; } }
    public int ID { get { return _id; } }
    public int Damage { get { return _damage; } }
}
