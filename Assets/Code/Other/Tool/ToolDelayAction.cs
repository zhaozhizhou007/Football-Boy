using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 延迟执行脚本.
/// </summary>
public class ToolDelayAction : MonoBehaviour {
	
	private Action _Action;
	public void SetAction(Action action, float delay)
	{
		_Action = action;
		Invoke ("_DoDelay",delay);
	}

	void Start () {
		
	}

	void OnDisable()
	{
		CancelInvoke("_DoDelay");
	}


	void _DoDelay(){
		_Action ();
	}

}
