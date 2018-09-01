using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 8f;
    public GameObject navigationEarth;
    public GameObject navigationMars;
    public float magnitude;


    private Transform earth;
    private Transform mars;
    private Rigidbody2D rb;
    private bool moved;
    private float stoppedTime = 3f;
    private bool dead;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        var vector = rb.transform.right * speed * Time.deltaTime;
        rb.velocity = GameManager.instance.GetPointOnCircunference(Vector2.zero, 1) * speed;
        StartPlay();
    }

    public void StartPlay() {
        navigationEarth.SetActive(true);
        navigationMars.SetActive(false);
    }

    private void Update() {
        if (GameManager.instance.state == GameState.Playing) {
            LookAtEarth();
            if (GameManager.instance.enableMars) {
                navigationMars.SetActive(false);
                LookAtMars();
            }

        }
    }

    private void LookAtEarth() {
        if (earth == null)
            earth = GameObject.FindGameObjectWithTag("Earth").transform;
        if (earth == null) return;
        PointCompass(earth, navigationEarth);
    }

    private void LookAtMars() {
        if (mars == null)
            mars = GameObject.FindGameObjectWithTag("Mars").transform;
        if (mars == null) return;
        PointCompass(mars, navigationMars);
    }

    private void PointCompass(Transform planet, GameObject navigation) {
        Vector3 directionLookAt = planet.position - navigation.transform.position;
        float angleLookAt = Mathf.Atan2(directionLookAt.y, directionLookAt.x) * Mathf.Rad2Deg;
        navigation.transform.rotation = Quaternion.AngleAxis(angleLookAt, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (GameManager.instance.state == GameState.Playing) {
            MoveRigidBody();
            magnitude = rb.velocity.magnitude;

            if (magnitude <= 3f) {
                stoppedTime -= Time.deltaTime;
                if (stoppedTime <= 0f && !dead)
                    GameManager.instance.ChangeState(GameState.DefeatBySingularity);
            }
        }

    }

    private void MoveRigidBody() {
        rb.velocity = speed * (rb.velocity.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Core")){
            var core = collision.GetComponent<NamedCore>();
            GameManager.instance.destroyedBy = new GameManager.DestroyData {
                name = core.coreName,
                type = core.nameType,
                collision = true
            };
            print(GameManager.instance.destroyedBy.name + "DESTROY");
            Debug.Log(collision.GetComponent<NamedCore>().coreName);
            Debug.Log("End!");
            GameManager.instance.ChangeState(GameState.Defeat);
            dead = true;
            rb.velocity = Vector2.zero;
        }

        if (collision.CompareTag("Earth") || collision.CompareTag("Mars")) {
            Debug.Log("Victory!");
            GameManager.instance.ChangeState(GameState.Victory);

        }
    }
}
