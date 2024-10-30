using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GiantWormBaseState
{
    public abstract void EnterState(GiantWormController giant_worm);
    public abstract void UpdateState(GiantWormController giant_worm);
    public abstract void OnTriggerEnter2D(GiantWormController giant_worm, Collider2D other);
    public abstract void OnTriggerExit2D(GiantWormController giant_worm, Collider2D other);
    public abstract void ExitState(GiantWormController giant_worm);
}