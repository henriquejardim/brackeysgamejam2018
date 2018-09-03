
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public struct DestroyData {
        public string name;
        public NamedCore.NameType type;
        public bool collision;
        public bool stop;
        public bool time;
    }
    public static GameManager instance = null;

    [Header("Quantity Settings")]
    public float numberOfStars = 100f;
    public float numberOfSuns = 100f;
    public float numberOfBlackHoles = 30f;

    [Header("Prefabs")]
    public GameObject earthPrefab;
    public GameObject marsPrefab;
    public GameObject star;
    public GameObject sun;
    public GameObject blackHole;
    public GameObject universe;
    public GameState state;

    public HudController hud;

    public DestroyData destroyedBy;
    public Camera main;
    public float timer = 0f;
    public bool enableMars = false;
    public GameObject mars;
    public AudioSource audioSource;
    public AudioClip audioSong;
  

    float lerpTime = 3f;
    float currentLerpTime;

    void Init() {
        hud = FindObjectOfType<HudController>();
        universe = GameObject.FindGameObjectWithTag("Universe");
    }

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
    }

    void Start() {
        Init();
        ChangeState(state);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update() {

        if (state == GameState.Introduction) {
            if (Input.GetMouseButtonDown(0))
                JumpIntro();
        }

        if (Input.GetKeyUp(KeyCode.R))
            Restart();

        if (state == GameState.DefeatBySingularity) {
            destroyedBy = new GameManager.DestroyData {
                name = "The singularity has broken, The Universe no longer exists.",
                stop = true
            };
            LerpCamera();
        }

        if (state == GameState.Playing) {
            timer += 1f / 60f * Time.deltaTime;
            //if (timer >= 0.75f) {
            //    enableMars = true;
            //}

            if (timer >= 1f) {
                destroyedBy = new GameManager.DestroyData {
                    name = "Mankind has been extinguished without the knowledge of its origins.",
                    stop = true
                };
                LerpCamera();
            }
        }
        
    }

    private void LerpCamera() {
        if (!lerp) {
            currentLerpTime = 0f;
        }

        lerp = true;
        if (lerp) {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
                Camera.main.orthographicSize = 7.2f;
                lerp = false;
                ChangeState(GameState.Defeat);
            }
            float perc = currentLerpTime / lerpTime;
            Camera.main.orthographicSize = Mathf.Lerp(7.2f, 72f, perc);
        }
    }

    bool lerp = false;

    private void Restart() {
        SceneManager.LoadScene(0);
        Init();
    }

    public void ChangeState(GameState newState) {
        state = newState;
        enableMars = false;

        print(newState);

        switch (newState) {
            case GameState.Start:
            return;
            case GameState.Introduction:
                PlayIntroduction();
            break;
            case GameState.Playing:
            SceneManager.LoadScene("Game");
            return;
            case GameState.Victory:
            SceneManager.LoadScene("Victory");
            break;
            case GameState.Defeat:
            StartCoroutine(Defeated());
            break;
            case GameState.DefeatBySingularity:
            case GameState.Pause:
            break;
            default:
            break;
        }

    }

    private void PlayIntroduction() {
        SceneManager.LoadScene("Introduction");
        audioSource.clip = audioSong;
        audioSource.volume = 0.6f;
        audioSource.Play();
        Init();
    }

    public Vector2 GetPointOnCircunference(Vector2 point, float radius = 1f) {
        var random = Random.Range(0f, Mathf.PI * 2f);

        return point + new Vector2(Mathf.Sin(random), Mathf.Cos(random)).normalized * radius;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        Init();

        if (state == GameState.Playing)
            GameStart();
    }

    public void GameStart() {
        timer = 0f;
        for (int i = 0; i < numberOfStars; i++) {
            var point = Random.insideUnitCircle.normalized * Random.Range(25f, 150f);
            Instantiate(star, point, Quaternion.identity, universe.transform);
        }

        for (int i = 0; i < numberOfSuns; i++) {
            var point = Random.insideUnitCircle.normalized * Random.Range(38f, 150f);
            Instantiate(sun, point, Quaternion.identity, universe.transform);
        }

        for (int i = 0; i < numberOfBlackHoles; i++) {
            var point = Random.insideUnitCircle.normalized * Random.Range(70f, 200f);
            Instantiate(blackHole, point, Quaternion.identity, universe.transform);
        }

        var pointEarth = GetPointOnCircunference(Vector2.zero, 110f);
        Instantiate(earthPrefab, pointEarth, Quaternion.identity, universe.transform);

        var pointMars = GetPointOnCircunference(pointEarth, 50f);
        mars = Instantiate(marsPrefab, pointMars, Quaternion.identity, universe.transform);
    }

    public void ToIntro() {
        ChangeState(GameState.Introduction);
    }

    public void JumpIntro() {
        ChangeState(GameState.Playing);
    }

    IEnumerator Defeated() {
        hud.GameEnd();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Defeat");
    }
}

public enum GameState {
    Start,
    Introduction,
    Playing,
    Victory,
    Defeat,
    DefeatBySingularity,
    Pause
}
