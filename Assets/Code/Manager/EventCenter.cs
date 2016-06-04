using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 事件中心处理所有广播事件.
/// </summary>
public class EventCenter {


//	public static EventKeyDown Event_KeyDown = new EventKeyDown();
//	public static UnityEvent Event_KeyUp = new UnityEvent ();
	public static UnityEvent Event_HomeEnter = new UnityEvent();


	/// <summary>
	/// 方向输入事件.
	/// </summary>
	public static EventJoystickDirection Event_DirectionInput = new EventJoystickDirection();

	/// <summary>
	/// 方向输入结束.
	/// </summary>
	public static UnityEvent Event_DirectionInputStop = new UnityEvent();


	/// <summary>
	/// 攻击输入
	/// </summary>
	public static UnityEvent Event_InputFire = new UnityEvent();



}
