using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonSM : StateMachine
{
    public RandomMoveState randomMove;
    public FollowAttackTargetState followAttackTargetState;
    public AttackTargetState attackTargetState;
    public SpanWaitTime spawnWaitState;
    public AttackWaitState attackWaitState;

    protected override void Awake()
    {
        base.Awake();

        randomMove = new RandomMoveState(this);
        followAttackTargetState = new FollowAttackTargetState(this);
        attackTargetState = new AttackTargetState(this);
        spawnWaitState = new SpanWaitTime(this);
        attackWaitState = new AttackWaitState(this);

        states.Add(randomMove);
        states.Add(followAttackTargetState);
        states.Add(attackTargetState);
        states.Add(spawnWaitState);
        states.Add(attackWaitState);

        InitAllStates();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override BaseState GetInitialState()
    {
        return spawnWaitState;
    }


}