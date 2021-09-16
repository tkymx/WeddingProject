using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointIncrementNotify : MonoBehaviour {

    [SerializeField]
    Animator animator = null;

    [SerializeField]
    TextMeshProUGUI textMesh = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPoint(int point)
    {
        textMesh.text = string.Format("+{0}pt",point.ToString());
    }

    public void Appear()
    {
        animator.SetTrigger("PlusPoint");
    }
}
