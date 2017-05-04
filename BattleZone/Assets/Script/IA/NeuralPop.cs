using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class NeuralPop {

    public int _taillePop;

    public List<int> _config;

    public List<NeuralNet> _neuralNetPop;

    public List<NeuralNet> NeuralNetPop { get { return _neuralNetPop; } }

	public NeuralPop( List<int> config, int taillePop )
	{
		_neuralNetPop = new List<NeuralNet>();
		_config = config;
		_taillePop = taillePop;
		
	}
	
	public void LoadPop()
	{
		
	}
	
    public void InitPop(ContinuousUniform uni)
    {
        int i = 0;
        while (i < _taillePop)
        {
            
            _neuralNetPop.Add(new NeuralNet(uni,_config));
            i++;
        }
    }

    public void MutatePop(List<NeuralNet> bestNN, Normal norm, float probaMutation)
    {
        foreach (NeuralNet n in bestNN)
        {
            float proba = UnityEngine.Random.Range(0, 1.0f);

            if (proba < probaMutation)
            {
                n.MutateBias(norm, _config);
                n.MutateWeigh(norm, _config);
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
