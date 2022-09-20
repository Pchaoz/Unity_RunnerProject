using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keyboard_Listener : MonoBehaviour
{
    public TextMeshProUGUI score_text;

    private void Start()
    {
        score_text.text = "Score: " + GameManager.Instance.Score;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
