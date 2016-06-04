using UnityEngine;
using System.Collections;

/// <summary>
/// 状态列表
/// </summary>
public enum EBehaviourStateID{

	None 		= 0,
	Idle 		= 1,
	Move		= 2,		//移动.
	Roll 		= 3,
	Bound		= 10, 		//被动压制:被击飞/击倒,被束缚 等. 需要其他减益效果支援.
	Kick		= 20, 	//处于攻击,包括技能,而普通攻击也当作技能一样触发.
	Skill		= 30, 
	Awaken		= 40,	//觉醒
	Appear		= 50,		//出场.
	Death		= 90,	//死亡

}
