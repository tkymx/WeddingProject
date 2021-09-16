using UnityEngine;
using System.Collections;

public class Chapter
{
    private GameObject masuGroup = null;
    private IMasuGenerator masuGenerator;

    public Chapter(IMasuGenerator masuGenerator)
    {
        this.masuGenerator = masuGenerator;
    }

    /// <summary>
    /// Initialize this instance.
    /// 章の初期化を行う。マップの生成などもここで行う
    /// </summary>
    public void  Initialize(GameObject masuGroup)
    {
        this.masuGenerator.Generate(masuGroup);
        this.masuGroup = masuGroup;
    }

    /// <summary>
    /// Removes the chapter.
    /// 章のために生成されたあれこれを消去する
    /// </summary>
    public void RemoveChapter()
    {
        if (masuGroup != null) {
            foreach (Transform n in masuGroup.transform) {
                GameObject.Destroy(n.gameObject);
            }
        }
    }
}
