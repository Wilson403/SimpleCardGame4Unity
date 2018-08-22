using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Select : MonoBehaviour {

    public static _Select select;
    private UISprite SelectRole;
    private TweenPosition SelectTween;
    public UISprite Hero_0;
    int randnum;

    void Awake()
    {
        select = this;   
    }


	void Start ()
    {
        randnum = Random.Range(1, 10);
        SelectTween = this.transform.Find("Hero_Select").GetComponent<TweenPosition>();
        SelectRole = this.transform.Find("Hero_Select").GetComponent<UISprite>();
        SelectTween.AddOnFinished(ReSetAnochor);
        this.transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.transform.gameObject.SetActive(true);
    }

    void ReSetAnochor()
    {
        SelectRole.rightAnchor.relative = 1;
        SelectRole.rightAnchor.absolute = -59;
    }

    public void EnterButtonClick()
    {
        VSShow._vS.Show(Hero_0.spriteName, "hero" + randnum.ToString());
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
		yield return new WaitForSeconds (2f);
        SceneManager.LoadScene("VSScene");

    }
}
