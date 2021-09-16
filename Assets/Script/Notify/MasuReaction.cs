using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuReaction : MonoBehaviour {

    [SerializeField]
    private GameObject abnormalMasu = null;

    [SerializeField]
    private GameObject eventMasu = null;

	// Use this for initialization
	void Start () {
        CloseRaction();
	}

    public void ShowAbnormalMasuReactoin()
    {
        CloseRaction();
        abnormalMasu.SetActive(true);
    }

    public void ShowEventMasuReactoin()
    {
        CloseRaction();
        eventMasu.SetActive(true);
    }

    public void CloseRaction()
    {
        abnormalMasu.SetActive(false);
        eventMasu.SetActive(false);
    }
}
