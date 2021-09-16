using UnityEngine;
using System.Collections;

public class HitoMasuMover
{
    private HitoMover hitoMover;
    private MasuSummary masuSummary;

    private Masu currentMasu;
    public Masu CurrentMasu {
        get { return currentMasu; }
    }

    private int totalMasuCount;
    public int TotalMasuCount {
        get {
            return totalMasuCount;
        }
    }

    private int currentMoveCount;
    private int moveCount;

    enum Phase
    {
        Initialize,
        Decition,
        Move,
        Finish
    };

    Phase movePhase;
    IMasuDecision masuDesition;

    public HitoMasuMover(Hito hito, MasuSummary masuSummary)
    {
        this.masuSummary = masuSummary;
        this.currentMasu = masuSummary.StartMasu;
        this.totalMasuCount = masuSummary.StartMasuIndex;

        this.hitoMover = new HitoMover(hito);
        this.hitoMover.SetMasuPosition(currentMasu);

        movePhase = Phase.Initialize;
    }

    public void MoveStart(int moveCount)
    {
        this.moveCount = moveCount;
        this.currentMoveCount = 0;
        this.movePhase = Phase.Initialize;
    }

    public bool IsFinish()
    {
        if (movePhase == Phase.Finish) {
            return true;
        }

        return false;
    }

    public void Move()
    {
        // 順序
        if (movePhase == Phase.Initialize) {

            // 現在のマスカラ一個次のマスへの移動クラスを作成
            masuDesition = masuSummary.GetNextMasuDecition(currentMasu);
            movePhase = Phase.Decition;
        }
        else if (movePhase == Phase.Decition) {
            if (masuDesition.IsDecisionNextMasu()) {
                var nextMasu = masuDesition.GetNextMasu();
                hitoMover.MoveStart(this.currentMasu, nextMasu);
                this.currentMasu = nextMasu;
                movePhase = Phase.Move;
            }
            else {
                masuDesition.DoingDcition();
            }
        }
        else if (movePhase == Phase.Move) {
            if(hitoMover.IsFinish()){

                this.currentMoveCount++;
                this.totalMasuCount++;


                if (IsStop()) {
                    movePhase = Phase.Finish;
                }
                else {
                    movePhase = Phase.Initialize;
                }
            }
            else {
                hitoMover.Move();
            }
        }
    }

    bool IsStop() {
        if (currentMasu.IsStop()) {
            return true;
        }

        if (currentMoveCount >= moveCount) {
            return true;
        }

        return false;
    }
}
