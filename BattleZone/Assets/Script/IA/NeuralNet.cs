using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

[System.Serializable]
public class NeuralNet {

    List<NeuralLayer> _layerNet;
	List<int> _config;
    int _score;

    public List<NeuralLayer> LayerNet { get { return _layerNet; } }
	
	public List<int> Config{ get { return _config; } }

    public int Score { get { return _score; } set { _score = value; } }

    public NeuralNet(ContinuousUniform uni, List<int> config)
    {
        _layerNet = new List<NeuralLayer>();
		_config = config;
        int i = 0;
        while (i < (_config.Count - 1))
        {
            _layerNet.Add(new NeuralLayer(uni, _config[i], _config[i + 1]));
            i++;
        }

        _score = 0;
    }

    public void CalcNet(Vector<float> entree)
    {
		int i = 0;
		
		while (i<(_layerNet.Count-1))
		{
			entree=_layerNet[i++].CalcLayer(entree);
		}
		
		entree=_layerNet[i].CalcLastLayer(entree);
  
    }

    public void MutateBias(Normal norm)
    {

        int c = UnityEngine.Random.Range(0, (_config.Count-1));
        int i = UnityEngine.Random.Range(0, _config[c]);

        Debug.Log("cM1:" + c);
        Debug.Log("iM1:" + i);

        (_layerNet[c].Bias)[i] = (_layerNet[c].Bias)[i] +(float) norm.Sample();

    }

    public void MutateWeigh(Normal norm)
    {

        int c = UnityEngine.Random.Range(0, (_config.Count-1));
        int i = UnityEngine.Random.Range(0, _config[c]);
        int j = UnityEngine.Random.Range(0, _config[c + 1]);

        Debug.Log("cM2:" + c);
        Debug.Log("iM2:" + i);
        Debug.Log("jM1:" + j);
       

        (_layerNet[c].Weight)[i, j] = (_layerNet[c].Weight)[i, j] +(float) norm.Sample();

    }

    public List<NeuralNet> Reproduce1( NeuralNet neuralNet2)
    {


        NeuralNet child1 = this;
        NeuralNet child2 = neuralNet2;

        child1.Score = 0;
        child2.Score = 0;

        int c = UnityEngine.Random.Range(0, (_config.Count-1));
        int i = UnityEngine.Random.Range(0, _config[c]);
        int j = UnityEngine.Random.Range(0, _config[c + 1]);
        int k = UnityEngine.Random.Range(0, _config[c]);

        Debug.Log("cR1:" + c);
        Debug.Log("iR1:" + i);
        Debug.Log("jR1:" + j);
        Debug.Log("kR1:" + k);
      

        float aux1 = (child1.LayerNet[c].Weight)[i, j];
        (child1.LayerNet[c].Weight)[i, j] = (child2.LayerNet[c].Weight)[i, j];
        (child2.LayerNet[c].Weight)[i, j] = aux1;

        float aux2 = (child1.LayerNet[c].Bias)[k];
        (child1.LayerNet[c].Bias)[k] = (child2.LayerNet[c].Bias)[k];
        (child2.LayerNet[c].Bias)[k] = aux2;


        var children = new List<NeuralNet>
        {
            child1,
            child2
        };

        return children;

    }

    public List<NeuralNet> Reproduce2(NeuralNet neuralNet2)
    {


        NeuralNet child1 = this;
        NeuralNet child2 = neuralNet2;

        child1.Score = 0;
        child2.Score = 0;


        int c = UnityEngine.Random.Range(0, (_config.Count-1));
        int i = UnityEngine.Random.Range(0, _config[c]);

        Debug.Log("cR2:" + c);
        Debug.Log("iR2:" + i);

        if (c == 0)
        {
            Vector<float> aux1 = (child1.LayerNet[c].Weight).Row(i);
            (child1.LayerNet[c].Weight).SetRow(i, (child2.LayerNet[c].Weight).Row(i));
            (child2.LayerNet[c].Weight).SetRow(i, aux1);
        }

        else if (c == (_config.Count - 2))
        {
            Vector<float> aux2 = (child1.LayerNet[c - 1].Weight).Column(i);
            (child1.LayerNet[c - 1].Weight).SetColumn(i, (child2.LayerNet[c - 1].Weight).Column(i));
            (child2.LayerNet[c - 1].Weight).SetColumn(i, aux2);

            float aux3 = (child1.LayerNet[c - 1].Bias)[i];
            (child1.LayerNet[c - 1].Bias)[i] = (child2.LayerNet[c - 1].Bias)[i];
            (child2.LayerNet[c - 1].Bias)[i] = aux3;
        }

        else
        {
            Vector<float> aux4 = (child1.LayerNet[c].Weight).Row(i);
            (child1.LayerNet[c].Weight).SetRow(i, (child2.LayerNet[c].Weight).Row(i));
            (child2.LayerNet[c].Weight).SetRow(i, aux4);

            Vector<float> aux5 = (child1.LayerNet[c - 1].Weight).Column(i);
            (child1.LayerNet[c - 1].Weight).SetColumn(i, (child2.LayerNet[c - 1].Weight).Column(i));
            (child2.LayerNet[c - 1].Weight).SetColumn(i, aux5);

            float aux6 = (child1.LayerNet[c - 1].Bias)[i];
            (child1.LayerNet[c - 1].Bias)[i] = (child2.LayerNet[c - 1].Bias)[i];
            (child2.LayerNet[c - 1].Bias)[i] = aux6;
        }

        var children = new List<NeuralNet>
        {
            child1,
            child2
        };

        return children;

    }

}
