using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeletonBaseState
{
    public abstract void EnterState(SkeletonController skeleton);
    public abstract void UpdateState(SkeletonController skeleton);
    public abstract void OnTriggerEnter2D(SkeletonController skeleton, Collider2D other);
    public abstract void OnTriggerExit2D(SkeletonController skeleton, Collider2D other);
    public abstract void ExitState(SkeletonController skeleton);
}