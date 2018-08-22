using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    private UISprite hero1;
    private UISprite hero2;

    private Transform Hero1Parent;
    private Transform Hero2Parent;

    private UILabel hero1UiLabel;
    private UILabel hero2UiLabel;

    private static int hpCount = 30;
    
    

    void Start()
    {
        hero1 = this.transform.Find("hero1").GetComponent<UISprite>();
        hero2 = this.transform.Find("hero2").GetComponent<UISprite>();

        hero1UiLabel = this.transform.Find("hero1/Label").GetComponent<UILabel>();
        hero2UiLabel = this.transform.Find("hero2/Label").GetComponent<UILabel>();

        Hero1Parent = this.transform.Find("Hero1NewPos").GetComponent<Transform>();
        Hero2Parent = this.transform.Find("Hero2NewPos").GetComponent<Transform>();

        TweenPosition hero1tween = hero1.GetComponent<TweenPosition>();
        TweenPosition hero2tween = hero2.GetComponent<TweenPosition>();

        hero1.spriteName = PlayerPrefs.GetString("hero1");
        hero2.spriteName = PlayerPrefs.GetString("hero2");

        hero1tween.AddOnFinished(()=>HeroSetAnother(hero1,Hero1Parent));
        hero2tween.AddOnFinished(()=>HeroSetAnother(hero2,Hero2Parent));
    }

    void HeroSetAnother(UISprite uISprite,Transform ParentSprite)
    {
        uISprite.transform.SetParent(ParentSprite.transform);
        uISprite.transform.localPosition = new Vector3(0, 0, 0);

        uISprite.SetAnchor(ParentSprite);

        uISprite.leftAnchor.relative = 0;
        uISprite.rightAnchor.relative = 1;
        uISprite.topAnchor.relative = 1;
        uISprite.bottomAnchor.relative = 0;

        uISprite.leftAnchor.absolute = -15;
        uISprite.rightAnchor.absolute = 15;
        uISprite.topAnchor.absolute = 0;
        uISprite.bottomAnchor.absolute = -10;
    }

    private void takeHealth(int damage)
    {
        hpCount -= damage;
        hpCount = Mathf.Clamp(hpCount, 0, 30);
        hero1UiLabel.text = hpCount + "";
    }
    
    private void cure(int cureCount)
    {
        hpCount += cureCount;
        hpCount = Mathf.Clamp(hpCount, 0, 30);
        hero1UiLabel.text = hpCount + "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            cure(10);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            takeHealth(10);
        }
        
    }
}
