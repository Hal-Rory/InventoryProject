using System.Collections.Generic;
/// <summary>
/// Takes a list of weighted objects and returns them at random using those weights
/// </summary>
public class WeightMap
{
    private List<IWeighted> _weights;
    private int _weightTotal;

    public WeightMap(IEnumerable<IWeighted> weights, params IWeighted[] additionalWeights)
    {
        _weights = new List<IWeighted>(weights);
        _weights.AddRange(additionalWeights);
        _weightTotal = 0;
        foreach (IWeighted weight in _weights)
        {
            _weightTotal += weight.Rarity;
        }
    }

    /// <summary>
    /// Get a random tile from a weighted table
    /// by checking if a random value is (starting > random <= tile)
    /// with the starting value upgraded to the next tile rarity until a tile is found
    /// </summary>
    /// <returns></returns>
    public IWeighted GetValue()
    {
        int randomValue = UnityEngine.Random.Range(1, _weightTotal);
        int currentValueRange = 0;

        foreach (IWeighted weight in _weights)
        {
            if (randomValue > currentValueRange && randomValue <= (currentValueRange + weight.Rarity))
            {
                return weight;
            }
            currentValueRange += weight.Rarity;
        }
        //this shouldn't happen unless the weight total is wrong
        return null;
    }
}