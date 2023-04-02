using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : BaseState
{
    float timer = 0;
    AttackData attackData;

    public WaitState(StateMachine stateMachine, AttackData attackData) : base(stateMachine)
    {
        this.attackData = attackData;
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

        if (timer >= attackData.waitTime)
        {
            stateMachine.ChangeState(stateMachine.GetState<RandomMoveState>());
            return;
        }

        timer += Time.deltaTime;
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }
}
