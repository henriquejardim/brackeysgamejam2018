using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {


    public Text timerText;
    public Text intro1;
    public Text intro2;
    public Text intro3;
    public Text destroyedByText;
    public Text supportText;
    public Scrollbar bar;

    public Image panel;
    public Image end;

    private float timer = 0.0f;
    public decimal totalTime = 13000000000;

    private float c = 0.0f;

    IntroPage introPage;

    // Use this for initialization
    void Start () {

        GameManager.instance.hud = this;

        if (GameManager.instance.state == GameState.Introduction) {
            PlayIntro();
        }

        if (GameManager.instance.state == GameState.Playing ||
            GameManager.instance.state == GameState.Defeat ||
            GameManager.instance.state == GameState.Start) {
            Reveal();
        }

        if (GameManager.instance.state == GameState.Defeat) {
            if (GameManager.instance.destroyedBy.collision) {
                destroyedByText.text = GameManager.instance.destroyedBy.type
                .ToString() + " " + GameManager.instance.destroyedBy.name;
                return;
            }
            supportText.text = "You have failed.";
            destroyedByText.text = GameManager.instance.destroyedBy.name;

        } 
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.state == GameState.Playing)
            if (bar != null)
                bar.value = GameManager.instance.timer;
    }

    IEnumerator Fade(Text text, bool fadeAway) {
        if (fadeAway) {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime) {
                // set color with i as alpha
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
        }
        else {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime) {
                // set color with i as alpha
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
        }
    }

    IEnumerator Fade(Image panel, bool fadeAway, float time = 1.5f) {
        if (fadeAway) {
            // loop over 1 second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime) {
                // set color with i as alpha
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, i);
                yield return null;
            }
        } else {
            // loop over 1 second
            for (float i = 0; i <= time; i += Time.deltaTime) {
                // set color with i as alpha
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, i);
                yield return null;
            }
        }
    }

    public  void Reset() {
        Alpha0(intro1);
        intro1.gameObject.SetActive(false);
        Alpha0(intro2);
        intro2.gameObject.SetActive(false);
        Alpha0(intro3);
        intro3.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }

    public void Alpha0(Text text) {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    enum IntroPage {
        Intro1,
        Intro2,
        Intro3
    }

    public void PlayIntro() {
        Reset();
        panel.gameObject.SetActive(true);
        StartCoroutine(Play());
    }

    public void Reveal() {
        StartCoroutine(Fade(panel, true, 1f));
        StartCoroutine(SetActive(panel.gameObject, false, 1f));
    }

    IEnumerator SetActive(GameObject gameObject, bool setActive, float t) {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
    
    public void GameEnd() {
        end.gameObject.SetActive(true);
        StartCoroutine(Fade(end, false, 1f));
    }

    private void ChangeIntro(IntroPage newIntroPage) {
        switch (newIntroPage) {
            case IntroPage.Intro1:
            intro1.gameObject.SetActive(true);
            StartCoroutine(Fade(intro1, false));
            break;
            case IntroPage.Intro2:
            StartCoroutine(Fade(intro1, true));
            intro2.gameObject.SetActive(true);
            StartCoroutine(Fade(intro2, false));
            break;
            case IntroPage.Intro3:
            StartCoroutine(Fade(intro2, true));
            intro3.gameObject.SetActive(true);
            StartCoroutine(Fade(intro3, false));
            break;
            default:
            break;
        }
    }

    IEnumerator Play() {
        yield return new WaitForSeconds(0.5f);
        ChangeIntro(IntroPage.Intro1);
        yield return new WaitForSeconds(5f);
        ChangeIntro(IntroPage.Intro2);
        yield return new WaitForSeconds(6f);
        ChangeIntro(IntroPage.Intro3);
        yield return new WaitForSeconds(5f);
        StartCoroutine(Fade(intro3, false));
        GameManager.instance.JumpIntro();
    }
}
