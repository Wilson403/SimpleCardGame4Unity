using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour {

    public int UsableNumber;
    public int TotalNumber;

    public UISprite[] Crystal;
    public UILabel CurrentNum;

    private int MaxNumber;

    void Start()
    {
        MaxNumber = Crystal.Length;
    }

    void Update()
    {
        UpdateShow();
    }

    void UpdateShow()
    {
        for (int i = TotalNumber; i < MaxNumber; i++)
        {
            Crystal[i].gameObject.SetActive(false);
        }

        for (int i = UsableNumber; i < TotalNumber; i++)
        {
            Crystal[i].spriteName = "TextInlineImages_TextInlineImages_11";
        }

        for (int i = 0; i < UsableNumber; i++)
        {
            Crystal[i].spriteName = "TextInlineImages_TextInlineImages_12";
        }

        CurrentNum.text = UsableNumber + "/" + TotalNumber;
    }

	 
}
