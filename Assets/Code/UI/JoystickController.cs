using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour 
{

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Button_Click(int key)
	{
		
		EDirection dir = (EDirection)key;
		EventCenter.Event_DirectionInput.Invoke(dir);
		//Debug.Log(dir);
	}

}
