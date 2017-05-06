using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class TrainIA : MonoBehaviour {

    Normal _normalLaw;

    ContinuousUniform _uniformLaw;

    public float _probaMutation;

    public int _taillePop;

    public Vector2 _range = Vector2.zero;

    public Vector2 _paramNorm = Vector2.zero;

    public List<int> _config;

    
    // Use this for initialization
    void Start () {
		
        _uniformLaw = new ContinuousUniform(_range.x, _range.y);
        _normalLaw = new Normal(_paramNorm.x, _paramNorm.y);

		NeuralPop neuralPop = new NeuralPop(_config,_taillePop);
        neuralPop.InitPop(_uniformLaw,_config);
		Vector<float> entree = Vector<float>.Build.Dense(4);
		foreach (NeuralNet ln in neuralPop.NeuralNetPop)
        {
            
            ln.CalcNet(entree);
            foreach (NeuralLayer n in ln.LayerNet)

              
            {
                for (int i = 0; i < n.Weight.RowCount; i++)
                {
                    for (int j = 0; j < n.Weight.ColumnCount; j++)
                    {
                        Debug.Log("Point W: " + n.Weight[i, j]);
                    }
                }
                Debug.Log("count : " + n.Bias.Count);
                for (int i = 0; i < n.Bias.Count; i++)
                {
                   Debug.Log("Point b:" + n.Bias[i]);
                }
            }
        }
		neuralPop.SavePop("IA");
		neuralPop.LoadPop("IA");
		neuralPop.ReproducePop(10);
		neuralPop.MutatePop(_normalLaw,_probaMutation);
		

        

		
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   

}
