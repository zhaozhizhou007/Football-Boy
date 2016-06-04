using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManager : SingletonCom<TouchManager> {


	//开关可用.
	public bool Available = true;

	/// <summary>
	/// 双击的间隔时间.
	/// </summary>
	//public static float DoubleInterval = 0.2f;

	/*-----------------*/
	/// <summary>
	/// 敲击事件
	/// </summary>
	/// <param name="clickObject"></param>
	/// <param name="position"></param>
	public delegate void DelegateTap (GameObject clickObject,Vector3 position);

	/// <summary>
	/// 单击事件委托
	/// </summary>
	public DelegateTap On_TapOne;

	/// <summary>
	/// 双击事件委托
	/// </summary>
	public DelegateTap On_TapDouble;

	private Gesture _PreTapGesture;
	public override void Init ()
	{
		base.Init ();
				Available = true;
		//		//重新设置触控的摄像机发射射线.
		//		//GameManager.I.gameObject.GetComponentInChildren<ScreenRaycaster> ().Cameras = new Camera[] { Camera.main };

	}

	/// <summary>
	///  重新发送上次的点击事件.
	/// </summary>
	public void RecallNextFrame()
	{
		StartCoroutine(_SoRecall());
	}
	IEnumerator _SoRecall()
	{
		yield return 0;
		OnTapFilter(_PreTapGesture);
	}

	void OnEnable ()
	{	
		//敲击事件钩子,过滤单击,双击.
		FingerGestures.OnGestureEvent += OnTapFilter; 
	}

	void OnDisable ()
	{
		//移除过滤单击,双击.
		FingerGestures.OnGestureEvent -= OnTapFilter; 
	}

	private KeyCode _CurKey = KeyCode.None;

	private float timeInterval = 0.1f;
	private float nextTime = 0;
	void Update()
	{
		if(_CurKey != KeyCode.None)
		{

			EDirection dir = EDirection.None;

			switch (_CurKey) {
			case KeyCode.A:
				dir =  EDirection.Left;
				break;
			case KeyCode.D:
				dir = EDirection.Right;
				break;
			case KeyCode.W:
				dir = EDirection.Up;
				break;
			case KeyCode.S:
				dir = EDirection.Down;
				break;

			}

			if(nextTime < Time.time)
			{
				EventCenter.Event_DirectionInput.Invoke(dir);
				nextTime = Time.time + timeInterval;
			}

		}
	}

	void OnGUI()
	{

		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) )
		{
			_CurKey = KeyCode.None;
			EventCenter.Event_DirectionInputStop.Invoke();
		}

		else if(Input.anyKey)
		{
			if(Event.current.isKey)
			{
				//---不知道为什么会触发2次，第一次是None
				KeyCode key = Event.current.keyCode;
				//Debug.Log(key);
				if(key == KeyCode.A || key == KeyCode.S || key == KeyCode.W || key == KeyCode.D)
				{
					_CurKey = key;
					//EventCenter.Event_KeyDown.Invoke(key)	;
				}
				else if(key == KeyCode.Space)
				{
					EventCenter.Event_InputFire.Invoke();
				}

			}

		}

	}

	//敲击事件
	void OnTapFilter (Gesture gesture)
	{
		//eventSystem.currentSelectedObject
		//不可用.
		if(!Available){
			return;
		}

		if (gesture is TapGesture) {
			TapGesture e = gesture as TapGesture;
			if (e.Selection == null) {
//				Debug.Log ("Tap selection is null , so return.");
				return;
			}
			_PreTapGesture = gesture;

//			if(IsPointerOverGameObject())
//				return;

			Vector3 hitPos3d = e.Raycast.Hit3D.point;
			if (e.Taps == 0) {
				//设置单击事件
				if (On_TapOne != null) {
					On_TapOne (e.Selection, hitPos3d);
				}

			} else if (e.Taps == 2) {
				//设置双击事件
				if (On_TapDouble != null) {
					On_TapDouble (e.Selection, hitPos3d);
				}

			}

		}

	}


	public bool IsPointerOverGameObject( )
	{

		if (EventSystem.current.IsPointerOverGameObject ())
			return true;
		if(EventSystem.current.currentSelectedGameObject != null)
			return true;
		if(Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
		{
			if (EventSystem.current.currentSelectedGameObject != null
			   && EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId))
				return true;

		}
		return false;
	}

}
