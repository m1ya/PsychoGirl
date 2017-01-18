using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour {

	public Rigidbody _rigidbody;
	public GameObject player;

	// Use this for initialization
	void Start () {
		_rigidbody.AddForce (Vector3.back * 300);
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.z - this.transform.position.z > 10)
			this.transform.position = new Vector3(Random.Range(-1,1),0, player.transform.position.z + 4 * 12 / 3 * PlayerLevel.speed * PlayerLevel.NeedTime);
	
		if (this.transform.position.z - player.transform.position.z < PlayerLevel.NeedTime * PlayerLevel.speed * 3)
		if (this.transform.position.y > 0.5) {
			_rigidbody.constraints = RigidbodyConstraints.None;
			_rigidbody.AddForce (Vector3.back * 300);
		}
	}

}
