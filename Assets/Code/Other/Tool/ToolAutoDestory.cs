using UnityEngine;
using System.Collections;
using System ;

/// <summary>
/// 自动销毁脚本.
/// </summary>
public class ToolAutoDestory : MonoBehaviour {

	public bool EnableAwake = false;
	public float Time = 4f;

	private Action CallBack;

	void OnEnable()
	{	
		if (EnableAwake && Time > 0)
			Do (Time);
		
	}

	public void Do(float time = 2f)
	{
		CancelInvoke ();
		Time = time;
		Invoke ("_Destory",time);
	}

	void OnDisable()
	{
		CancelInvoke ();
	}

	public void Stop()
	{
		CancelInvoke();

	}


	void _Destory()
	{
		if(CallBack != null)
		{
			CallBack.Invoke();
		}

		UnityEngine. Object.Destroy(gameObject);
		//ResourceManager.I.Recycle (this.gameObject);
	}


	public void SetCallBack(Action _action)
	{
		this.CallBack = _action;
	}
}
