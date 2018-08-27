using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPooler{

	public int pooledAmount = 20;
	public bool willGrow = true;

	private List<GameObject> pooledObjects;
	public delegate GameObject ObjectCreationDelegate();
	private ObjectCreationDelegate objectCreationMethod;

	public ObjectPooler (ObjectCreationDelegate creator, Transform poolTransform, int pooledAmount = 20, bool willGrow = true)
	{
		objectCreationMethod = creator;
		pooledObjects = new List<GameObject>();
		this.pooledAmount = pooledAmount;
		this.willGrow = willGrow;

		for(int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = objectCreationMethod ();
			obj.transform.SetParent(poolTransform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetObject()
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}    
		}

		if (willGrow)
		{
			GameObject obj = objectCreationMethod();
			obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}

}
