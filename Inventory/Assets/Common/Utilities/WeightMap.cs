using System.Collections.Generic;
/// <summary>
/// Takes a list of weighted objects and returns them at random using those weights
/// </summary>
public class WeightMap
{
    private List<IWeighted> _weights;
    private int _weightTotal;

    public WeightMap(IEnumerable<IWeighted> weights)
    {
        _weights = new List<IWeighted>(weights);
        _weightTotal = 0;
        foreach (var weight in _weights)
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
        int _randomValue = UnityEngine.Random.Range(1, _weightTotal);
        int _currentValueRange = 0;

        foreach (var weight in _weights)
        {
            if (_randomValue > _currentValueRange && _randomValue <= (_currentValueRange + weight.Rarity))
            {
                return weight;
            }
            _currentValueRange += weight.Rarity;
        }
        //this shouldn't happen unless the weight total is wrong
        return null;
    }
}
