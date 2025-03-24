using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] LevelState levelState;
    [SerializeField] private GameObject menuParent;

    // Update is called once per frame
    void Update()
    {
        menuParent.SetActive(!levelState.IsLevelStarted);
    }

    public void OnClickAide()
    {
        Debug.Log("DOAOWDKO");
    }

    public void OnQuitter()
    {
        Application.Quit();
    }
}
