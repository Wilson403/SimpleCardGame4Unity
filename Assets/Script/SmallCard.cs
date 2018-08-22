using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCard : MonoBehaviour {

    [SerializeField] private Transform smallCard_1;
    [SerializeField] private Transform smallCard_2;

    [SerializeField] private GameObject prafab;

    private float offset;

    private List<GameObject> list = new List<GameObject>();

	void Start()
    {
        offset = smallCard_2.transform.position.x - smallCard_1.transform.position.x;
    }

    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            CreateCard();
//        }
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            LoadCard();
//        }
    }

    public void CreateCard()
    {
        GameObject go = NGUITools.AddChild(this.gameObject, prafab);
        go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Vector3 toPosition = smallCard_1.position + new Vector3(offset, 0, 0) * list.Count;
        iTween.MoveTo(go, toPosition, 1f);
        list.Add(go);
    }

    public void LoadCard()
    {
        int index = Random.Range(0, list.Count);
        Destroy(list[index]);
        list.RemoveAt(index);
        for(int i = 0; i < list.Count; i++)
        {
            iTween.MoveTo(list[i], smallCard_1.position + new Vector3(offset, 0, 0) * i, 0.5f);
        }
    }
}
