using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManager : MonoBehaviour {

    IAVision _vision;
    NeuralNet _neuralNet;

	// Use this for initialization
	void Awake () {
        _vision = new IAVision();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitNeuralNet()
    {

    }

}
