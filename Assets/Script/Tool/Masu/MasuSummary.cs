using UnityEngine;
using System.Collections.Generic;


public class MasuSummaryGenerator
{
    public static MasuSummary Generate(List<Masu> masus, int startMsuIndex)
    {
        return new MasuSummary(masus, startMsuIndex);
    }
}

/// <summary>
/// Masu summary.
/// マスは直線になることが決まっている
/// </summary>
public class MasuSummary
{
    private int startMasuIndex;
    public int StartMasuIndex {
        get { return startMasuIndex; }
    }

    private List<Masu> masus = null;

    public MasuSummary(List<Masu> masus, int startMasuIndex) {
        this.masus = masus;
        this.startMasuIndex = startMasuIndex;
    }

    public Masu StartMasu {
        get {
            return masus[startMasuIndex];
        }
    }

    public IMasuDecision GetNextMasuDecition(Masu currentMasu) {
        int index = masus.FindIndex((Masu masu) => masu == currentMasu);
        if (index < 0) {
            Debug.LogError("現在のますが存在しておりません");
        }
        return new MasuForceDecision(masus[index + 1]);
    }
}
