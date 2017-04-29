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

    public double _probaMutation;

    public int _taillePop;

    public Vector2 _range = Vector2.zero;

    public List<int> _config;

    public List<List<NeuralLayer>> _neuralNetPop = new List<List<NeuralLayer>>();

    List<NeuralLayer> _currentNetPop;

    public double ProbaMutation { get { return _probaMutation; } set { _probaMutation = value; } }

    public int TaillePop { get { return _taillePop; } set { _taillePop = value; } }

    public List<int> Config { get { return _config; } set { _config = value; } }

    public List<List<NeuralLayer>> NeuralNetPop { get { return _neuralNetPop; } set { _neuralNetPop = value; } }

    // Use this for initialization
    void Start () {
        _uniformLaw = new ContinuousUniform(_range.x, _range.y);
        InitPop();

        /*foreach (List<NeuralLayer> ln in _neuralNetPop)
        {
            foreach (NeuralLayer n in ln)
            {
                for (int i = 0; i < n.Weight.RowCount; i++)
                {
                    for (int j = 0; j < n.Weight.ColumnCount; j++)
                    {
                        //sDebug.Log("Point W: " + n.Weight[i, j]);
                    }
                }
                Debug.Log("count : " + n.Bias.Count);
                for (int i = 0; i < n.Bias.Count; i++)
                {
                   // Debug.Log("Point b:" + n.Bias[i]);
                }
            }
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitNet()
    {
        int i = 0;
        while (i < (_config.Count-1))
        {
            _currentNetPop.Add(new NeuralLayer(_uniformLaw, _config[i], _config[i + 1]));
            i++;
        }
    }

    public void InitPop()
    {
        int i = 0;
        while (i < _taillePop)
        {
            _currentNetPop = new List<NeuralLayer>();
            InitNet();
            _neuralNetPop.Add(_currentNetPop);
            i++;
        }
    }

    public void Mutate(List<s_neural> neuralNet)
    {
        

        int c = UnityEngine.Random.Range(0, (_config.Count-1));
        int i = UnityEngine.Random.Range(0, _config[c]);
        int j = UnityEngine.Random.Range(0, _config[c+1]);

        (neuralNet[c].W)[i,j] = (neuralNet[c].W)[i, j] + _normalLaw.Sample();

    }

    public List<List<s_neural>> Reproduce1(List<s_neural> neuralNet1, List<s_neural> neuralNet2)
    {

        
        List<s_neural> child1 = neuralNet1;
        List<s_neural> child2 = neuralNet2;

        int c = UnityEngine.Random.Range(0, _config.Count);
        int i = UnityEngine.Random.Range(1, _config[c]);
        int j = UnityEngine.Random.Range(1, _config[c+1]);
        int k = UnityEngine.Random.Range(1, _config[c]);

        double aux1 = (child1[c].W)[i, j];
        (child1[c].W)[i, j] = (child2[c].W)[i, j];
        (child2[c].W)[i, j] = aux1;

        double aux2 = (child1[c].b)[k];
        (child1[c].b)[k]= (child2[c].b)[k];
        (child2[c].b)[k] = aux2;


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

        int c = UnityEngine.Random.Range(0, (_config.Count+1));
        int i = UnityEngine.Random.Range(1, (_config[c]+1));

        if ( c==0 )
        {
            Vector<double> aux1 = (child1[c].W).Row(i);
            (child1[c].W).SetRow(i, (child2[c].W).Row(i));
            (child2[c].W).SetRow(i, aux1);
        }

        if (c == (_config.Count))
        {
            Vector<double> aux2 = (child1[c - 1].W).Column(i);
            (child1[c - 1].W).SetColumn(i, (child2[c - 1].W).Column(i));
            (child2[c - 1].W).SetColumn(i, aux2);

            double aux3 = (child1[c-1].b)[i];
            (child1[c-1].b)[i] = (child2[c-1].b)[i];
            (child2[c-1].b)[i] = aux3;
        }


        Vector<double> aux4 = (child1[c].W).Row(i);
        (child1[c].W).SetRow(i, (child2[c].W).Row(i));
        (child2[c].W).SetRow(i, aux4);

        Vector<double> aux5 = (child1[c-1].W).Column(i);
        (child1[c-1].W).SetColumn(i, (child2[c-1].W).Column(i));
        (child2[c-1].W).SetColumn(i, aux5);

        double aux6 = (child1[c-1].b)[i];
        (child1[c-1].b)[i] = (child2[c-1].b)[i];
        (child2[c-1].b)[i] = aux6;

        var children = new List<List<s_neural>>
        {
            child1,
            child2
        };

        return children;

    }

    public void CalcNet(Vector<double> entree, List<NeuralLayer> neuralNet)
    {
        foreach (NeuralLayer e in neuralNet)
        {
          e.CalcLayer(entree);
        }
    }


}
