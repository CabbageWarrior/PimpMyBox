﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("BButton-P1") || Input.GetButton("BButton-P2"))
        {
            Destroy(GameObject.Find("GameManager"));

            SceneManager.LoadScene("Intro");
        }
    }
}
