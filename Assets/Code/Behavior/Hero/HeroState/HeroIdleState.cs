using UnityEngine;
using System.Collections;
using Common.Enum;
//using DG.Tweening;

/// <summary>
/// 普通待机状态机.
/// </summary>
public class HeroIdleState : HeroBaseState ,IInput,IInputFire{

	public HeroIdleState (GameObject noum) : base (noum)
	{
		base._StateID = EBehaviourStateID.Idle;
		base.AddTransition (EBehaviourTransition.ToMove,	EBehaviourStateID.Move);
		base.AddTransition (EBehaviourTransition.ToKick,	EBehaviourStateID.Kick);
	}

	public override void OnEnter (object param)
	{
		base.OnEnter (param);
		MHeroAnimator.PlayIdle();
		//BAnimator.PlayIdle ();
	}

	public override void Reason(float timeElapse)
	{

	}

	public override void Act (float timeElapse)
	{
		
	}

	public void OnDirectionContrl (EDirection dir)
	{

		bool canMove = MissionManager.I.CanMove(MHeroController.MCurCoordinate,dir);

		//---转向方向;
		MHeroController.TrunToDirection(dir);
		if(canMove)
		{
			MHeroController.SetNextDirection(dir);
			MMachine.PerformTransition(EBehaviourTransition.ToMove);
		}

	}

	public void OnDirectionStop ()
	{
		
	}


	public void OnFire ()
	{
		MMachine.PerformTransition(EBehaviourTransition.ToKick);
	}
}
