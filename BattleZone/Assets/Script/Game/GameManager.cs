using System.Collections;
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

    public int _nbTeam = 5;
    public int _shipByTeam = 2;

    List<GameObject> _ships;

    public static GameManager Instance { get { return _instance; } }
    public ScriptableWeapon WeaponData { get { return _weaponData; } }
    public List<GameObject> Ships { get { return _ships; } }

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
        if (_shipPrefab != null)
        {
            for (int i = 0; i < _nbTeam; i++)
            {
                for (int j = 0; j < _shipByTeam; j++)
                {
                    GameObject ship = Instantiate(_shipPrefab) as GameObject;
                    ship.GetComponent<ShipManager>().Team = i;
                    ship.transform.position = new Vector3(Random.Range(0.0f, _mapSize.x), 3.0f, Random.Range(0.0f, _mapSize.y));
                    _ships.Add(ship);
                }
            }
        }
    }
}
