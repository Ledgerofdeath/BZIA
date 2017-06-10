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

    public float _probaSelectMut;

    public float _probaSelectRep;

    public int _taillePop;

    public Vector2 _range = Vector2.zero;

    public Vector2 _paramNorm = Vector2.zero;

    public List<int> _config;

    
    // Use this for initialization
    void Start () {
		
        _uniformLaw = new ContinuousUniform(_range.x, _range.y);
        _normalLaw = new Normal(_paramNorm.x, _paramNorm.y);
        Vector<float> entree = Vector<float>.Build.Dense(4);



        NeuralPop neuralPop = new NeuralPop();
        neuralPop.InitPop(_uniformLaw,_config, _taillePop);
		
        neuralPop.SavePop("IA");
        foreach (NeuralNet ln in neuralPop.NeuralNetPop)
        {
            
            ln.CalcNet(entree);
            foreach (NeuralLayer n in ln.LayerNet)

              
            {
               
                   Debug.Log("W: " + n.Weight.ToString());
                    
               
                   Debug.Log("b:" + n.Bias.ToString());

                

            }

            

        }
        NeuralPop neuralPop2 = new NeuralPop();
        neuralPop2.LoadPop("IA");
        foreach (NeuralNet ln in neuralPop2.NeuralNetPop)
        {

            ln.CalcNet(entree);
            foreach (NeuralLayer n in ln.LayerNet)


            {

                Debug.Log("W: " + n.Weight.ToString());


                Debug.Log("b:" + n.Bias.ToString());

                

            }

          
        }
       
        neuralPop.ReproducePop(2,_probaSelectRep);
		neuralPop.MutatePop(_normalLaw,_probaMutation,_probaSelectMut);
        foreach (NeuralNet ln in neuralPop.NeuralNetPop)
        {

            ln.CalcNet(entree);
            foreach (NeuralLayer n in ln.LayerNet)


            {

                Debug.Log("W: " + n.Weight.ToString());


                Debug.Log("b:" + n.Bias.ToString());

                

            }

           

        }





    }
	
	// Update is called once per frame
	void Update () {
		
	}

   

}
