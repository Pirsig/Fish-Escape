using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public static class DebugMessages
{
    //Use to indicate when something has subscribed to an event
    public static void ClassInObjectSubscribed(Component caller, string eventName)
    {
        Debug.Log(caller.GetType().Name + " in " + caller.gameObject.name + " is subscribed to " + eventName + '.');
    }

    //Use to indicate when something has unsubscribed to an event
    public static void ClassInObjectUnsubscribed(Component caller, string eventName)
    {
        Debug.Log(caller.GetType().Name + " in " + caller.gameObject.name + " is unsubscribed to " + eventName + '.');
    }

    //Use to indicate when an event has been invoked in an object
    public static void EventFired(Component caller, string eventName)
    {
        Debug.Log(eventName + " in " + caller.gameObject.name + " has fired.");
    }

    //Use to indicate when a method is called that invokes an event
    public static void OriginatingEventFired(Component caller, string eventName)
    {
        Debug.Log("Originating event " + eventName + " in " + caller.gameObject.name + " has fired.");
    }

    //Use to indicate when a method has tried to do something it cannot do
    public static void ClassInObjectFatalError(Component caller)
    {
        Debug.LogError(caller.GetType().Name + " in " + caller.gameObject.name + " has suffered a fatal error!!!");
    }

    //Use to indicate when a method has tried to do something it cannot do
    //Overload to allow custom error message to follow
    public static void ClassInObjectFatalError(Component caller, string customErrorMessage)
    {
        Debug.LogError(caller.GetType().Name + " in " + caller.gameObject.name + " has suffered a fatal error!!!");
        Debug.LogError(customErrorMessage);
    }

    //Used to indicate the output of one float operation in a method
    public static void SimpleMethodOutput(Component caller, float output, string outputName, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + outputName + " = " + output);
    }

    //Used to indicate the output of one float operation in a method
    public static void SimpleMethodOutput(Component caller, int output, string outputName, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + outputName + " = " + output);
    }

    //Returns the calling method's name
    public static string GetCallingMethod([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
    {
        return memberName;
    }

    //Way to return variable name, need to look into using this properly
    public static string GetVariableName<T>(T item) where T : class
    {
        return typeof(T).GetProperties()[0].Name;
    }
}