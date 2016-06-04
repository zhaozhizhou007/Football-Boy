using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 基础动画控制
/// </summary>
public class BaseAnimator : MonoBehaviour
{
	public Animator am;

	/// <summary>
	/// 初始化
	/// </summary>
	public virtual void Init ()
	{
		am = GetComponent<Animator> ();
		if(am == null)
		{
			//从子结点获取.
			am = GetComponentInChildren<Animator> ();
		}

		if(am == null)
		{
			Debug.LogError ("Cannt find Animator , plz check it . ");
		}

	}



	/// <summary>
	/// 清空所有状态.
	/// </summary>
	public virtual void Clean ()
	{
		
	}

	/// <summary>
	/// 播放跑步.
	/// </summary>
	public virtual void PlayRun ()
	{
		
	}

	/// <summary>
	/// 播放走路.
	/// </summary>
	public virtual void PlayWalk ()
	{
		
	}

	/// <summary>
	/// 播放技能.
	/// </summary>
	/// <param name="skillNo">Skill no.</param>
	public virtual void PlaySkill (int skillNo)
	{
		
	}

	public virtual void PlayDeath ()
	{
		
	}

	public virtual void PlayIdle ()
	{
		
	}

	//出生动画
	public virtual void PlayAppear ()
	{
		
	}



}


