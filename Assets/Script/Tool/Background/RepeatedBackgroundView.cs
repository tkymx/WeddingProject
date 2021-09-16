using UnityEngine;
using System.Collections;

public class RepeatedBackgroundView : MonoBehaviour
{
    public void Initialize(GameObject backGroundPrefab,GameObject root, float maxHeight)
    {
        var rect = backGroundPrefab.GetComponent<RectTransform>();
        for (float currentY = 0; currentY < maxHeight; currentY += rect.sizeDelta.y) {
            var background = GameObject.Instantiate(backGroundPrefab);
            background.transform.SetParent(root.transform, false);
            background.transform.position = new Vector3(0, currentY, 0);
        }
    }
}
