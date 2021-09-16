using UnityEngine;
using System.Collections;

public class HitoMover
{
    private Hito hito;

    public HitoMover(Hito hito) {
        this.hito = hito;
    }

    private Vector3 startHitoPosition;
    private Vector3 endOffset;
    private float progress = 1.0f;

    public void SetMasuPosition(Masu masu) {
        this.hito.transform.position = masu.transform.position;
    }

    public void MoveStart(Masu currentMasu, Masu nextMasu) {
        startHitoPosition = hito.transform.position;
        endOffset = nextMasu.transform.position - currentMasu.transform.position;
        progress = 0;

        hito.Punch();
    }

    public bool IsFinish()
    {
        if (progress >= 1.0f) {

            hito.Punch();
            return true;
        }            
        return false;
    }

    public void Move()
    {
        progress += Time.deltaTime;
        hito.transform.position = startHitoPosition + endOffset * progress;
    }
}
