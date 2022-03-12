﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    public class InventoryController : MonoBehaviour
    {
        //Also from Rhys' Collection, although Unity didn't like me adding headers into this script for some reason - || - Redundant within this project, however another script I use requires it, so I just transferred it to this project rather than editing out the dependency

        [System.Serializable]
        public class InventoryEvent
        {
            public string key;
            public UnityEvent OnAdd, OnRemove;
        }
        [System.Serializable]
        public class InventoryChecker
        {

            public string[] inventoryItems;
            public UnityEvent OnHasItem, OnDoesNotHaveItem;

            public bool CheckInventory(InventoryController inventory)
            {

                if (inventory != null)
                {
                    for (var i = 0; i < inventoryItems.Length; i++)
                    {
                        if (!inventory.HasItem(inventoryItems[i]))
                        {
                            OnDoesNotHaveItem.Invoke();
                            return false;
                        }
                    }
                    OnHasItem.Invoke();
                    return true;
                }
                return false;
            }
        }


        public InventoryEvent[] inventoryEvents;

        HashSet<string> inventoryItems = new HashSet<string>();

        public void AddItem(string key)
        {
            if (!inventoryItems.Contains(key))
            {
                var ev = GetInventoryEvent(key);
                if (ev != null) ev.OnAdd.Invoke();
                inventoryItems.Add(key);
            }
        }

        public void RemoveItem(string key)
        {
            if (inventoryItems.Contains(key))
            {
                var ev = GetInventoryEvent(key);
                if (ev != null) ev.OnRemove.Invoke();
                inventoryItems.Remove(key);
            }
        }

        public bool HasItem(string key)
        {
            return inventoryItems.Contains(key);
        }

        public void Clear()
        {
            inventoryItems.Clear();
        }

        InventoryEvent GetInventoryEvent(string key)
        {
            foreach (var iv in inventoryEvents)
            {
                if (iv.key == key) return iv;
            }
            return null;
        }

    }

