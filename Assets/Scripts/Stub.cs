using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stub : MonoBehaviour {

	public Driver driver;

	void Update() {
		transform.position += Vector3.right * 100f * Time.deltaTime;
		if(transform.position.x > 50) {
			driver.pool.Pool(this);
		}
	}

}
