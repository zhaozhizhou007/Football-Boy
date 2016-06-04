using UnityEngine;
using System.Collections;

/// <summary>
/// 状态的转换.
/// </summary>
public enum EBehaviourTransition  {

	None		 	= 0,
	ToAppear		= 1,
	ToIdle			= 2,
	ToWalk			= 3,
	ToMove		 	= 4,		//开始移动.
	ToRoll			= 5,		//翻滚.
	ToBound			= 10,
	ToKick		    = 20,		//踢球状态.
	ToSkill			= 30,
	ToAwake			= 40,		//觉醒
	ToDeath			= 90,		//死亡.

}
