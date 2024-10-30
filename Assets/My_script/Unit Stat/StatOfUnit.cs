using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatOfUnit : ScriptableObject 
{
  public List<UnitStat> unitStats;
}

[Serializable]
public class UnitStat
{
    public string name;
    public int hp;
    public int attackPower; 
    public float attackRange;
    public float attackCoolDown;
    public float movementSpeed;
    public float movementRange;
}
