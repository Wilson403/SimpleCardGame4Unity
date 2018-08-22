using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSShow : MonoBehaviour {

    public static VSShow _vS;

    public TweenPosition Hero1tween;
    public TweenPosition Hero2tween;
    public TweenScale vstween;
    public GameObject VS;
    

    void Awake()
    {
        _vS = this;
    }

    void Start () {
         
	}


    public void Show(string heroname1,string heroname2)
    {
        BlackMesh._blackMesh.Show();
        VS.gameObject.SetActive(true);
        PlayerPrefs.SetString("hero1", heroname1);
        PlayerPrefs.SetString("hero2", heroname2);
        Hero1tween.GetComponent<UISprite>().spriteName = heroname1;
        Hero2tween.GetComponent<UISprite>().spriteName = heroname2;

        Hero1tween.PlayForward();
        Hero2tween.PlayForward();
        vstween.PlayForward();
    }
}
