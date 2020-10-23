using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    float Score;

    [SerializeField]
    Text Scoretext;

    [SerializeField]
    Text FinalScoretext;

    void Update() 
    {
        Scoretext.text = ""+Score;
        FinalScoretext.text = ""+Score;
    
    }
    public void AddScore(float pts)
    {
        Score += pts;
    }
    public float ShowScore()
    {
        return Score;
    }
}
