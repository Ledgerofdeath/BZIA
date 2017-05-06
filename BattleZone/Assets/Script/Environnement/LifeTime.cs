using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public float _time = 0.0f;

    void LateUpdate()
    {
        if (_time < 0.0f)
            Destroy(gameObject);
        _time -= Time.deltaTime;
    }
}
