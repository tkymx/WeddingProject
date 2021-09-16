using UnityEngine;
using System.Collections;

public class MasuForceDecision : IMasuDecision
{
    Masu nextMasu;

    public MasuForceDecision(Masu masu) 
    {
        nextMasu = masu;
    }

    public void DoingDcition()
    {
        
    }

    public bool IsDecisionNextMasu()
    {
        return true;
    }

    public Masu GetNextMasu()
    {
        return nextMasu;
    }
}
