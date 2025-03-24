using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] ControlleurZone zoneController;
    [SerializeField] public ZoneTypes zoneType = ZoneTypes.DEFAULT;
    
    private bool isCible = false;

    private void Start()
    {
        SetCible(false);
    }

    // Détect la collision de la balle
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            zoneController.ZoneToucher(this);
        }
    }

    // Met la zone en mode cible
    public void SetCible(bool state)
    {
        isCible = state;
        GetComponent<Renderer>().material.color = isCible ? Color.yellow : Color.red;
    }
}
