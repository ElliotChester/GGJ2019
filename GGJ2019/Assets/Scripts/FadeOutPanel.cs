﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutPanel : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(2);
    }


    public void OpenMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
