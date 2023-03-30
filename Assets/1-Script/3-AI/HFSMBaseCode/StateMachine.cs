using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    protected List<BaseState> states;

    protected virtual void Awake()
    {
        states = new List<BaseState>();
    }

    protected virtual void Start()
    {
        currentState = GetInitialState();
    }

    void Update()
    {
        if (currentState != null)
            currentState.Execution();
    }

    protected void InitAllStates()
    {
        foreach (var state in states)
        {
            state.InitState();
        }
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    public void ChangeState(BaseState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public T GetState<T>() where T : BaseState
    {
        foreach (var state in states)
        {
            if (state is T)
            {
                return (T)state;
            }
        }

        Debug.LogError("Not Found State");
        return null;
    }

    public List<T> GetStates<T>() where T : BaseState
    {
        List<T> states = new List<T>();

        foreach (var state in states)
        {
            if (state is T)
            {
                states.Add(state);
            }
        }

        return states;
    }
}