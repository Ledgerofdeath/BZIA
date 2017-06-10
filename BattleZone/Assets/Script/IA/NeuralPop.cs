using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class NeuralPop {


    public int _taillePop;

    public List<NeuralNet> _neuralNetPop;

    public List<NeuralNet> NeuralNetPop { get { return _neuralNetPop; } }

	public NeuralPop()
	{
		_neuralNetPop = new List<NeuralNet>();
		
	}
	
	public void InitPop(ContinuousUniform uni, List<int> config, int taillePop)
    {
        _taillePop = taillePop;
        int i = 0;
        while (i < _taillePop)
        {
            
            _neuralNetPop.Add(new NeuralNet(uni, config));
            i++;
        }
    }

	
	public void SavePop( string filename)
	{
		FileStream fs = new FileStream(filename + ".dat", FileMode.Create);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(fs, _neuralNetPop);
		fs.Close();

	}
	
	public void LoadPop(string filename)
	{
		using (Stream stream = File.Open(filename + ".dat", FileMode.Open))
         {
             var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
 
             _neuralNetPop = (List<NeuralNet>)bformatter.Deserialize(stream);
         }
		 _taillePop = _neuralNetPop.Count;
	}
	
	public List<NeuralNet> SelectBest (int numSel) 
	{
		_neuralNetPop.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));
		List<NeuralNet> res = _neuralNetPop.GetRange(0,numSel);
		return res;
	}
	
    public void MutatePop( Normal norm, float probaMutation, float probaSelectMut )
    {
        foreach (NeuralNet n in _neuralNetPop)
        {
            float proba = UnityEngine.Random.Range(0, 1.0f);

            if (proba < probaMutation)
            {
                float selectMut = UnityEngine.Random.Range(0f, 1.0f);
                if (selectMut < probaSelectMut)
                {
                    n.MutateBias1(norm);
                    n.MutateWeigh1(norm);
                }
                else
                {                 
                    n.MutateBias2();
                    n.MutateWeigh2();  
                }
               
            }
        }
    }

    public void ReproducePop (int numSel, float probaSelectRep)
    {
        int i;
        int j;
        List<NeuralNet> temp = new List<NeuralNet>();
        List<NeuralNet> bestNN = this.SelectBest(numSel);
        float selectRep = UnityEngine.Random.Range(0f, 1.0f);

        while (temp.Count < _taillePop)
        {
             i= UnityEngine.Random.Range(0, bestNN.Count);
          
            do
            {
                 j= UnityEngine.Random.Range(0, bestNN.Count);

            } while (i == j);

            if (selectRep < probaSelectRep)
            {
                temp.AddRange(bestNN[i].Reproduce1(bestNN[j]));
            }
            else
            {
                temp.AddRange(bestNN[i].Reproduce2(bestNN[j]));
            }
            
        }

        _neuralNetPop = temp;
    }

}
