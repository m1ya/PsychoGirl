using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMove : MonoBehaviour {

	GameObject Player;
	public GameObject Field1;
	public GameObject Field2;
	int border;
	bool fieldNumber1;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		border = 2880;
		fieldNumber1 = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Player.transform.position.z > border) {
			if (fieldNumber1 == false) {
				Field1.transform.position += new Vector3 (0, 0, 3840);
			} else {
				Field2.transform.position += new Vector3 (0, 0, 3840);
			}
			border += 1920;
			fieldNumber1 = !fieldNumber1;
		}
		
	}
}
