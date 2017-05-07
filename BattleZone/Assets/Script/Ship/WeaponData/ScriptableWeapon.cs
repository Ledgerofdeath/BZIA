using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/Missile", order = 1)]
public class ScriptableWeapon : ScriptableObject {
    public List<WeaponData> weapons = null;
}
