using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected _Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(_Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public virtual void PhysicsUpdate() {}
    public virtual void AnimationTriggerEvent(_Enemy.AnimationTriggerType triggerType) {}
}