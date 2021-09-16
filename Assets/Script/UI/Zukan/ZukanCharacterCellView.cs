using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ZukanCharacterCellView : MonoBehaviour
{
    [SerializeField]
    private Button selectButton = null;

    [SerializeField]
    private Text characterName = null;

    [SerializeField]
    private Text kind = null;

    public void UpdateView(string name, string kind, Action onClick)
	{
        this.characterName.text = name;
        this.kind.text = kind;
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onClick());
	}
}
