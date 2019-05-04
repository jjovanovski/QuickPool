using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickPool {

	public interface IPool<T> {

		T Get();
		void Pool(T poolable);
		void Clear();
		int Count();
		int GetMaxCapacity();
		bool IsEmpty();
		bool IsFull();

	}


}