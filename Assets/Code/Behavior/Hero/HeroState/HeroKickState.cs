using UnityEngine;
using System.Collections;
using Common.Enum;
//using DG.Tweening;

/// <summary>
/// 普通待机状态机.
/// </summary>
public class HeroKickState : HeroBaseState {

	private float _Duration = 0.499f;
	private float _KickTimming = 0.26f;

	public HeroKickState (GameObject noum) : base (noum)
	{
		base._StateID = EBehaviourStateID.Kick;
		base.AddTransition (EBehaviourTransition.ToIdle,EBehaviourStateID.Idle);
	}

	public override void OnEnter (object param)
	{
		base.OnEnter (param);
		_Duration = 0.499f;
		_KickTimming = 0.26f;
		MHeroAnimator.PlayKick();
	}

	public override void Reason(float timeElapse)
	{
		if(timeElapse > _Duration)
		{
			MMachine.PerformTransition(EBehaviourTransition.ToIdle);
		}
	}

	public override void Act (float timeElapse)
	{
		if(timeElapse > _KickTimming)
		{
			_KickTimming = float.MaxValue;
			_NowKick();

		}
	}

	private void _NowKick()
	{
		EDirection curDirection = MHeroController.CurDirection();
		SGridCoordinate forwardCoor = Definition.GetNextCoorinate(MHeroController.MCurCoordinate,curDirection);
		MissionManager.I.KickBackBall(MHeroController.MCurCoordinate, forwardCoor);

	}

}
