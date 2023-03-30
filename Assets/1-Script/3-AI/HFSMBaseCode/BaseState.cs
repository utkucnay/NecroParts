using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseState
{
    enum Stage
    {
        Start,
        Update,
        OnEnd
    }

    protected StateMachine stateMachine;
    public Transform transform;
    public GameObject gameObject;

    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        transform = stateMachine.transform;
        gameObject = stateMachine.gameObject;

        StartStateEvent = new UnityEvent();
        UpdateStateEvent = new UnityEvent();
        OnEndStateEvent = new UnityEvent();
    }

    Stage stage = Stage.Start;

    UnityEvent StartStateEvent;
    UnityEvent UpdateStateEvent;
    UnityEvent OnEndStateEvent;

    public void Execution()
    {
        switch (stage)
        {
            case Stage.Start:
                StartState();
                stage = Stage.Update;
                goto case Stage.Update;
            case Stage.Update:
                UpdateState();
                break;
            case Stage.OnEnd:
                OnEnd();
                break;
        }
    }

    public virtual void InitState() { }
    protected virtual void StartState() { StartStateEvent.Invoke(); }
    protected virtual void UpdateState() { UpdateStateEvent.Invoke(); }
    protected virtual void OnEnd() { OnEndStateEvent.Invoke(); }

    public void ExitState()
    {
        stage = Stage.OnEnd;
        Execution();
    }

    public void EnterState()
    {
        stage = Stage.Start;
    }

    public void AddStartStateEvent(UnityAction action)
    {
        StartStateEvent.AddListener(action);
    }

    public void AddUpdateStateEvent(UnityAction action)
    {
        UpdateStateEvent.AddListener(action);
    }
    public void AddOnEndStateEvent(UnityAction action)
    {
        OnEndStateEvent.AddListener(action);
    }
}