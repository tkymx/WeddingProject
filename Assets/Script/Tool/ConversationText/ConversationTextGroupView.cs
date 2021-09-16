using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConversationTextGroupView : MonoBehaviour
{
    [SerializeField]
    private Image[] images = new Image[4];

    [SerializeField]
    private Text nameText = null;

    [SerializeField]
    private Text text = null;

    public void InitializeView(string ll, string lr, string rl, string rr)
    {
        var paths = new string[4]{
            ll,lr,rl,rr
        };
        for (int pathIndex = 0; pathIndex < 4;pathIndex++) {
            images[pathIndex].gameObject.SetActive(paths[pathIndex] != "");
            if (paths[pathIndex] != "")
            {
                images[pathIndex].sprite = ResourceManager.GetSprite(paths[pathIndex]);
            };
        }
    }

    public void UpdateView(string text, string nameText, int charaIndex)
    {
        this.nameText.text = nameText;
        this.text.text = text;

        int currentSiblingIndex = 0;
        for (int index = 0; index < images.Length; index++) {
            if (index == charaIndex) {
                images[index].color = Color.white;
                images[index].transform.SetSiblingIndex(images.Length-1);
            }
            else
            {
                images[index].color = Color.gray;
                images[index].transform.SetSiblingIndex(currentSiblingIndex++);
            }
        }
    }

}

