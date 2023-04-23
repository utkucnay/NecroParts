using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.AI;


public class RandomMoveState : BaseState
{
    RandomMoveData randomMoveData;

    Vector2 loc;

    FollowAttackTargetState followAttackTargetState;
    Rigidbody rb;
    Skeleton skeleton;
    GameObject player;

    public RandomMoveState(StateMachine stateMachine) : base(stateMachine)
    {
        randomMoveData = stateMachine.GetData<RandomMoveData>("randomMoveData");
    }

    public override void InitState()
    {
        base.InitState();
        SetUpRef();
    }

    protected override void StartState()
    {
        base.StartState();
        FindLoc();
    }

    protected override void UpdateState()
    {
        base.UpdateState();
        if (CheckStateTransition()) return;
        GoToLocation();
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }

    void SetUpRef()
    {
        followAttackTargetState = stateMachine.GetState<FollowAttackTargetState>();
        rb = gameObject.GetComponent<Rigidbody>();
        player = Player.s_Instance.gameObject;
        skeleton = gameObject.GetComponent<Skeleton>();
    }
    void FindLoc()
    {
        var dir = Vector2.up;
        dir.Rotate(Random.Range(0, 360));
        loc = player.transform.position + (Vector3)(dir * Random.Range(randomMoveData.moveRangeMin, randomMoveData.moveRangeMax));
        //loc = loc.normalized * Random.Range(3,7);
    }
    void GoToLocation()
    {
        Vector2 dir = loc - (Vector2)transform.position;
        dir = dir.normalized;
        skeleton.MoveDir(dir * randomMoveData.speed);
        skeleton.RotateDir(dir);
    }
    bool CheckStateTransition()
    {
        if (Vector2.Distance(transform.position, loc) <= .75f) stateMachine.ChangeState(this);
        if (Vector2.Distance(loc, player.transform.position) >= 12f)
            stateMachine.ChangeState(this);

        if (randomMoveData.attackTarget)
        {
            if (AttackEnemyInRange())
                return true;
        }

        return false;
    }
    bool AttackEnemyInRange()
    {
        var targetPoints = AIManager.s_Instance.CalculateTargetPointsForPos(transform.position, randomMoveData.attackRange, 10);
        CalculateTargetPoints(targetPoints, Player.s_Instance.targetEnemyPoints);
        int maxValue = int.MinValue, index = -1;

        for (int i = 0; i < targetPoints.Length; i++)
        {
            if (maxValue < targetPoints[i] && targetPoints[i] > 0)
            {
                index = i;
                maxValue = targetPoints[i];
            }
        }

        if (index != -1)
        {
            followAttackTargetState.targetObject = AIManager.s_Instance.GetEnemy(index).gameObject;
            stateMachine.ChangeState(followAttackTargetState);
            return true;
        }

        return false;
    }
    void CalculateTargetPoints(int[] targetPoints, int[] playerTargetPoints)
    {
        int length = targetPoints.Length;

        for (int i = 0; i < length; i++)
        {
            if (playerTargetPoints[i] > 0)
            {
                targetPoints[i] = targetPoints[i] + playerTargetPoints[i];
            }
            else
            {
                targetPoints[i] = 0;
            }
        }
    }
}