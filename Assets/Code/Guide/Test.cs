using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Test : MonoBehaviour {


	public List<GameObject> Os;

	Seeker MSeeker ;
	CharacterController Mcc ;
	public Transform target;
	public Path path;
	public float speed = 20f;
	public int currentWaypoint = 0;
	public float nextWaypointDis = 0.5f;
	public AstarPath Astar;
	void Start () 
	{
		Mcc = GetComponent<CharacterController>();
		MSeeker = GetComponent<Seeker>();

	}

	void OnEnable()
	{
		TouchManager.I.On_TapOne += DelegateTap;
	}

	void Update () 
	{

		if(path == null)
			return;

		if(currentWaypoint >= path.vectorPath.Count)
			return;

		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

		dir = dir * speed;// * Time.deltaTime;
		Mcc.SimpleMove(dir);
		float dis = Vector3.Distance(transform.position,path.vectorPath[currentWaypoint]) ;
		if(dis < nextWaypointDis)
		{
			currentWaypoint  ++;
			return;
		}


	}

	void DelegateTap (GameObject clickObject,Vector3 position)
	{
		//Astar.Scan();
		MSeeker.StartPath(transform.position,position,OnpathEnd);

	}
	void OnpathEnd(Pathfinding.Path p)
	{
		if(p.error)
			return;
		Debug.Log(p.vectorPath.Count);

		path = p;
		currentWaypoint = 0;
//		foreach(var one in path.vectorPath)
//		{
//			Debug.Log(one);
//
//		}

		//path.path[0].position;
		//Debug.Log(path.duration);
	}
	void OnDisable()
	{
		TouchManager.I.On_TapOne -= DelegateTap;
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,100,50),"DO"))
		{
			foreach(var one in Os)
			{
				if(one && one.activeInHierarchy == false)
				{
					one.SetActive(true);
					Astar.Scan();
					break;

				}

			}
		}


	}


}
