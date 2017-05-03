using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

public class NeuralLayer {

    Matrix<float> _Weight;
    Vector<float> _bias;

    public Matrix<float> Weight { get { return _Weight; } }
    public Vector<float> Bias { get { return _bias; } }

    public NeuralLayer(ContinuousUniform uni, int nbNeur1, int nbNeur2)
    {
        _Weight = Matrix<float>.Build.Random(nbNeur1, nbNeur2, uni);
        _bias = Vector<float>.Build.Random(nbNeur2, uni);
    }

    public Vector<float> CalcLayer(Vector<float> entree)
    {
        entree = SigmoidVect((_Weight).LeftMultiply(entree) + _bias);
        return entree;
    }

    Vector<float> SigmoidVect(Vector<float> z)
    {
        z.Negate(z);
        z.PointwiseExp(z);
        z.Add(1, z);
        z.PointwisePower(-1, z);

        return z;
    }
}
