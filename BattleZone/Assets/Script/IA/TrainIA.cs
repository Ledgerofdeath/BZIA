using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public class TrainIA : MonoBehaviour {

    public struct s_neural {
       public Matrix<float> W;
       public Vector<float> a;
       public Vector<float> b;

    }

    public List<int> _config;

    public List<List<s_neural>> _neuralNetPop;

    public List<List<s_neural>> NeuralNetPop { get { return _neuralNetPop; } set { _neuralNetPop = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public double Sigmoid(double z)
    {
        return (1/(1+Math.Exp(z)));
    }

    public void SigmoidVect(Vector<float> z)
    {
        int i = 0;
        while (i < z.Count)
        {
            z[i++]= (float) (1 / (1 + Math.Exp(z[i])));
        }
    }

    public void InitLayer(s_neural neur)
    {

    }

    public void InitNet(List<s_neural> neuralNet)
    {

    }

    public void CalcLayer(Vector<float> entree, s_neural neur )
    {
        entree = (neur.W).LeftMultiply(entree) + neur.b;
        SigmoidVect(entree);

    }

    public void CalcNet(Vector<float> entree, List<s_neural> neuralNet)
    {
        foreach (s_neural e in neuralNet)
        {
          CalcLayer(entree, e);
        }
    }


}
