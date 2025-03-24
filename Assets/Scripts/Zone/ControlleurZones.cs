using UnityEngine;
using System.Collections;
using System.Linq;

public class ControlleurZone : MonoBehaviour
{
    [SerializeField] private LevelState levelState;
    private Zone zoneCible;
    private int serie = 0;

    void Start()
    {
        ChoisirZoneCible();
    }

    // Choisi une nouvelle zone cible aléatoirement
    private void ChoisirZoneCible()
    {
        Zone[] zones = GetComponentsInChildren<Zone>();
        Zone[] filteredZones = zones.Where(zone => zone.zoneType == ZoneTypes.MULTIPLIER).ToArray();
        if (filteredZones.Length == 0) return;

        if (zoneCible != null)
        {
            zoneCible.SetCible(false);
        }

        zoneCible = filteredZones[Random.Range(0, filteredZones.Length)];
        zoneCible.SetCible(true);
    }

    // Fonction appeler lorsqu'une zone est touché
    public void ZoneToucher(Zone zone)
    {
        if (!levelState.IsLevelStarted || levelState.IsLevelRestarting) return;

        serie = (zone == zoneCible) ? serie + 1 : 0;

        Debug.Log(zone.zoneType);
        switch (zone.zoneType)
        {
            case ZoneTypes.DEFAULT:
                levelState.HitEnemySide();
                if (zone == zoneCible)
                {
                    ChoisirZoneCible();
                }
                break;
            case ZoneTypes.MULTIPLIER:
                levelState.HitMultiplierZone();
                ChoisirZoneCible();
                break;
            case ZoneTypes.FRIENDLY:
                levelState.HitFriendlySide();
                break;
            case ZoneTypes.BAD:
                StartCoroutine(ResetLevelDelayed());
                break;
        }
    }

    private IEnumerator ResetLevelDelayed()
    {
        if (!levelState.IsLevelRestarting)
        {
            levelState.IsLevelRestarting = true;
            yield return new WaitForSeconds(1f);
            levelState.ResetLevel();
        } else
        {
            yield return null;
        }
    }
}
