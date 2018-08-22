using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public enum GameState
	{
		Start,
		Playing,
		End
	}

	private UISprite _rope;
	
	private float _ropeWidth;
	private float _ropeHeight;
	
	public  float RoundTime = 60f;
	private float timer = 0.0f;
	
	private Vector3 _ropePosition;
	//private UIWidget _ropeRect;
	

	public GameState state = GameState.Start;

	void Awake()
	{
		_rope = this.GetComponent<UISprite>();
		//_ropeRect = _rope.GetComponent<UIWidget>();
	}

	private void Start()
	{
		_ropeWidth = _rope.width;
		_ropeHeight = _rope.height;
		_ropePosition = _rope.transform.position;
		_rope.enabled = false;
	}

	private void Update()
	{
		if (state == GameState.Playing)
		{
			timer += Time.deltaTime;
			if (timer > RoundTime)
			{
				TransformRound();
			}
			else if((RoundTime-timer)<=15)
			{
				_rope.enabled = true;
				_rope.transform.position = _ropePosition;
				_rope.height = (int)_ropeHeight;
				_rope.width = (int) ((RoundTime - timer) / 15 * _ropeWidth);
				if(_rope.width <= 10)
					_rope.enabled = false;
			}
		}
	}

	private void TransformRound()
	{
		Debug.Log("对手回合");
	}
}
