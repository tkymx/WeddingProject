using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCell : MonoBehaviour {

    [SerializeField]
    Button button = null;

    [SerializeField]
    Text text = null;

    private void Initialize(Action debugAction, string text)
    {
        this.button.onClick.AddListener(() => debugAction());
        this.text.text = text;
    }

    public static DebugCell CreateDebugCell(Action debugAction, string text, GameObject parent)
    {
        var prefab = ResourceManager.GetPrefab("UI/DebugCell");
        var ins = GameObject.Instantiate(prefab);
        ins.transform.SetParent(parent.transform, false);
        var debugCell = ins.GetComponent<DebugCell>();
        debugCell.Initialize(debugAction, text);
        return debugCell;
    }
}
