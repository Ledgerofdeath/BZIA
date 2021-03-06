﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Vector2 _mapSize = new Vector2();

    [SerializeField]
    private ScriptableWeapon _weaponData;

    private List<WeaponData> _weaponList;
    static GameManager _instance;

    [SerializeField]
    GameObject _shipPrefab = null;

    [SerializeField]
    TerrainCollider _terrain;

    public int _nbTeam = 5;
    public int _shipByTeam = 2;

    List<GameObject> _ships;

    public static GameManager Instance { get { return _instance; } }
    public ScriptableWeapon WeaponData { get { return _weaponData; } }
    public List<GameObject> Ships { get { return _ships; } }
    public TerrainCollider Terrain { get { return _terrain; } }

    void Awake()
    {
        _instance = this;
        _weaponList = new List<WeaponData>();
        _ships = new List<GameObject>();

        foreach (WeaponData weapon in _weaponData.weapons)
        {
            _weaponList.Add(weapon);
        }
        ShipCreator();
    }

    public WeaponData FindWeaponByID(int id)
    {
        return _weaponList.Find(x => x.ID == id);
    }

    void ShipCreator()
    {
        TerrainData terrainInfo = _terrain.terrainData;
        float x, z;
        if (_shipPrefab != null)
        {
            for (int i = 0; i < _nbTeam; i++)
            {
                for (int j = 0; j < _shipByTeam; j++)
                {
                    GameObject ship = Instantiate(_shipPrefab) as GameObject;
                    ship.GetComponent<ShipManager>().Team = i;
                    x = Random.Range(0.0f, terrainInfo.size.x);
                    z = Random.Range(0.0f, terrainInfo.size.z);
                    ship.transform.position = new Vector3(x, terrainInfo.GetHeight((int)x, (int)z) + 3.0f, z);
                    _ships.Add(ship);
                }
            }
        }
    }
}
