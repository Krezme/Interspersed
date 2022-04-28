using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorTurnOnOnStart : MonoBehaviour
{
    
    
    [Header(" ")]
    [Header("                                                 ---===== Or is it =====---")] // ! Georgi Aleksandrov
    [Header("                                            ---===== Written by Rhys =====---")]
    
    public UnityEvent OnEnter;

    public void Start() {
        ExecuteOnEnter();
    }

    protected virtual void ExecuteOnEnter()
    {
        OnEnter.Invoke();
    }
}