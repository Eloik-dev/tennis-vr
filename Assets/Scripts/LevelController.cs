using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] LevelState level;

    private void Awake()
    {
        level.ResetLevel();
    }
}
