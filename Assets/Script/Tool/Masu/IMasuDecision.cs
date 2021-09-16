using UnityEngine;
using System.Collections;

/// <summary>
/// マスの決定を行う。
/// ・マスの決定に対話要素を入れることを考慮して決定フェイズをインターフェース化
/// ・IsDecisionNextMasu ：次のマスが決定しているかを確認
/// ・DoingDcition：マスの決定を進める
/// ・GetNextMasu：終わっていたら次のマスを取得する
/// </summary>
public interface IMasuDecision
{
    void DoingDcition();
    bool IsDecisionNextMasu();
    Masu GetNextMasu();
}
