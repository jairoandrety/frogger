using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public string name;
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;

    public List<GameObject> pooledObjects;

    public PoolItem(string name, int amountToPool, GameObject objectToPool, bool shouldExpand)
    {
        this.name = name;
        this.amountToPool = amountToPool;
        this.objectToPool = objectToPool;
        this.shouldExpand = shouldExpand;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    //public List<string> objects = new List<string>(); 

    public List<PoolItem> poolItems;


    void Awake()
    {
        //SharedInstance = this;
    }

    public void SetPooler(PoolItem item)
    {
        bool check = poolItems.Exists(i => i == item);

        if (!check)
            poolItems.Add(item);

        SetItemPool(item);
    }

    public void SetPooler(List<PoolItem> items)
    {
        poolItems = items;
        //poolItems.AddRange(items);

        foreach (PoolItem item in poolItems)
        {
            SetItemPool(item);
            //
        }
    }

    private void SetItemPool(PoolItem item)
    {
        for (int i = 0; i < item.amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            item.pooledObjects = new List<GameObject>();
            item.pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(string value)
    {
        PoolItem itemSelected = poolItems.Find(i => i.name == value);
        if(itemSelected != null)
        {
            for (int i = 0; i <itemSelected.pooledObjects.Count; i++)
            {
                if (!itemSelected.pooledObjects[i].activeInHierarchy)
                {
                    return itemSelected.pooledObjects[i];
                }
            }

            foreach (PoolItem item in poolItems)
            {
                if (item.objectToPool.tag == tag)
                {
                    if (item.shouldExpand)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        item.pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
        }


        return null;
    }

    public void ResetPooler()
    {
        for (int i = 0; i < poolItems.Count ; i++)
        {
            for (int j = 0; j < poolItems[i].pooledObjects.Count; j++)
            {
                poolItems[i].pooledObjects[i].SetActive(false);
            }

        }
    }
}