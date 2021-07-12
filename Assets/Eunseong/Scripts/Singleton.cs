using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T instance = null;
	private static object syncObject = new object();

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncObject)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						GameObject obj = new GameObject(typeof(T).Name);
						instance = obj.AddComponent<T>();
					}
				}
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
		}

		DontDestroyOnLoad(this);
	}
}