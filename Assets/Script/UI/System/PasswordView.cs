using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PasswordView : MonoBehaviour {

    [SerializeField]
    private InputField pinInput = null;

    [SerializeField]
    private Text error = null;

    [SerializeField]
    private Button submit = null;

	public void Start()
	{
        error.gameObject.SetActive(false);
	}

	public void Initialize(Action compreted)
    {
        submit.onClick.AddListener(() =>
        {
            var systemModel = SystemRepository.Get();
            if (systemModel.isOk(pinInput.text)) {
                compreted();
            }
            else {
                error.gameObject.SetActive(true);
            }
        });
    }
}
