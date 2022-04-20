using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberOfDropsChance{ 
    public int numberOfDrops;
    public int numberOfEntries;
}

public class EnemyDrops : MonoBehaviour
{
    public ItemPool itemPool;
    public NumberOfDropsChance[] numberOfDrops;

    private List<GameObject> itemsWithEntries = new List<GameObject>();

    private List<int> numberOfDropsChance = new List<int>();

    void Start () {
        foreach (GameObjectPool gOPool in itemPool.pool) {
            for (int i = 0; i < gOPool.numberOfEntries; i++) {
                itemsWithEntries.Add(gOPool.item);
            }
        }
        foreach (NumberOfDropsChance nODC in numberOfDrops) {
            for (int i = 0; i < nODC.numberOfEntries; i++) {
                numberOfDropsChance.Add(nODC.numberOfDrops);
            }
        }
    }

    public void RandomiseDrops() {
        int rndNumberOfDrops = numberOfDropsChance[Random.Range(0, numberOfDropsChance.Count)];
        for (int i = 0; i < rndNumberOfDrops; i++) {
            Instantiate(RandomItem(), this.gameObject.transform.position, Quaternion.identity);
        }
    }

    public GameObject RandomItem() {
        return itemsWithEntries[Random.Range(0, itemsWithEntries.Count)];
    }
}
