using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameMover : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera = null;

    [SerializeField]
    private Hito hito = null;

    [SerializeField]
    float leapSpeed = 0.5f;

	// Update is called once per frame
	void Update () {
        if (hito.transform.position.y > 0) {
            
            var targetPosition = mainCamera.transform.position;
            targetPosition.y = hito.transform.position.y;

            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position, 
                targetPosition, 
                leapSpeed
            );
        }
	}
}
