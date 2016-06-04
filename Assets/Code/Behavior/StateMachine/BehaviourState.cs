using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourState
{
	
	protected Dictionary<EBehaviourTransition,EBehaviourStateID> TransitionMap = new Dictionary<EBehaviourTransition, EBehaviourStateID> ();
	protected EBehaviourStateID _StateID;
	public EBehaviourStateID ID { get { return _StateID; } }
	private float _TimeElapse = 0;

	/// <summary>
	/// 添加切换的状态.
	/// </summary>
	/// <param name="trans">Trans.</param>
	/// <param name="stateID">State I.</param>
	public void AddTransition (EBehaviourTransition trans, EBehaviourStateID stateID)
	{
		// 不要传空的进来哦.
		if (trans == EBehaviourTransition.None) {
			Debug.LogError ("FSMState ERROR: NullTransition is not allowed for a real transition");
			return;
		}

		if (stateID == EBehaviourStateID.None) {
			Debug.LogError ("FSMState ERROR: NullStateID is not allowed for a real ID");
			return;
		}

		// Since this is a Deterministic FSM,
		// check if the current transition was already inside the map
		if (TransitionMap.ContainsKey (trans)) {
			Debug.LogError ("FSMState ERROR: State " + stateID.ToString () + " already has transition " + trans.ToString () +
			"Impossible to assign to another state");
			return;
		}

		TransitionMap.Add (trans, stateID);

	}

	/// <summary>
	/// 删除转换.
	/// </summary>
	/// <param name="trans">Trans.</param>
	public void DeleteTransition (EBehaviourTransition trans)
	{
		// 不要传空的进来哦.
		if (trans == EBehaviourTransition.None) {
			Debug.LogError ("FSMState ERROR: NullTransition is not allowed for a real transition");
			return;
		}

		// Check if the pair is inside the map before deleting
		if (TransitionMap.ContainsKey (trans)) {
			TransitionMap.Remove (trans);
			return;
		}
		Debug.LogError ("FSMState ERROR: Transition " + trans.ToString () + " passed to " + _StateID.ToString () +
		" was not on the state's transition list");

	}


	/// <summary>
	/// This method returns the new state the FSM should be if
	/// this state receives a transition and 
	/// </summary>
	public EBehaviourStateID GetOutputState (EBehaviourTransition trans)
	{
		// Check if the map has this transition
		if (TransitionMap.ContainsKey (trans)) {
			return TransitionMap [trans];
		}
		return EBehaviourStateID.None;
	}

	/// <summary>
	/// This method is used to set up the State condition before entering it.
	/// It is called automatically by the FSMSystem class before assigning it
	/// to the current state.
	/// </summary>
	public virtual void OnEnterBefore ()
	{ 
		_TimeElapse = 0;
	}

	/// <summary>
	/// 进入之后的参数.
	/// </summary>
	/// <param name="param">Parameter.</param>
	public virtual void OnEnter(object param = null)
	{
		
	}

	/// <summary>
	/// This method is used to make anything necessary, as reseting variables
	/// before the FSMSystem changes to another one. It is called automatically
	/// by the FSMSystem before changing to a new state.
	/// </summary>
	public virtual void OnExit ()
	{ }


	/// <summary>
	/// 入口
	/// </summary>
	public void Reason ()
	{
		// first is 0.
		Reason (_TimeElapse);
		_TimeElapse += Time.deltaTime;
	}


	/// <summary>
	/// This method decides if the state should transition to another on its list
	/// NPC is a reference to the object that is controlled by this class
	/// </summary>
	public abstract void Reason (float timeElapse);

	public void Act()
	{
		Act (_TimeElapse);
	}
	/// <summary>
	/// This method controls the behavior of the NPC in the game World.
	/// Every action, movement or communication the NPC does should be placed here
	/// NPC is a reference to the object that is controlled by this class
	/// </summary>
	public abstract void Act ( float timeElapse);


}
