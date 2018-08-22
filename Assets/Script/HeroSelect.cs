using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelect : MonoBehaviour
{

    public UISprite Hero_0;
    public UILabel HeroNameText;
   
	 
	void Start () {
        
    }

    void OnClick()
    {
        string HeroName = this.gameObject.name;
        Hero_0.spriteName = HeroName;
        char HeroNameIndex = HeroName[HeroName.Length - 1];
        int index = HeroNameIndex - '0';
        HeroNameText.text = GameDate._gamedate.HeroNames[index - 1];
    }

    
}
