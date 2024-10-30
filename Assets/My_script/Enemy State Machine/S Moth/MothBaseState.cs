using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MothBaseState 
{
    public abstract void EnterState(MothController moth);
    public abstract void UpdateState(MothController moth);
    public abstract void OnTriggerEnter2D(MothController moth, Collider2D other);
    public abstract void OnTriggerExit2D(MothController moth, Collider2D other);
    public abstract void ExitState(MothController moth);
}
