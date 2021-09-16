using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAnimation : MonoBehaviour {

    [SerializeField]
    private Animation sinmpeleAnimation = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        sinmpeleAnimation.Play();
	}
}
