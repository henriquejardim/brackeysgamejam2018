using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalAura : MonoBehaviour {

    public GameObject center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Player")) {
    //        Debug.Log("Enter");

    //        var player = collision.gameObject;
    //        var distance =  Vector2.Distance(player.transform.position, center.transform.position);
    //        var degrees = 5 - 4 * distance;

    //        player.transform.eulerAngles = new Vector3(0f, 0f, player.transform.eulerAngles.z + degrees);

    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision) {
        
    //    if (collision.gameObject.CompareTag("Player")) {
    //        Debug.Log("Stay");
    //        var player = collision.gameObject;
    //        var distance = Vector2.Distance(player.transform.position, center.transform.position);
    //        var degrees = 5 - 4 * distance;

    //        player.transform.eulerAngles = new Vector3(0f, 0f, player.transform.eulerAngles.z + degrees);

    //    }
    //}
}
