using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoixMap : MonoBehaviour
{
    [Header("Maps")]
    [SerializeField] private GameObject Ville;
    [SerializeField] private GameObject Future;
    [SerializeField] private GameObject Foret;
    [SerializeField] private GameObject Gladiateur;

    private void Start()
    {
        OnDropdownValueChanged();
    }

    public void OnDropdownValueChanged()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        int index = dropdown.value;

        Ville.SetActive(false);
        Future.SetActive(false);
        Foret.SetActive(false);
        Gladiateur.SetActive(false);

        switch (index)
        {
            case 0:
                Ville.SetActive(true);
                break;
            case 1:
                Future.SetActive(true);
                break;
            case 2:
                Foret.SetActive(true);
                break;
            case 3:
                Gladiateur.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid dropdown index");
                break;
        }

        Debug.Log($"Selected index: {index}");
    }
}
