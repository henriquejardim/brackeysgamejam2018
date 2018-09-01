using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void ToIntro() {
        GameManager.instance.ToIntro();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
