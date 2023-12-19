using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeDict
{
    [SerializeField]
    public List<ResourceType> items;
    [SerializeField]
    public GameObject outputPrefab;
}
