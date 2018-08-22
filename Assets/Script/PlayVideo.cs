using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class PlayVideo : MonoBehaviour
{ 
    //是否跳过动画的提示信息
    public UILabel TipManager;

    //跳过动画所需时间的进度条
    public UISlider SkipSlider;

    public UIPanel BackGround;

    public TweenScale LogoTween;

    public TweenPosition SelectRoleTween;

	MovieTexture VideoSource;

    //是否展示提示信息
	bool IsShowTipManager;

    //提示信息存在时间
    float TipShowTime;

    //按住跳过所需时间
    float HoldTime;

    //动画是否结束
    bool IsEnd;

    //是否允许选择角色
    bool IsSelectRole;


	 
	void Start () {

        TipManager.text = "Press on 'ESC' skip";
        TipShowTime = 0.0f;
        HoldTime = 0.0f;
        SkipSlider.value = 0;
		IsShowTipManager = false;
	    VideoSource = (MovieTexture)this.GetComponent<UITexture> ().mainTexture;
		VideoSource.loop = false;
		VideoSource.Play ();
        IsEnd = false;
        IsSelectRole = false;

        LogoTween.AddOnFinished(StartGame);
       // SelectRoleTween.AddOnFinished(StartGame);
	}
	
	 
	void Update () {

        if (IsSelectRole && Input.GetMouseButtonDown(0))
        {
            _Select.select.Show();
            SelectRoleTween.PlayForward();
        }

        //动画非跳过结束的情况，只执行一次
        if (!VideoSource.isPlaying && !IsEnd)
        {
            VideoEnd();
            IsEnd = true;
        }

        //按下回车或ESC或空格允许显示跳过画面的提示信息
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            //动画在播放时才生效
            if (VideoSource.isPlaying)
                IsShowTipManager = true;
        }

        //在允许显示提示UI以及UI还在闪烁时间内的时候
        if (IsShowTipManager && TipShowTime < 6f)
        {
            TipShowTime += Time.deltaTime;
            ActiveTipManager(true);  

            //在提示UI出现的情况下按住ESC键3秒可以停止动画
            if (Input.GetKey(KeyCode.Escape)) 
            {
                //按下ESC键则重置提示UI的存在时间
                TipShowTime = 0.0f;

                HoldTime += Time.deltaTime;
                
                //将按住时间绑定进度条
                SkipSlider.value = (HoldTime / 3);

                //按住超过3秒
                if (HoldTime >= 3f)
                {
                    VideoEnd();
                    IsEnd = true;
                }
            }
            else
            {
                //松开ESC键重置按住时间
                HoldTime = 0.0f;

                //松开ESC键回滚进度条
                SkipSlider.value = 0;
                
            }
        }

        //过了闪烁时间或者动画结束的情况
        else
        {
            ActiveTipManager(false);
            IsShowTipManager = false;

            //闪烁时间重置
            TipShowTime = 0.0f;
        }
	}


    /// <summary>
    /// 是否激活提示UI元素
    /// </summary>
    /// <param name="BoolStr"></param>
    void ActiveTipManager(bool BoolStr)
    {
         
        if (BoolStr)
        {
            TipManager.gameObject.SetActive(true);
            SkipSlider.gameObject.SetActive(true);
        }

        else if(BoolStr == false)
        {
            TipManager.gameObject.SetActive(false);
            SkipSlider.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// 动画结束后所要处理的逻辑
    /// </summary>
    void VideoEnd()
    {
        VideoSource.Stop();
        IsShowTipManager = false;

        //将游戏背景图激活
        BackGround.gameObject.SetActive(true);
        
    }

     
    public void StartGame()
    {
        IsSelectRole = true;
    }



   



}
