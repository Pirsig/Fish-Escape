using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugMessages
{
    public static void ClassInObjectSubscribed(Component caller, string eventName)
    {
        Debug.Log(caller.GetType().Name + " in " + caller.gameObject.name + " is subscribed to " + eventName + '.');
    }

    public static void ClassInObjectUnsubscribed(Component caller, string eventName)
    {
        Debug.Log(caller.GetType().Name + " in " + caller.gameObject.name + " is unsubscribed to " + eventName + '.');
    }

    public static void EventFired(Component caller, string eventName)
    {
        Debug.Log(eventName + " in " + caller.gameObject.name + " has fired.");
    }

    public static void OriginatingEventFired(Component caller, string eventName)
    {
        Debug.Log("Originating event " + eventName + " in " + caller.gameObject.name + " has fired.");
    }
}