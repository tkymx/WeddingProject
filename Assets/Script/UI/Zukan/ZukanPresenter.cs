using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZukanPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject cellRoot = null;

    [SerializeField]
    private GameObject cellPrefab = null;

    [SerializeField]
    private ZukanCharacterInforPresenter characterInforPresenter = null;

    [SerializeField]
    private Button back = null;

    bool isInitialize = false;

	public void Initialize()
    {
        characterInforPresenter.gameObject.SetActive(false);

        if (isInitialize) {
            foreach(Transform trans in cellRoot.transform) {
                GameObject.Destroy(trans.gameObject);
            }
        }

        // キャラクタの開放状況を取ってきて
        var characterModels = PlayerCharacterRepository.GetAll();

        // その情報に応じてセルを追加する
        foreach(var model in characterModels) 
        {
            // セルをインスタンス化する
            var cell = GameObject.Instantiate(cellPrefab);
            cell.transform.SetParent(cellRoot.transform, false);

            var cellView = cell.GetComponent<ZukanCharacterCellView>();

            if (model.IsGet || DebugMenu.Instance.DebugParameter.AllZukan) {
                cellView.UpdateView(
                    model.Name,
                    model.Kind,
                    () => ShowCharacterInfoDialog(model)
                );
            } else {
                cellView.UpdateView(
                    "？？？？",
                    model.Kind,
                    () => {}
                );
            }
        }

        back.onClick.AddListener(() =>
        {
            // 画面を消すことで戻る
            gameObject.SetActive(false);
        });

        isInitialize = true;
    }

    public void ShowCharacterInfoDialog(PlayerCharacterModel characterModel)
    {
        // キャラクタの詳細モーダルを表示する
        Debug.Log(characterModel.Name + "のモーダルを表示します。");

        characterInforPresenter.gameObject.SetActive(true);
        characterInforPresenter.UpdateView(characterModel);
    }
}
