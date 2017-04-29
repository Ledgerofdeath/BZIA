using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class NeuralLayer {

    Matrix<double> _Weight;
    Vector<double> _bias;

    public Matrix<double> Weight { get { return _Weight; } }
    public Vector<double> Bias { get { return _bias; } }

    public NeuralLayer(ContinuousUniform uni, int nbNeur1, int nbNeur2)
    {
        _Weight = Matrix<double>.Build.Random(nbNeur1, nbNeur2, uni);
        _bias = Vector<double>.Build.Random(nbNeur2, uni);
    }

    public void InitLayer(ContinuousUniform uni, int nbNeur1, int nbNeur2)
    {
        _Weight = Matrix<double>.Build.Random(nbNeur1, nbNeur2, uni);
        _bias = Vector<double>.Build.Random(nbNeur2, uni);
    }

    public void CalcLayer(Vector<double> entree)
    {
        entree = (_Weight).LeftMultiply(entree) + _bias;
        SigmoidVect(entree);
    }

    void SigmoidVect(Vector<double> z)
    {
        z.Negate(z);
        z.PointwiseExp(z);
        z.Add(1, z);
        z.PointwisePower(-1, z);
    }
}
