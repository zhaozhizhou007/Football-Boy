using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// 抛物线运动脚本。
/// </summary>
public class MotionParabolic : MonoBehaviour {


	public const float G = 19.8000f;

	public Vector3 StartPosition;
	public Vector3 EndPosition;
	//
	public float Theta;
	public float Speed;
	public float Duration;

	private float _CurVerticalSpeed = 0;
	private float _ElapseTime = 0;

	private float _CurG = G;

	//是否正在运行
	private bool _Doing = false;

	private Action _CallBack ;

	public void SetInitG(float g)
	{
		_CurG = g;
	}

	public Vector3 GetVeclocityHorizontal ()
	{
		return StartPosition.MDirectionIgnoreY (EndPosition) * Speed * Mathf.Cos (Mathf.Deg2Rad * Theta);
	}

	public Vector3 UpdateOffectPosition (float elapseTime)
	{
		Vector3 positionOffect  = Vector3.zero;
		positionOffect 			+= GetVeclocityHorizontal () * elapseTime;

		Vector3 v 			= Vector3.up * (_CurVerticalSpeed * elapseTime - 0.5f * _CurG * elapseTime * elapseTime);
		_CurVerticalSpeed	= _CurVerticalSpeed - _CurG * elapseTime;
		_ElapseTime 		+= elapseTime;
		positionOffect 		+= v;
		return positionOffect;

	}
		

	void Update () 
	{

		if(!_Doing )
			return;

		Vector3 offectPosition = UpdateOffectPosition(Time.deltaTime);;

		transform.position += offectPosition;

		if(Vector3.Distance(transform.position,EndPosition) <= 0.1f || _ElapseTime >= Duration)
		{
			transform.position = EndPosition;
			if( _CallBack != null)
			{
				_CallBack();
			}
			_Doing = false;

		}


	}


	public void Go(Vector3 startPosition, Vector3 endPosition, float theta,float initialVelocity)
	{
		/*----------------------------------------------------------
         * x = vo·t·cosθ
         * y = vo·t·sinθ - 1/2θ·a·t^2
         ------------------------------------------------------------*/
		float horizontalS 		 = startPosition.MDistanceIgnoreY (endPosition);
		float startVerticalH 	 = startPosition.y - endPosition.y;

		float speedHorizontal = initialVelocity * Mathf.Cos (Mathf.Deg2Rad * theta);
		float speedVertical	 = initialVelocity * Mathf.Sin (Mathf.Deg2Rad * theta);

		float timeDuration 		 = horizontalS / speedHorizontal;
		float initG = 2 * (speedVertical * timeDuration + startVerticalH) / (timeDuration * timeDuration );

		StartPosition 	= startPosition;
		EndPosition 	= endPosition;
		Theta 			= theta;
		Duration 		= timeDuration;
		Speed 			= initialVelocity;

		SetInitG(initG);
		_ElapseTime = 0f;
		_CurVerticalSpeed = Speed * Mathf.Sin (Mathf.Deg2Rad * Theta);

		transform.position = StartPosition;
		_Doing = true;

	}

	public void SetCallBack(Action action)
	{
		_CallBack = action;

	}


}
