using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {
    [SerializeField]
    private int _maxArmor = 2800;
    [SerializeField]
    private int _maxAmmo = 1500;

    private int _armor;
    private int _ammo;

    [SerializeField]
    private GameObject _shootSpawn = null;
    private MissileManager _missileManager;


    [SerializeField]
    private ShipMove _shipMove = null;

    public int Armor { get { return _armor; } }
    public int Ammo { get { return _ammo; } }

    public int MaxArmor { get { return _maxArmor; } set { _maxArmor = value; } }
    public int MaxAmmo { get { return _maxAmmo; } set { _maxAmmo = value; } }

    public MissileManager MissileManager { get { return _missileManager; } }
    public ShipMove ShipMove { get { return _shipMove; } }

    void Start()
    {
        _missileManager = new MissileManager();
        _missileManager.ShootSpawn = _shootSpawn;
        _missileManager.ShipTransform = transform;
        _missileManager.AddMissile(1);
        _shipMove.StartMove();
        _ammo = _maxAmmo;
        _armor = _maxArmor;
    }

    void Update()
    {
        //_shipMove.UpdateMove();
        //_missileManager.UpdateShoot();
    }

    public void GetDammage(int dmg)
    {
        _armor -= dmg;
        if (_armor <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public bool UseAmmo(int ammo)
    {
        if (_ammo >= ammo)
        {
            _ammo -= ammo;
            return true;
        }
        return false;
    }
}
