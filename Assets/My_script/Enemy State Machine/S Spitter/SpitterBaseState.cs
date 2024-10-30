using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpitterBaseState
{
    public abstract void EnterState(SpitterController spitter);
    public abstract void UpdateState(SpitterController spitter);
    public abstract void OnTriggerEnter2D(SpitterController spitter, Collider2D other);
    public abstract void OnTriggerExit2D(SpitterController spitter, Collider2D other);
    public abstract void ExitState(SpitterController spitter);
}
