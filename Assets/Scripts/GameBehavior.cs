using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance {get; private set;}

    public enum MindState{
        REALITY,
        IMAGINATION,
        REALITY_LOCKED,
        IMAGINATION_LOCKED,
    }

    [Header("Current State")]
    public MindState currentState = MindState.REALITY;

    [Header("Meters")]
    [Range(0f, 100f)] public float boredom = 0f;
    [Range(0f, 100f)] public float insanity = 0f;

    [Header("Meters Rates Per Second")]
    public float boredomIncreaseRate = 15f; // fills in reality
    public float boredomDecreaseRate = 20f; // lowers in imagination
    public float insanityIncreaseRate = 15f; // fills in imagination
    public float insanityDecreaseRate = 20f; // lowers in reality

    [Header("Switching")]
    public float switchCooldownDuration = 2f;
    private float switchCooldownTimer = 0f;

    [Header("Lockout")]
    public float lockDuration = 60f; // 1 minute
    private float lockTimer = 0f;

    [Header("Movement")]
    public float normalRealitySpeed = 4f;
    public float slowedRealitySpeed = 2f;
    public float imaginationSpeed = 6f;
    public float extremeSlowSpeed = 1f;

    [Header("Customer Spawning")]
    public float customerSpawnDelay = 1f;
    public BoxCollider2D customerSpawn;
    public GameObject customerPrefab;

    private float customerSpawnTimer = 0f;

    [Header("Gameplay Effects")]
    [Range(0f, 1f)] public float imaginationOrderScrambleChance = 0.25f;
    [Range(0f, 1f)] public float maxInsanityGarbleRate = 1f;

    // Ideas for references to other systems
    // public PlayerMovement playerMovement;
    
    // public OrderSystem orderSystem;
    // public VisualManager visualManager;
    // public CustomerManager customerManager;
    // public TooltipManager tooltipManager;

    private void Awake() {
        Instance = this;
    }    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     EnterState(currentState);
    // }

    // Update is called once per frame
    void Update()
    {
        // UpdateCoolDowns();
        // UpdateMeter();
        // CheckForForcedLocks();
        // HandleManualSwitchInput();
        // UpdateDynamicEffects();

        customerSpawnTimer += Time.deltaTime;
        if (customerSpawnTimer >= customerSpawnDelay) {
            SpawnCustomer();
            customerSpawnTimer = 0f;
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, customerSpawn.bounds.center, Quaternion.identity);
    }

    // private void UpdateCoolDowns()
    // {
    //     if (switchCooldownTimer > 0f)
    //         switchCooldownTimer -= Time.deltaTime;

    //     if (lockTimer > 0f)
    //         lockTimer -= Time.deltaTime;

    //     // End lock states when timer is done
    //     if (lockTimer <= 0f)
    //     {
    //         if (currentState == MindState.REALITY_LOCKED)
    //         {
    //             ChangeState(MindState.REALITY);
    //         }
    //         else if (currentState == MindState.IMAGINATION_LOCKED)
    //         {
    //             ChangeState(MindState.IMAGINATION);
    //         }
    //     }
    // }

    // private void UpdateMeter()
    // {
    //     switch (currentState)
    //     {
    //         case MindState.REALITY:
    //         case MindState.REALITY_LOCKED:
    //             boredom += boredomIncreaseRate * Time.deltaTime;
    //             insanity -= insanityDecreaseRate * Time.deltaTime;
    //             break;

    //         case MindState.IMAGINATION:
    //         case MindState.IMAGINATION_LOCKED:
    //             insanity += insanityIncreaseRate * Time.deltaTime;
    //             boredom -= boredomDecreaseRate * Time.deltaTime;
    //             break;
    //     }
    //     boredom = Mathf.Clamp(boredom, 0f, 100f);
    //     insanity = Mathf.Clamp(insanity, 0f, 100f);
    // }

    // private void CheckForForcedLocks()
    // {
    //     // If meter maxes out during a normal state, force lock into that mode
    //     if (currentState == MindState.REALITY && boredom >= 100f)
    //     {
    //         ChangeState(MindState.REALITY_LOCKED);
    //     }

    //     else if (currentState == MindState.IMAGINATION && insanity >= 100f)
    //     {
    //         ChangeState(MindState.IMAGINATION_LOCKED);
    //     }
    // }

    // private void HandleManualSwitchInput()
    // {
    //     if (!Input.GetKeyDown(KeyCode.Tab))
    //         return;

    //     // Cannot manually switch during a locked state
    //     if (currentState == MindState.REALITY_LOCKED || currentState == MindState.IMAGINATION_LOCKED)
    //         return;

    //     // Cannot switch if cooldown is active
    //     if (switchCooldownTimer > 0f)
    //         return;

    //     if (currentState == MindState.REALITY)
    //     {
    //         ChangeState(MindState.IMAGINATION);
    //     }
    //     else if (currentState == MindState.IMAGINATION)
    //     {
    //         ChangeState(MindState.REALITY);
    //     }
    // }

    // public void ChangeState(MindState newState)
    // {
    //     ExitState(currentState);
    //     currentState = newState;
    //     EnterState(newState);
    // }

    // private void EnterState(MindState state)
    // {
    //     switch(state)
    //     {
    //         case MindState.REALITY:
    //             ApplyRealityMode();
    //             switchCooldownTimer = switchCooldownDuration;
    //             break;

    //         case MindState.IMAGINATION:
    //             ApplyImaginationMode();
    //             switchCooldownTimer = switchCooldownDuration;
    //             break;

    //         case MindState.REALITY_LOCKED:
    //             ApplyRealityMode();
    //             lockTimer = lockDuration;
    //             break;

    //         case MindState.IMAGINATION_LOCKED:
    //             ApplyImaginationMode();
    //             lockTimer = lockDuration;
    //             break;

    //     }
    // }

    // private void ExitState(MindState state)
    // {
    //     // Optional cleanup if we need it later
    // }

    // private void ApplyRealityMode()
    // {
    //     // Visuals
    //     visualManager.setGrayScaleWorld();
    //     customerManager.SetHumanCustomer();

    //     // Orders
    //     orderSystem.SetUseMedicationNames(true);
    //     orderSystem.SetScrambleChance(0f);

    //     // Player Movements
    //     playerMovement.SetMoveSpeed(normalRealitySpeed);

    //     // Tooltips
    //     tooltipManager.SetGarbleRate(0f);

    //     // Optional object lables / shelf UI
    //     orderSystem.ShowRealityLabels();
    // }

    // private void ApplyImaginationMode()
    // {
    //     // Visuals
    //     visualManager.SetFunkyColorWorld();
    //     visualManager.SetAlienCustomers();

    //     // Order
    //     orderSystem.SetUseMedicationNames(false); // using nonsense objects now
    //     orderSystem.SetScrambleChance(imaginationOrderScrambleChance);

    //     // Player Movement
    //     playerMovement.SetMoveSpeed(imaginationSpeed);

    //     // Tooltip 
    //     tooltipManager.SetGarbleRate(insanity / 100f);

    //     // Optional object lables / shelf UI
    //     orderSystem.ShowImaginationLabels();
    // }

    // private void UpdateDynamicEffects()
    // {
    //     // Reality punishment with full boredom bringing slowness
    //     if (currentState == MindState.REALITY || currentState == MindState.REALITY_LOCKED)
    //     {
    //         if (boredom >= 100f)
    //         {
    //             playerMovement.SetMoveSpeed(extremeSlowSpeed);
    //         }
    //         else if (boredom >= 75f)
    //         {
    //             playerMovement.SetMoveSpeed(slowedRealitySpeed); 
    //         }
    //         else
    //         {
    //             playerMovement.SetMoveSpeed(normalRealitySpeed);
    //         }

    //         tooltipManager.SetGarbleRate(0f);
    //     }

    //     // Imagination punishment with full insanity increasing garble
    //     if (currentState == MindState.IMAGINATION || currentState == MindState.IMAGINATION_LOCKED)
    //     {
    //         float garbleRate = insanity / 100f;
    //         garbleRate = Mathf.Clamp(garbleRate, 0f, maxInsanityGarbleRate);

    //         tooltipManager.SetGarbleRate(garbleRate);
    //         orderSystem.SetLiveGarbleRate(garbleRate);

    //         playerMovement.SetMoveSpeed(imaginationSpeed);
    //     }
    // }
}
