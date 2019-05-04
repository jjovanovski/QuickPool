using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickPool;

public class Driver : MonoBehaviour {

	public Stub tmp;

	public ObjectPool<Stub> pool;

	// Use this for initialization
	void Start () {
		pool = Pool.CreateObjectPool<Stub>(tmp, transform, 10, -1, (s) => { s.transform.position = Vector3.zero; });
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			Stub poolable = pool.Get();
			poolable.driver = this;
		}
	}
}
