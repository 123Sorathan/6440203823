using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PrefabToClone : ScriptableObject
{
    public List<PrefabList> prefabToClone;
}

[Serializable]
public class PrefabList
{
    public string prefabName;
    public GameObject prefabObject;
}
