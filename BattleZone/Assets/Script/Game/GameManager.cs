using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private ScriptableWeapon _weaponData;

    private List<WeaponData> _weaponList;
    static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    public ScriptableWeapon WeaponData { get { return _weaponData; } }

    void Awake()
    {
        _instance = this;
        _weaponList = new List<WeaponData>();

        foreach (WeaponData weapon in _weaponData.weapons)
        {
            _weaponList.Add(weapon);
        }
    }

    public WeaponData FindWeaponByID(int id)
    {
        return _weaponList.Find(x => x.ID == id);
    }

}
