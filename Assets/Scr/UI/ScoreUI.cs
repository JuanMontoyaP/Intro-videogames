using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Text _TextComponent;
    private int _Score;

    void Start()
    {
        _TextComponent = GetComponent<Text>();
        _TextComponent.text = "0";
        AIAgent.OnEnemyDeath += EnemyDied;
    }

    private void EnemyDied(int score)
    {
        _Score += score;
        _TextComponent.text = $"{_Score}";
    }
}
