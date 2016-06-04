using UnityEngine;
using System.Collections;

/// <summary>
/// 单例模式,不销毁
/// </summary>
public class SingletonCom<T> : MonoBehaviour where T : Component
{
	const string SingletonRoot = "GameManager";
	private static T _instance;

	public static T I {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType (typeof(T)) as T;
				if (_instance == null) {
					GameObject obj = new GameObject (typeof(T).Name);
					//该物体将不能保存到场景。当一个新的场景被加载，它也不会被销毁.
					//obj.hideFlags = HideFlags.DontSave;
					//obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent (typeof(T)) as T;
					//将他们都归类在管理器空物体下.
					GameObject m = GameObject.Find ("GameManager");
					if(m != null)
						_instance.transform.parent = m.transform;
					
				}
			}
			return _instance;
		}
	}

	public virtual void Awake ()
	{

		DontDestroyOnLoad (this.gameObject);
		if (_instance == null) {
			_instance = this as T;
		} 

	}

	public virtual void Init()
	{

	}

}
