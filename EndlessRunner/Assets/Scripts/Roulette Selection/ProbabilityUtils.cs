using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dr. Mike Cooper's Probability utility.
/// (Added a few comments for clarity)
/// </summary>
public class ProbabilityUtils {

    public class CumulativeProbabilityObject
    {
		//Pass a starting probability, and the probability increase for each time it isn't picked.
        public CumulativeProbabilityObject(int baseProbability, int deltaProbabilityIncrease)
        {
            this.baseProbability = currentProb = baseProbability;
            delta = deltaProbabilityIncrease;
        }

        public int baseProbability;
        public int delta;
        public int currentProb;


        public bool IsHit()
        {
			
            if (Random.Range(0, 100) < currentProb)
            {
                currentProb = baseProbability;
                return true;
            }
            else
            {
                currentProb += delta;
                return false;
            }
        }
    }

    // class that stores a number of objects, and the current cumulativ eprob
    // of each getting picked
    public class Picker
    {
        public int baseProb;
        public int delta;
        public int[] currentProbs;
        public object[] objects;

        public Picker(int bp, int d, object[] o)
        {
            baseProb = bp;
            delta = d;
            objects = o;
        }

        public object GetNext()
        {
            if (currentProbs == null)
            {
                currentProbs = new int[objects.Length];
                for (int i = 0; i < objects.Length; i++)
                    currentProbs[i] = 1;
            }
            int index = GetRouletteIndex(currentProbs);
            for (int i = 0; i < currentProbs.Length; i++)
            {
                if (index == i)
                    currentProbs[i] = baseProb;
                else
                    currentProbs[i] += delta;
            }
            return objects[index];
        }
    }

    // static function for getting an index when given a set of different probabilities
    static public int GetRouletteIndex(int[] probs)
    {
        int total = 0;
        for (int i = 0; i < probs.Length; i++)
            total += probs[i];
        int dice = Random.Range(0, total);
        total = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            total += probs[i];
            if (total > dice)
                return i;
        }
        return -1;
    }

}
