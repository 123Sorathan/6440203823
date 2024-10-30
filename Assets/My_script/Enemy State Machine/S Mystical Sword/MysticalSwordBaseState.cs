using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MysticalSwordBaseState
{
    public abstract void EnterState(MysticalSwordController mystical_Sword);
    public abstract void UpdateState(MysticalSwordController mystical_Sword);
    public abstract void OnTriggerEnter2D(MysticalSwordController mystical_Sword, Collider2D other);
    public abstract void OnTriggerExit2D(MysticalSwordController mystical_Sword, Collider2D other);
    public abstract void ExitState(MysticalSwordController mystical_Sword);
}
