using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitcher : MonoBehaviour {

	void SwitchSceneAsync()
    {
        SceneManager.LoadSceneAsync("Intro");
    }
}
