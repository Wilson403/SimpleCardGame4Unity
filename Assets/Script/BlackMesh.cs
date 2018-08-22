using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMesh : MonoBehaviour {

    public static BlackMesh _blackMesh;

    void Awake()
    {
        _blackMesh = this;
        this.gameObject.SetActive(false);
    }

   public void Show()
    {
        this.gameObject.SetActive(true);
    }

   public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
