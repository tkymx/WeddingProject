using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActiveter : MonoBehaviour {

    [SerializeField]
    private List<GameObject> gameObjects = null;

	private void Awake()
	{
        foreach(var go in gameObjects) {
            go.SetActive(true);
        }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
