using UnityEngine;
using System.Collections;
using Common.Enum;
//using DG.Tweening;

/// <summary>
/// 普通待机状态机.
/// </summary>
public class HeroBaseState : BehaviourState {

	//本体
	public GameObject Noumenon;
	//换体
	public Transform NoumenonT;

	public HeroController MHeroController ;

	public HeroAnimatorController MHeroAnimator;

	public BehaviourStateMachine MMachine;

	public CharacterController MCharacter;

	public HeroBaseState (GameObject noum) 
	{
		if(noum != null)
		{
			Noumenon = noum;
			NoumenonT = noum.transform;
			MHeroController = noum.GetComponent<HeroController>();
			MMachine = MHeroController.MMachine;
			MCharacter = noum.GetComponent<CharacterController>();
			MHeroAnimator = noum.GetComponent<HeroAnimatorController>();
		}

	}

	public override void Reason (float timeElapse)
	{
	}

	public override void Act (float timeElapse)
	{
	}


}
