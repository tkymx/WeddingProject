using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZukanCharacterInfoView : MonoBehaviour {

    [SerializeField]
    private Text characterName = null;

    [SerializeField]
    private Text type = null;

    [SerializeField]
    private Text location = null;

    [SerializeField]
    private Text profile = null;

    [SerializeField]
    private Image image = null;

    public void UpdateView(string name, string type, string location, string profile, Sprite image)
	{
        this.characterName.text = name + "の情報";
        this.type.text = type;
        this.location.text = location;
        this.profile.text = profile;
        this.image.sprite = image;
        this.image.preserveAspect = true;
	}
}
