using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace QuickPool {

	public class Pool : MonoBehaviour {

		private static Pool Instance = null;

		public static void Init() {
			if(Instance == null) {
				GameObject parent = new GameObject();
				Instance = parent.AddComponent<Pool>();
				DontDestroyOnLoad(Instance);
			}
		}

		public static Pool GetInstance() {
			return Instance;
		}

		public static ObjectPool<T> CreateObjectPool<T>(T poolableObject, Transform objectsParent, int initialCapacity, int maxCapacity = -1, Action<T> resetAction = null) where T : MonoBehaviour {
			Init();
			return new ObjectPool<T>(poolableObject, objectsParent, initialCapacity, maxCapacity, resetAction);
		}

	}

}
