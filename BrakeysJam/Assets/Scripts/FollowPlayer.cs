using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
       offset = player.position - transform.position;
       offset.z = transform.position.z;

    }

    // Update is called once per frame
    void Update () {
      
    }

    private void FixedUpdate() {
        var newPosition = player.position + offset;
        var v = Vector3.Lerp(transform.position, newPosition, 0.125f);
        transform.position = v;
    }

}
