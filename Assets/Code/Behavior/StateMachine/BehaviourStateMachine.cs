using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourStateMachine {

	private List<BehaviourState> _States;

	// The only way one can change the state of the FSM is by performing a transition
	// Don't change the CurrentState directly
	private EBehaviourStateID _CurStateID;
	public EBehaviourStateID CurStateID{ get { return _CurStateID; } } 

	private BehaviourState _CurState;
	public BehaviourState CurState{ get { return _CurState; }}

	public BehaviourStateMachine ()
	{
		_States = new List<BehaviourState> ();
	}

	/// <summary>
	/// This method places new _States inside the FSM,
	/// or prints an ERROR message if the state was already inside the List.
	/// First state added is also the initial state.
	/// </summary>
	public void AddState(BehaviourState s)
	{

		if (s == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
		}

		// First State inserted is also the Initial state,
		// the state the machine is in when the simulation begins
		if (_States.Count == 0)
		{
			_States.Add(s);
			_CurState = s;
			_CurStateID = s.ID;
			_CurState.OnEnter ();
			return;
		}

		// Add the state to the List if it's not inside it
		foreach (BehaviourState state in _States)
		{
			if (state.ID == s.ID)
			{
				Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() + 
					" because state has already been added");
				return;
			}
		}
		_States.Add(s);

	}

	/// <summary>
	/// 查找指定的状态.
	/// </summary>
	/// <returns>The state.</returns>
	/// <param name="stateID">State I.</param>
	public BehaviourState FindState(EBehaviourStateID stateID)
	{
		return _States.Find ((state) =>{
			return state.ID == stateID;
		});

	}

	/// <summary>
	/// This method delete a state from the FSM List if it exists, 
	///   or prints an ERROR message if the state was not on the List.
	/// </summary>
	public void DeleteState(EBehaviourStateID id)
	{
		// Check for NullState before deleting
		if (id == EBehaviourStateID.None)
		{
			Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
			return;
		}

		// Search the List and delete the state if it's inside it
		foreach (BehaviourState state in _States)
		{
			if (state.ID == id)
			{
				_States.Remove(state);
				return;
			}
		}

		Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() + 
			". It was not on the list of states");
	}


	public bool CanTransition(EBehaviourTransition trans)
	{
		// Check for NullTransition before changing the current state
		if (trans == EBehaviourTransition.None)
		{
			Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
			return false;
		}

		EBehaviourStateID id = _CurState.GetOutputState (trans);

		if(id == EBehaviourStateID.None)
		{
//			Debug.LogWarning("FSM ERROR: State " + _CurStateID.ToString() +  " does not have a target state " + 
//				" for transition " + trans.ToString());
			return false;
		}
		return true;
	}

	/// <summary>
	/// 执行转换，外部调用关键方法。
	/// </summary>
	/// <param name="trans">Trans.</param>
	public void PerformTransition(EBehaviourTransition trans, object param = null)
	{
		if (!CanTransition (trans))
		{
			return;
		}

		_CurStateID = _CurState.GetOutputState (trans);
		foreach(BehaviourState state in _States)
		{
			if(state.ID == _CurStateID)
			{
				_CurState.OnExit ();
				_CurState = state;
				_CurState.OnEnterBefore ();
				_CurState.OnEnter (param);
				break;
			}

		}

	}

}
