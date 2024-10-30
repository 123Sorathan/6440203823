using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBaseState 
{
    public abstract void EnterState(GhostController ghost);
    public abstract void UpdateState(GhostController ghost);
    public abstract void OnTriggerEnter2D(GhostController ghost, Collider2D other);
    public abstract void OnTriggerExit2D(GhostController ghost, Collider2D other);
    public abstract void ExitState(GhostController ghost);
}
