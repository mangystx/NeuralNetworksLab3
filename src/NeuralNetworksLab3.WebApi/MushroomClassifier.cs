namespace NeuralNetworksLab3.WebApi;

public class MushroomClassifier
{
    private readonly double[] _weights;
    private double _bias;
    private const double LearningRate = 0.1;

    public MushroomClassifier()
    {
        _weights = new double[5];
        _bias = 0.5;
        
        var rnd = new Random();
        for (var i = 0; i < _weights.Length; i++)
        {
            _weights[i] = rnd.NextDouble();
        }
        
        var trainingInputs = new int[][]
        {
            [0, 0, 0, 0, 1],
            [1, 1, 1, 1, 0],
            [1, 0, 1, 1, 0], 
            [0, 1, 1, 0, 0],
            [0, 1, 0, 1, 0],
            [0, 1, 1, 0, 0],
            [0, 1, 1, 0, 1],
            [1, 1, 1, 0, 0],
            [0, 0, 0, 0, 0],
            [1, 1, 1, 1, 1],
            [0, 1, 0, 0, 0],
            [0, 1, 1, 1, 0]
        };
        
        var trainingOutputs = new[] { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 }; // 1 - отруйний, 0 - їстівний
        
        Train(trainingInputs, trainingOutputs, 3000);
    }
    
    public int Predict(int[] inputs)
    {
        var sum = inputs.Select((t, i) => t * _weights[i]).Sum() + _bias;
        return sum >= 0 ? 1 : 0;
    }
    
    private void Train(int[][] inputs, int[] outputs, int epochs)
    {
        for (var epoch = 0; epoch < epochs; epoch++)
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                var predicted = Predict(inputs[i]);
                var error = outputs[i] - predicted;
                
                for (var j = 0; j < _weights.Length; j++)
                {
                    _weights[j] += LearningRate * error * inputs[i][j];
                }

                _bias += LearningRate * error;
            }
        }
    }
}
