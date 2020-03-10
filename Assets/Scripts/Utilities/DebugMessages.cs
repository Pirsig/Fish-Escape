using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public static class DebugMessages
{
    //Generic version, theoretically can be used with anything that has a ToString() method.
    public static void ArrayVariableOutput<T>(Component caller, T[] output, string nameOfOutput, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        string arrayValues = "";
        int index = 0;
        while (index < output.Length)
        {
            if(index == 0)
            {
                arrayValues = output[index].ToString();
                index++;
                continue;
            }
            arrayValues += ", " + output[index].ToString();
            index++;
        }
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": Array " + nameOfOutput + " = " + "{ " + arrayValues + " }");
    }

    public static void MethodInClassDestroyObject(Component caller, GameObject objectDestroyed, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.LogWarning(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + " will attempt to destroy " + objectDestroyed.name);
    }

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

    /*  SimpleVariableOutput()
     *  
     *  Used to indicate the output of one variable in a method, has overloads for various types
     *  !Important!
     *  Use nameOf(variable) in the nameOfOutput variable to have consistency even if the variable name is changed without having to manually change strings*/
     
    //Used to indicate the output of one in variable in a method.
    public static void SimpleVariableOutput(Component caller, int output, string nameOfOutput, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + nameOfOutput + " = " + output.ToString());
    }

    //Used to indicate the output of one float variable in a method.
    public static void SimpleVariableOutput(Component caller, float output, string nameOfOutput, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + nameOfOutput + " = " + output.ToString());
    }

    //Used to indicate the output of one Vector3 variable in a method.
    public static void SimpleVariableOutput(Component caller, Vector3 output, string nameOfOutput, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + nameOfOutput + " = " + output.ToString());
    }

    //Generic version, theoretically can be used with anything that has a ToString() method.
    public static void SimpleVariableOutput<T>(Component caller, T output, string nameOfOutput, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.Log(methodName + " from " + caller.GetType().Name + " in " + caller.gameObject.name + ": " + nameOfOutput + " = " + output.ToString());
    }

    public static void CoroutineStarted(Component caller, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.LogWarning(methodName + " coroutine from " + caller.GetType().Name + " in " + caller.gameObject.name + " has started.");
    }

    public static void CoroutineEnded(Component caller, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
    {
        Debug.LogWarning(methodName + " coroutine from " + caller.GetType().Name + " in " + caller.gameObject.name + " has ended.");
    }

    /*
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
    */
}