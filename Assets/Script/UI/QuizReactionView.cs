using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizReactionView : MonoBehaviour
{

    [SerializeField]
    private GameObject OKModal = null;
    [SerializeField]
    private GameObject FailedModal = null;

    // Use this for initialization
    void Start()
    {
        OKModal.SetActive(false);
        FailedModal.SetActive(false);
    }

    public void Show(bool ok)
    {
        OKModal.SetActive(ok);
        FailedModal.SetActive(!ok);
    }

    public void Close()
    {
        OKModal.SetActive(false);
        FailedModal.SetActive(false);
    }
}
