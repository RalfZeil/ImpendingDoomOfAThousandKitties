using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMeshPro;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score", 1);

        m_TextMeshPro.text = score.ToString();
    }

}
