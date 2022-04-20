using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectPool {
    public GameObject item;
    public int numberOfEntries;
}

[CreateAssetMenu(fileName = "New Item Pool", menuName = "Pools/ItemPool")]
public class ItemPool : ScriptableObject
{
    public GameObjectPool[] pool;
}
