using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] LevelState levelState;
    [SerializeField] TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = $"Score: {levelState.PlayerScore.ToString()}";
    }
}
