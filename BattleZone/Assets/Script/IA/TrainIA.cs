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

    public List<NeuralNet> _neuralNetPop = new List<NeuralNet>();

    public List<int> Config { get { return _config; } set { _config = value; } }

    public List<NeuralNet> NeuralNetPop { get { return _neuralNetPop; } set { _neuralNetPop = value; } }

    // Use this for initialization
    void Start () {
        _uniformLaw = new ContinuousUniform(_range.x, _range.y);
        _normalLaw = new Normal(_paramNorm.x, _paramNorm.y);

        InitPop();

        Vector<float> entree = Vector<float>.Build.Dense(4);

        foreach (NeuralNet ln in _neuralNetPop)
        {

            ln.MutateBias(_normalLaw, _config);
            ln.MutateWeigh(_normalLaw, _config);
            List<NeuralNet> test = ln.Reproduce1(ln, _config);
            List<NeuralNet> test2 = ln.Reproduce2(ln, _config);
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitPop()
    {
        int i = 0;
        while (i < _taillePop)
        {
            
            _neuralNetPop.Add(new NeuralNet(_uniformLaw,_config));
            i++;
        }
    }

    public void MutatePop(List<NeuralNet> bestNN)
    {
        foreach (NeuralNet n in bestNN)
        {
            float proba = UnityEngine.Random.Range(0, 1.0f);

            if (proba < _probaMutation)
            {
                n.MutateBias(_normalLaw, _config);
                n.MutateWeigh(_normalLaw, _config);
            }
        }
    }

    public void ReproducePop (List<NeuralNet> bestNN )
    {
        int i;
        int j;
        List<NeuralNet> temp = new List<NeuralNet>();

        while (temp.Count < _taillePop)
        {
             i= UnityEngine.Random.Range(0, bestNN.Count);
          
            do
            {
                 j= UnityEngine.Random.Range(0, bestNN.Count);

            } while (i == j);

            temp.AddRange(bestNN[i].Reproduce2(bestNN[j], _config));
        }

        NeuralNetPop = temp;
    }

}
