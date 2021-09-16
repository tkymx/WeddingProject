using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZukanCharacterInforPresenter : MonoBehaviour {

    [SerializeField]
    ZukanCharacterInfoView infoView = null;

    [SerializeField]
    Button back = null;

    public void UpdateView(PlayerCharacterModel model)
	{
        infoView.UpdateView(
            model.Name,
            model.Type,
            model.Location,
            model.Profile,
            ResourceManager.GetSprite(model.ImageName)
        );

        back.onClick.AddListener(() =>
        {
            // 自分の画面を非表示にすることで消す。
            gameObject.SetActive(false);
        });
	}
}
