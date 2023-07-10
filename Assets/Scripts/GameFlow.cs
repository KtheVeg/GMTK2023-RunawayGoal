using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GameFlow : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;
    public bool isRoaming = false;
    [SerializeField]
    public float roamingPhaseDuration = 20;
    [SerializeField]
    public float stationaryPhaseDuration = 10;
    [SerializeField]
    public float timer = 0;
    [SerializeField]
    int phase = -1;
    [SerializeField]
    ThirdPersonController playerController;
    [SerializeField]
    PhaseEvents[] phaseEvents;
    [SerializeField]
    bool lastPhase = false;
    [SerializeField]
    GameObject CongratsScreen;
    public int remainingInventoryItems = 0;
    [SerializeField]
    MiscPlayerHandler miscPlayerHandler;
    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    CanvasGroup InventoryCanvasGroup;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (phaseEvents.Length == 0)
        {
            Debug.LogError("No phase events defined! Aborting Game...");
            Application.Quit();
            return;
        }
        EnterRoamMode();
    }
    public void EnterRoamMode()
    {
        miscPlayerHandler.EnterCapsuleMode();
        phase++;
        if (phase == phaseEvents.Length - 1)
            lastPhase = true;
        foreach (GameObject go in phaseEvents[phase].enableOnRoaming)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in phaseEvents[phase].disableOnRoaming)
        {
            go.SetActive(false);
        }
        roamingPhaseDuration = phaseEvents[phase].newRoamingPhaseDuration;
        stationaryPhaseDuration = phaseEvents[phase].newStationaryPhaseDuration;
        remainingInventoryItems += phaseEvents[phase].earnedItemPoints;
        playerController.MovementAllowed = true;
        isRoaming = true;
        enemy.GetComponent<EnemyHandler>().Deactivate();
        InventoryCanvasGroup.alpha = 1;
        StartCoroutine(RoamModeRoutine());
    }
    IEnumerator RoamModeRoutine()
    {
        timer = roamingPhaseDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            yield return null;
        }
        EnterStationaryMode();
        
    }
    public void EnterStationaryMode()
    {
        miscPlayerHandler.EnterFlagpoleMode();
        foreach (GameObject go in phaseEvents[phase].disableOnStationary)
        {
            go.SetActive(false);
        }
        StopAllCoroutines();
        enemy.GetComponent<EnemyHandler>().Activate();
        playerController.MovementAllowed = false;
        isRoaming = false;
        InventoryCanvasGroup.alpha = 0.1f;
        StartCoroutine(StationaryModeRoutine());
    }
    IEnumerator StationaryModeRoutine()
    {
        timer = stationaryPhaseDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        if (lastPhase)
        {
            CongratsScreen.SetActive(true);
        }
        else
        {
            EnterRoamMode();
        }
        
    }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
    }
}
