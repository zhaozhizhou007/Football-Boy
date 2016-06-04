using UnityEngine;
using System.Collections;
using Common.Enum;
using DG;
using DG.Tweening;

public class HeroController : MonoBehaviour
{

	public BehaviourStateMachine MMachine = new BehaviourStateMachine ();

	public Seeker MSeeker;

	public CharacterController MCharacter;
	public float MSpeed = 5f;

	//--位置信息
	public SGridCoordinate MCurCoordinate = new SGridCoordinate (0, 0);
	//--下一步方位
	public EDirection _NextDirection = EDirection.None;
	//--下一步之后的目标.
	public EDirection _AimDirection = EDirection.None;

	void OnEnable ()
	{
		EventCenter.Event_DirectionInput.AddListener (OnDirectionInput);
		EventCenter.Event_DirectionInputStop.AddListener (OnDirectionStop);
		EventCenter.Event_InputFire.AddListener(OnInputFire);

	}

	void OnDisable ()
	{
		EventCenter.Event_DirectionInput.RemoveListener (OnDirectionInput);
		EventCenter.Event_DirectionInputStop.RemoveListener (OnDirectionStop);
		EventCenter.Event_InputFire.RemoveListener(OnInputFire);
	}

	void Start ()
	{
		MCharacter = GetComponent<CharacterController> ();
		Init();
	}

	void Update ()
	{
	
	}

	void FixedUpdate()
	{
		MMachine.CurState.Reason();
		MMachine.CurState.Act();

	}

	void Init()
	{

		//-----初始化状态机----------

		MMachine = new BehaviourStateMachine();
		HeroIdleState idleState = new HeroIdleState(gameObject);
		MMachine.AddState(idleState);
		HeroMoveState moveState = new HeroMoveState (gameObject);
		MMachine.AddState(moveState);
		HeroKickState kickState = new HeroKickState(gameObject);
		MMachine.AddState(kickState);

		//-----end----------------

		//----初始化角色位置
		MCurCoordinate = new SGridCoordinate (4, 0);
		Vector3 heroPosition = Definition.GridToWorld(MCurCoordinate.x,MCurCoordinate.z);

		//heroPosition.y += MHeroHeight / 2f;
		transform.position = heroPosition;

		//---------------------
	}

	/// <summary>
	/// 获取当前朝向.
	/// </summary>
	/// <returns>The direction.</returns>
	public EDirection CurDirection()
	{
		float angleY = transform.localEulerAngles.y;
		if(angleY > -1f && angleY < 1f)
			return EDirection.Up;

		if(angleY > 89 && angleY < 91)
			return EDirection.Right;

		if(angleY > 179 && angleY < 181)
			return EDirection.Down;

		if(angleY > 269 && angleY < 271)
			return EDirection.Left;
		
		return EDirection.None;

	}

	/// <summary>
	/// 转向指定方向
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void TrunToDirection(EDirection dir)
	{
		Vector3 newEuler = Vector3.zero;
		if(dir == EDirection.Up)
		{
			newEuler.y = 0;
		}
		else if(dir == EDirection.Right)
		{
			newEuler.y = 90;
		}
		else if(dir == EDirection.Down)
		{
			newEuler.y = 180;
		}
		else if(dir == EDirection.Left)
		{
			newEuler.y = 270;
		}

		transform.localEulerAngles = newEuler;
	}

	public void TrunToNext()
	{
		if(_NextDirection != EDirection.None)
		{
			TrunToDirection(_NextDirection);
		}
	}

	#region 移动控制

	/// <summary>
	/// 坐标转移到下一个网格.
	/// </summary>
	public void SetCurToNext()
	{
		MCurCoordinate = Definition.GetNextCoorinate(MCurCoordinate,_NextDirection);
	}

	public void SetNextDirection(EDirection dir)
	{
		_NextDirection = dir;
	}
	public void SetAimToNext()
	{
		_NextDirection = _AimDirection;
	}
	public void SetAimDirection(EDirection dir)
	{
		_AimDirection = dir;
	}

	public void CleanAim ()
	{
		_AimDirection = EDirection.None;
	}
	public void CleanNext()
	{
		_NextDirection = EDirection.None;
	}
	public Vector3 GetNextPosition()
	{

		Vector3 nextPosition = transform.position;
		if(_NextDirection != EDirection.None)
		{
			nextPosition = Definition.GetNextPosition(transform.position,MCurCoordinate,_NextDirection);
		}

		return nextPosition;
	}


	#endregion   


	#region 事件控制.

	/// <summary>
	/// 方向输入控制
	/// </summary>
	/// <param name="dir">Dir.</param>
	private void OnDirectionInput(EDirection dir)
	{

		if(MMachine.CurState is IInput)
		{
			IInput stateInput = MMachine.CurState as IInput;
			if(stateInput != null)
			{
				stateInput.OnDirectionContrl(dir);
			}
		}
	}

	/// <summary>
	/// 控制结束
	/// </summary>
	private void OnDirectionStop()
	{
		CleanAim ();
	}

	/// <summary>
	/// 开火输入.
	/// </summary>
	private void OnInputFire()
	{
		if(MMachine.CurState is IInputFire)
		{
			IInputFire stateInput = MMachine.CurState as IInputFire;
			if(stateInput != null)
			{
				stateInput.OnFire();
			}
		}
	}

	#endregion

	void OnGUI()
	{
		GUI.TextField(new Rect(Screen.width - 100,Screen.height - 100,200,50) , MMachine.CurStateID.ToString());

	}

}
