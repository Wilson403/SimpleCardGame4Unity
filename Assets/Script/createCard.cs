using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class createCard : MonoBehaviour {

    //初始位置
    [SerializeField] private Transform fromObject;
    //到达位置
    [SerializeField] private Transform toObject;
    
    [SerializeField] private Transform smallCard_1;
    [SerializeField] private Transform smallCard_2;
    
    private float offset;
    
    private List<GameObject> list = new List<GameObject>();

    private float transfromTime;
    private float timer;

    private UISprite sprite;
    private TweenScale cardTweenScale;

    private bool isTransform;
    private bool isMove;
    private bool isSkip;
    private bool isUP;


    [SerializeField] private float Speed;

    public GameObject prafab;
    private GameObject go;

    void Start()
    {
        offset = smallCard_2.transform.position.x - smallCard_1.transform.position.x;
        transfromTime = 2f;
        timer = 0.0f;
        isTransform = false;
        isMove = false;
        isSkip = false;
        isUP = true;
    }

    void Update()
    {
        if (!isSkip && Input.GetKeyDown(KeyCode.Space))
        {
            CreateCard();
            isUP = true;
        }

        if (isUP && Input.GetKeyUp(KeyCode.Space))
        {
            isSkip = true;
        }
        
        if (isSkip && Input.GetKeyDown(KeyCode.Space))
        {
            isMove = true;
            isTransform = false;
            isSkip = false;
            isUP = false;
        }


        if (isTransform)
        {
            timer += Time.deltaTime;
            int index = (int)(timer / (1f / Speed));
            index %= GameDate._gamedate.cardName.Length;
            sprite.spriteName = GameDate._gamedate.cardName[index];
            if (timer > transfromTime)
            {
                timer = 0.0f;
                isTransform = false;
                isMove = true;
                isSkip = false;
            }
        }
        
        if(isMove)
            cardMove();
    }

    void CreateCard()
    {
        go = NGUITools.AddChild(this.gameObject, prafab);
        go.transform.position = fromObject.position;
        sprite = go.GetComponent<UISprite>();
        cardTweenScale = go.GetComponent<TweenScale>();
        iTween.MoveTo(go, toObject.transform.position, 1f);
        isTransform = true;
        timer = 0;
    }

    
    void cardMove()
    {
        Vector3 newPos = smallCard_1.position + new Vector3(offset, 0, 0) * list.Count;
        cardTweenScale.PlayForward();
        iTween.MoveTo(go,newPos,1f);
        
        list.Add(go);
        isMove = false;
    }
}
