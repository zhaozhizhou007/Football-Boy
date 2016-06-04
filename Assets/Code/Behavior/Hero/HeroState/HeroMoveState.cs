
using UnityEngine;
using System.Collections;
using Common.Enum;

/// <summary>
/// 普通待机状态机.
/// </summary>
public class HeroMoveState : HeroBaseState,IInput {


	private Vector3 aimPosition;

	public HeroMoveState (GameObject hero) : base (hero)
	{

		base._StateID = EBehaviourStateID.Move;
		this.AddTransition (EBehaviourTransition.ToSkill,EBehaviourStateID.Skill);

		base.AddTransition (EBehaviourTransition.ToIdle, EBehaviourStateID.Idle);
		base.AddTransition (EBehaviourTransition.ToKick, EBehaviourStateID.Kick);

	}

	public override void OnEnter (object param)
	{
		base.OnEnter (param);
		aimPosition = MHeroController.GetNextPosition();
		//NoumenonT.forward = NoumenonT.position.MDirectionIgnoreY(aimPosition);
		MHeroAnimator.PlayRun();

	}
		
	public override void Reason (float timeElapse)
	{
		base.Reason (timeElapse);
		//---没有下一步就停下
		if(MHeroController._NextDirection == EDirection.None)
		{
			MMachine.PerformTransition(EBehaviourTransition.ToIdle);
			return;
		}

		if (NoumenonT.position.MDistanceIgnoreY (aimPosition) < 0.08f) 
		{
			//--
			MHeroController.SetCurToNext();
			MHeroController.CleanNext();

			if(MHeroController._AimDirection != EDirection.None)
			{
				bool canMove = MissionManager.I.CanMove(MHeroController.MCurCoordinate,MHeroController._AimDirection);
				if(canMove)
				{
					MHeroController.SetAimToNext();
					aimPosition = MHeroController.GetNextPosition();
				}

				MHeroController.CleanAim();
				MHeroController.TrunToNext();
			}
		}
	}

	public override void Act(float timeElapse)
	{
		
		NoumenonT.position = Vector3.MoveTowards(NoumenonT.position,aimPosition,Time.deltaTime * MHeroController.MSpeed);

	}

	public void OnDirectionContrl (EDirection dir)
	{
		MHeroController.SetAimDirection(dir);
	}

	public void OnDirectionStop ()
	{
		MHeroController.CleanAim();
	}

}
