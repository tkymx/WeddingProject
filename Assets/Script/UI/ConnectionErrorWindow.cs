using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionErrorWindow : MonoBehaviour {

    [SerializeField]
    private Button retryButton = null;

    public Action retryButtonEvent;

    public void Initialize()
    {
        if (retryButton == null) {
            retryButton = GetComponentInChildren<Button>();
        }
        retryButton.onClick.AddListener(() =>
        {
            retryButtonEvent();
        });
	}
}
