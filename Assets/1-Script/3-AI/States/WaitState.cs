using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : BaseState
{
    float timer = 0;
    protected float waitTime;
    protected BaseState goState;

    public WaitState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void InitState()
    {
        base.InitState();
        
    }

    protected override void StartState()
    {
        base.StartState();
        timer = 0;
    }

    protected override void UpdateState()
    {
        base.UpdateState();

        if (timer >= waitTime)
        {
            stateMachine.ChangeState(goState);
            return;
        }

        timer += Time.deltaTime;
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }
}
