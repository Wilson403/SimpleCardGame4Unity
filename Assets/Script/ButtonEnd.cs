using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnd : MonoBehaviour {

    private UIButton ButtonEndSprite;
    private UILabel ButtonEndLabel;

	 
	void Start () {
        ButtonEndSprite = this.GetComponent<UIButton>();
        ButtonEndLabel = this.transform.Find("ButtonEndLabel").GetComponent<UILabel>();
	}

   public void BeClick()
    {
        ButtonEndSprite.state = UIButton.State.Disabled;
        ButtonEndSprite.GetComponent<BoxCollider>().enabled = false;
        ButtonEndLabel.text = "对方回合";
    }
	 
}
