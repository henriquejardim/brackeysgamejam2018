using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public GameObject starPrefab;
    public GameObject universe;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetMouseButtonDown(0) && GameManager.instance.state == GameState.Playing) {

            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0f;

            var star = Instantiate(starPrefab, point, Quaternion.identity, universe.transform);
            audioSource.Play();
        }
	}
}
