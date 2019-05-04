using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace QuickPool {

	public class ObjectPool<T> : IPool<T> where T : MonoBehaviour {

		private T PoolableObject;
		private Transform ObjectsParent;
		private int MaxCapacity = -1;
		private Action<T> ResetAction = null;

		private Stack<T> PooledObjects;
		
		public ObjectPool(T poolableObject, Transform objectsParent, int initialCapacity, int maxCapacity, Action<T> resetAction) {
			this.PoolableObject = poolableObject;
			this.ObjectsParent = objectsParent;
			this.MaxCapacity = maxCapacity;
			this.ResetAction = resetAction;

			this.PooledObjects = new Stack<T>(initialCapacity);

			for(int i = 0; i < initialCapacity; i++) {
				T poolable = QuickPool.Pool.Instantiate<T>(PoolableObject, ObjectsParent);
				poolable.gameObject.SetActive(false);
				PooledObjects.Push(poolable);
			}
		}

		public T Get() {
			T poolable  = null;
			if(PooledObjects.Count > 0) {
				poolable = PooledObjects.Pop();
				poolable.gameObject.SetActive(true);
			} else {
                QuickPool.Pool.Instantiate<T>(PoolableObject, ObjectsParent);
			}

			if(ResetAction != null) {
				ResetAction(poolable);
			}

			return poolable;
		}

		public void Pool(T poolable) {
			if(MaxCapacity > -1 && PooledObjects.Count >= MaxCapacity) {
                QuickPool.Pool.Destroy(poolable.gameObject);
				return;	
			}

			poolable.gameObject.SetActive(false);
			PooledObjects.Push(poolable);
		}

		public void Pool(T poolable, bool destroyIfCapacityFull) {
			if(MaxCapacity > -1 && PooledObjects.Count >= MaxCapacity) {
				if(destroyIfCapacityFull) {
                    QuickPool.Pool.Destroy(poolable.gameObject);
				}
				return;	
			}

			poolable.gameObject.SetActive(false);
			PooledObjects.Push(poolable);
		}

		public void Clear() {
			foreach(T poolable in PooledObjects) {
				QuickPool.Pool.Destroy(poolable);
			}
			PooledObjects.Clear();
		}

		public int Count() {
			return PooledObjects.Count;
		}

		public int GetMaxCapacity() {
			return MaxCapacity;
		}

		public bool IsEmpty() {
			return Count() == 0;
		}

		public bool IsFull() {
			return MaxCapacity > -1 && Count() == MaxCapacity;
		}

	}

}
