using UnityEngine;
[System.Serializable]
public struct PhaseEvents
{
    public GameObject[] enableOnRoaming;
    public GameObject[] disableOnRoaming;
    public GameObject[] disableOnStationary;
    public int earnedItemPoints;
    public float newRoamingPhaseDuration;
    public float newStationaryPhaseDuration;
}