using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance {get; private set;}

    public enum MindState{
        REALITY,
        IMAGINATION,
        DEPRESSED,
        INSANE
    }

    [Header("Current State")]
    public MindState currentState = MindState.REALITY;

    [Header("Meters")]
    public Slider boredomBar;
    public Slider insanityBar;

    [Header("Switching")]
    public float switchCooldownDuration = 2f;
    private float switchCooldownTimer = 0f;

    [Header("Lockout")]
    public float lockDuration = 60f; // 1 minute
    private float lockTimer = 0f;

    [Header("Movement")]
    public float depressionSpeed = 1.2f;
    public float realitySpeed = 2.2f;
    public float imaginationSpeed = 4f;
    public float insaneSpeed = 6f;

    [Header("Customer Spawning")]
    public float customerSpawnDelay = 1f;
    public BoxCollider2D customerSpawn;
    public GameObject customerPrefab;
    private float customerSpawnTimer = 0f;

    [Header("Text Garble")]
    [Range(0f, 1f)] public float garbleChance = 0f;
    [Range(0f, 1f)] public float tempGarble = 0f;
    public float timeGarbleStart = 180f;

    [Header("Order Timer")]
    public int ordersMissed = 0;

    private void Awake() {
        Instance = this;
    }    

    void Start()
    {
        EnterState(currentState);
    }

    void Update()
    {
        customerSpawnTimer += Time.deltaTime;
        if (customerSpawnTimer >= customerSpawnDelay && Map.Instance.waitSpots[Map.Instance.waitSpots.Count - 1].GetComponent<LineSpot>().occupied is false) {
            SpawnCustomer();
            customerSpawnTimer = 0f;
        }

        UpdateCoolDowns();
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, customerSpawn.bounds.center, Quaternion.identity);
        SoundManager.Play(SoundType.CUSTOMER_ENTER);
    }

    public bool CheckGarble(){
        float randChance = Random.Range(0f,1f);
        if (randChance < garbleChance){
            return true;
        }
        return false;
    }

    public string Garble(string text){
        // convert to char array
        char[] charArray = text.ToCharArray();

        // make all lowercase
        charArray[0] = char.ToLower(charArray[0]);

        // scramble
        for (int i = 0; i < charArray.Length; i++){
            char temp = charArray[i];
            int randomIndex = Random.Range(i, charArray.Length);
            charArray[i] = charArray[randomIndex];
            charArray[randomIndex] = temp;
        }

        // make new first letter uppercase
        charArray[0] = char.ToUpper(charArray[0]);

        // return full string from char array
        return new string(charArray);
    }

    private void UpdateCoolDowns() {
        if (switchCooldownTimer > 0f) {switchCooldownTimer -= Time.deltaTime;}

        if (lockTimer > 0f) {lockTimer -= Time.deltaTime;}

        // End lock states when timer is done
        if (lockTimer <= 0f) {
            if (currentState == MindState.DEPRESSED) {
                ChangeState(MindState.REALITY);
            }
            else if (currentState == MindState.INSANE) {
                ChangeState(MindState.IMAGINATION);
            }
        }
    }

    private void UpdateGarble() {
        float currentTime = TimeManager.Instance.elapsedTimeSeconds;
        if (currentTime >= timeGarbleStart && currentTime % 60 == 0){
            garbleChance += 0.15f;
        }
    }

    private void CheckForDebuff() {
        // If meter maxes out during a normal state, force lock into that mode
        float boredom = boredomBar.value;
        float insanity = insanityBar.value;

        if (currentState == MindState.REALITY && boredom >= 1f)
        {
            ChangeState(MindState.DEPRESSED);
        }

        else if (currentState == MindState.IMAGINATION && insanity >= 1f)
        {
            ChangeState(MindState.INSANE);
        }
    }

    public void HandleManualSwitchInput() {
        // Cannot manually switch during a locked state
        if (currentState == MindState.DEPRESSED || currentState == MindState.INSANE) {return;}

        // Cannot switch if cooldown is active
        if (switchCooldownTimer > 0f) {return;}

        if (currentState == MindState.REALITY) {
            ChangeState(MindState.IMAGINATION);
        }
        else if (currentState == MindState.IMAGINATION) {
            ChangeState(MindState.REALITY);
        }
    }

    public void ChangeState(MindState newState) {
        ExitState(currentState);
        currentState = newState;
        EnterState(newState);
    }

    private void EnterState(MindState state) {
        switch(state) {
            case MindState.REALITY:
                switchCooldownTimer = switchCooldownDuration;
                // get animator from customer, set boolean to switch
                // switch post-proc
                PlayerControls.Instance.movementSpeed = 2.2f;
                break;

            case MindState.IMAGINATION:
                switchCooldownTimer = switchCooldownDuration;
                // get animator from customer, set boolean to switch
                // switch post-proc
                PlayerControls.Instance.movementSpeed = 4f;
                break;

            case MindState.DEPRESSED:
                lockTimer = lockDuration;
                // switch post-proc
                PlayerControls.Instance.movementSpeed = 1.2f;
                break;

            case MindState.INSANE:
                lockTimer = lockDuration;
                // switch post-proc
                PlayerControls.Instance.movementSpeed = 2.2f;
                tempGarble = garbleChance;
                garbleChance = 1f;
                break;
        }
    }

    private void ExitState(MindState state) {
        if (state == MindState.INSANE){
            garbleChance = tempGarble;
        }
    }
}
