using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class TrainIA : MonoBehaviour {

    public struct s_neural {
       public Matrix<double> W;
       public Vector<double> b;
       public int score;

    }

    Normal _normalLaw;

    ContinuousUniform _uniformLaw;

    public int _probaMutation;

    public int _taillePop;

    public List<int> _config;

    public List<List<s_neural>> _neuralNetPop;

    public int ProbaMutation { get { return _probaMutation; } set { _probaMutation = value; } }

    public int TaillePop { get { return _taillePop; } set { _taillePop = value; } }

    public List<int> Config { get { return _config; } set { _config = value; } }

    public List<List<s_neural>> NeuralNetPop { get { return _neuralNetPop; } set { _neuralNetPop = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void SigmoidVect(Vector<double> z)
    {
        z.Negate(z);
        z.PointwiseExp(z);
        z.Add(1, z);
        z.PointwisePower(-1, z);
    }

    public void InitLayer(s_neural neur, int nbNeur1, int nbNeur2)
    {
        neur.W = Matrix<double>.Build.Random(nbNeur1, nbNeur2, _uniformLaw);
        neur.b = Vector<double>.Build.Random(nbNeur2, 0);
        neur.score = 0;
    }

    public void InitNet(List<s_neural> neuralNet)
    {
        int i = 0;
        while (i < (_config.Count-1))
        {
            InitLayer(neuralNet[i], _config[i], _config[i + 1]);
        }
    }

    public void InitPop()
    {
        int i = 0;
        while (i < _taillePop)
        {
            InitNet(_neuralNetPop[i]);
        }
        

    }

    public void Mutate(List<s_neural> neuralNet)
    {
        

        int c = Random.Range(0, _config.Count);
        int i = Random.Range(0, ((neuralNet[c].W).RowCount + 1));
        int j = Random.Range(0, ((neuralNet[c].W).ColumnCount + 1));

        (neuralNet[c].W)[i,j] = (neuralNet[c].W)[i, j] + _normalLaw.Sample();

    }

    public List<List<s_neural>> Reproduce1(List<s_neural> neuralNet1, List<s_neural> neuralNet2)
    {

        
        List<s_neural> child1 = neuralNet1;
        List<s_neural> child2 = neuralNet2;

        int c = Random.Range(0, _config.Count);
        int i = Random.Range(1, ((neuralNet1[c].W).RowCount + 1));
        int j = Random.Range(1, ((neuralNet1[c].W).ColumnCount + 1));

        double aux = (child1[c].W)[i, j];
        (child1[c].W)[i, j] = (child2[c].W)[i, j];
        (child2[c].W)[i, j] = aux;

        var children = new List<List<s_neural>>
        {
            child1,
            child2
        };

        return children;

    }

    public List<List<s_neural>> Reproduce2(List<s_neural> neuralNet1, List<s_neural> neuralNet2)
    {


        List<s_neural> child1 = neuralNet1;
        List<s_neural> child2 = neuralNet2;

        int c = Random.Range(1, _config.Count);
        int i = Random.Range(1, (_config[c]+1));
        
        Vector<double> aux1 = (child1[c].W).Row(i);
        (child1[c].W).SetRow(i, (child2[c].W).Row(i));
        (child2[c].W).SetRow(i, aux1);

        Vector<double> aux2 = (child1[c-1].W).Column(i);
        (child1[c-1].W).SetColumn(i, (child2[c-1].W)Column(i));
        (child2[c-1].W).SetColumn(i, aux2);

        double aux3 = (child1[c].b)[i-1];
        (child1[c].b)[i-1] = (child2[c].b)[i-1];
        (child2[c].b)[i-1] = aux3;

        var children = new List<List<s_neural>>
        {
            child1,
            child2
        };

        return children;

    }


    public void CalcLayer(Vector<double> entree, s_neural neur )
    {
        entree = (neur.W).LeftMultiply(entree) + neur.b;
        SigmoidVect(entree);

    }

    public void CalcNet(Vector<double> entree, List<s_neural> neuralNet)
    {
        foreach (s_neural e in neuralNet)
        {
          CalcLayer(entree, e);
        }
    }


}
