using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Enum;


/// <summary>
/// 英雄动画控制
/// </summary>
public class HeroAnimatorController : BaseAnimator
{
		
	public static readonly string StateIdle = "Idle";
	public static readonly string StateRun = "Run";
	public static readonly string StateDie = "Die";
	public static readonly string StateKick = "Kick";
	
	public static readonly string ParamIsRun = "IsRun";
	public static readonly string ParamIsWalk = "IsWalk";

	private RuntimeAnimatorController[] mAnimCtrls;

	static HeroAnimatorController()
	{

	}

	void OnEnable()
	{
		Init();
	}

	void Start ()
	{
		
	}

	public override void Init ()
	{
		base.Init ();


	}

	public override void PlayIdle ()
	{
		Clean();
		am.SetBool(ParamIsRun,false);
	}

	public override void PlayRun ()
	{
		Clean();
		am.SetBool(ParamIsRun,true);
	}

	public void PlayKick()
	{
		Clean();
		am.Play(StateKick);
	}

}


