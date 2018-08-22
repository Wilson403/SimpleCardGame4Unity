
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDate : MonoBehaviour {

    public static GameDate _gamedate;

    public string[] HeroNames =
       {"01","02","03","04","05","06","07","08","09"};

    public string[] cardName;

    void Start () {
        _gamedate = this;
	}
	

}
