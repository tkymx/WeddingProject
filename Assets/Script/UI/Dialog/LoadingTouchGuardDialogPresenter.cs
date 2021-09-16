using UnityEngine;
using System.Collections;

public class LoadingTouchGuardDialogPresenter : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public void Close() {
        gameObject.SetActive(false);
        GameObject.Destroy(this);
    }
}
