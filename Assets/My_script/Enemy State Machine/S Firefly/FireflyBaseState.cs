using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireflyBaseState
{
    public abstract void EnterState(FireflyController firefly);
    public abstract void UpdateState(FireflyController firefly);
    public abstract void OnTriggerEnter2D(FireflyController firefly, Collider2D other);
    public abstract void OnTriggerExit2D(FireflyController firefly, Collider2D other);
    public abstract void ExitState(FireflyController firefly);
}
