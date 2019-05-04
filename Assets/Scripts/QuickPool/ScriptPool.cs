using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace QuickPool {

	public class ScriptPool<T> : IPool<T> where T : new() {

		private int MaxCapacity = -1;
		private Action<T> ResetAction = null;

		private Stack<T> PooledScripts;

		public ScriptPool(int initialCapacity, int maxCapacity, Action<T> resetAction) {
			this.MaxCapacity = maxCapacity;
			this.ResetAction = resetAction;

			this.PooledScripts = new Stack<T>(initialCapacity);

			for(int i = 0; i < initialCapacity; i++) {
				T poolable = new T();
				PooledScripts.Push(poolable);
			}
		}

		public T Get() {
			T poolable = default(T);
			if(PooledScripts.Count > 0) {
				poolable = PooledScripts.Pop();
			} else {
				poolable = new T();
			}

			if(ResetAction != null) {
				ResetAction(poolable);
			}

			return poolable;
		}

		public void Pool(T poolable) {
			if(MaxCapacity > -1 && PooledScripts.Count >= MaxCapacity) {
				return;
			}

			PooledScripts.Push(poolable);
		}

		public void Clear() {
			PooledScripts.Clear();
		}

		public int Count() {
			return PooledScripts.Count;
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
