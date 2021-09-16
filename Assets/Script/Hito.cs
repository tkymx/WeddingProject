using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitoResources {

    public string Path { get; private set; }
    public Sprite HitoSprite { get; private set; }

    public HitoResources(string path) 
    {
        Path = path;
        HitoSprite = ResourceManager.GetSprite(path);
    }
}

public class Hito : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// The point.
    /// クイズ等によって獲得することができるポイント
    /// </summary>
    private int point;
    public int Point{
        get {
            return point;
        }
        set {
            point = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeHitoImage(HitoResources hitoResources)
    {
        spriteRenderer.sprite = hitoResources.HitoSprite;
    }

    public void Punch()
    {
        iTween.PunchScale(this.gameObject, iTween.Hash(
            "x", 0.02,
            "y", 0.02,
            "time", 0.5f)
        );
    }
}
