using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistroyCards : MonoBehaviour {

    public UISprite inCard;
    public UISprite outCard;
    public GameObject CardPrefab;
    public Transform Card1Target;

    private bool IsNext;

    public List<GameObject> cardlist = new List<GameObject>();

    void Start()
    {
        IsNext = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsNext)
        {
            IsNext = false;
            StartCoroutine(AddCard());
        }
    }

    public IEnumerator AddCard()
    {
        GameObject go = Instantiate(CardPrefab, inCard.transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<UISprite>().depth = -1;
        yield return 0;
        go.GetComponent<UISprite>().depth = 9;
        go.transform.position = inCard.transform.position;
        iTween.MoveTo(go, Card1Target.transform.position, 1);
        cardlist.Add(go);

        if (cardlist.Count > 5)
        {
            iTween.MoveTo(cardlist[0], outCard.transform.position,1);
            Destroy(cardlist[0], 2f);
            cardlist.RemoveAt(0);
        }

        for (int i = 0; i < cardlist.Count - 1; i++)
        {
            iTween.MoveTo(cardlist[i], cardlist[i].transform.position + new Vector3(0, -0.1567398f, 0), 0.5f);
        }

        yield return new WaitForSeconds(1f);
        IsNext = true;

    }
}
