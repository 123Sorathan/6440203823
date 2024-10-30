using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeetleBaseState
{
    public abstract void EnterState(BeetleController beetle);
    public abstract void UpdateState(BeetleController beetle);
    public abstract void OnTriggerEnter2D(BeetleController beetle, Collider2D other);
    public abstract void OnTriggerExit2D(BeetleController beetle, Collider2D other);
    public abstract void ExitState(BeetleController beetle);
}
