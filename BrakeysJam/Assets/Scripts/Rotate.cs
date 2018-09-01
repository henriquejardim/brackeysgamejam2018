using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 1f;
 
	void Update () {
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * speed, Space.Self);

    }
}
