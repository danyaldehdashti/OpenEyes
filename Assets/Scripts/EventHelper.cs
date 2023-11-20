using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHelper : MonoBehaviour
{
    public UnityEvent action;

    public void InvokeAction()
    {
        action.Invoke();
    }
}
