using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    private ShipManager _shipManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _shipManager.ShipMove.UpdateMove();
        _shipManager.MissileManager.UpdateShoot();
    }
}
