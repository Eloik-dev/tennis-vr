using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "Scriptable Objects/LevelState")]
public class LevelState : ScriptableObject
{
    [SerializeField] private bool _isLevelStarted;
    [SerializeField] private bool _isLevelRestarting;
    [SerializeField] private float _playerScore;

    private bool _shouldBallComeBack = false;
    private float _multiplier = 1f;
    private int _totalFriendlyBounces = 0;

    public bool IsLevelStarted
    {
        get => _isLevelStarted;
        private set => _isLevelStarted = value;
    }
    
    public bool ShouldBallComeBack
    {
        get => _shouldBallComeBack;
        private set => _shouldBallComeBack = value;
    }

    public bool IsLevelRestarting
    {
        get => _isLevelRestarting;
        set => _isLevelRestarting = value;
    }

    public float PlayerScore
    {
        get => _playerScore;
        private set => _playerScore = value;
    }

    /// <summary>
    /// Redémarre le niveau en mettant toutes les valeurs à leur valeur initiale
    /// </summary>
    public void ResetLevel()
    {
        _isLevelStarted = false;
        _isLevelRestarting = false;
        _playerScore = 0;
        _multiplier = 1f;
        _totalFriendlyBounces = 0;
        _shouldBallComeBack = false;

        Debug.Log("Level state reset.");
    }

    /// <summary>
    /// Démarre le niveau
    /// </summary>
    public void StartLevel()
    {
        if (!_isLevelStarted)
        {
            _isLevelStarted = true;
            Debug.Log("Level started");
        }
    }

    /// <summary>
    /// Signale que le côté ennemi a été touché par la balle
    /// </summary>
    public void HitEnemySide()
    {
        _multiplier = 1f;
        AddScore();
    }

    /// <summary>
    /// Signale qu'un multiplicateur a été touché par la balle
    /// </summary>
    public void HitMultiplierZone()
    {
        _multiplier += 0.25f;
        AddScore();
    }
    
    /// <summary>
    /// Signale que le côté du joueur a été touché par la balle
    /// </summary>
    public void HitFriendlySide()
    {
        _totalFriendlyBounces += 1;
        ShouldBallComeBack = false;

        if (_totalFriendlyBounces > 1)
        {
            ResetLevel();
        }
    }

    /// <summary>
    /// Signale au jeu que la ball est revenue vers le joueur
    /// </summary>
    public void NotifyBallCameBack()
    {
        ShouldBallComeBack = false;
    }
    
    /// <summary>
    /// Ajoute le score au joueur
    /// </summary>
    public void AddScore()
    {
        if (!_isLevelStarted)
        {
            Debug.LogWarning("Cannot add score: Level has not started yet.");
            return;
        }

        _totalFriendlyBounces = 0;
        ShouldBallComeBack = true;
        _playerScore += 15 * _multiplier;
        Debug.Log($"Score updated: {_playerScore}");
    }
}
