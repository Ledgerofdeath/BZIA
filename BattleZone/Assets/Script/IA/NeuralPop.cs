using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class NeuralPop {

    public int _taillePop;

    public List<NeuralNet> _neuralNetPop;

    public List<NeuralNet> NeuralNetPop { get { return _neuralNetPop; } }

	public NeuralPop( List<int> config, int taillePop )
	{
		_neuralNetPop = new List<NeuralNet>();
		_taillePop = taillePop;
		
	}
	
	public void InitPop(ContinuousUniform uni, list<int> config)
    {
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
	
	public List<NeuralNet> SelectBest () 
	{
		
	}
	
    public void MutatePop(List<NeuralNet> bestNN, Normal norm, float probaMutation)
    {
        foreach (NeuralNet n in bestNN)
        {
            float proba = UnityEngine.Random.Range(0, 1.0f);

            if (proba < probaMutation)
            {
                n.MutateBias(norm);
                n.MutateWeigh(norm);
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

            temp.AddRange(bestNN[i].Reproduce2(bestNN[j]));
        }

        NeuralNetPop = temp;
    }

}
