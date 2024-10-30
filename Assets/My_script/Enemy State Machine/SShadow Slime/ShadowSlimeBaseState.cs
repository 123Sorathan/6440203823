using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowSlimeBaseState 
{
    public abstract void EnterState(ShadowSlimeController shadowSlime);
    public abstract void UpdateState(ShadowSlimeController shadowSlime);
    public abstract void OnTriggerEnter2D(ShadowSlimeController shadowSlime, Collider2D other);
    public abstract void OnTriggerExit2D(ShadowSlimeController shadowSlime, Collider2D other);
    public abstract void ExitState(ShadowSlimeController shadowSlime);
}
