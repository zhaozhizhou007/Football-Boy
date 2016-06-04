using UnityEngine;
using System.Collections;

public class EventDefine {

}

public class EventInt : UnityEngine.Events.UnityEvent<int>{}
public class EventString : UnityEngine.Events.UnityEvent<string>{}
public class EventGameObject : UnityEngine.Events.UnityEvent<GameObject>{}
public class EventJoystickDirection : UnityEngine.Events.UnityEvent<EDirection>{}
