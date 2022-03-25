using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public Sound explodeSound;
    public Sound collideSound;

    [SerializeField] private Sound gameOverSound;

    //Reference to parent of ui elements
    [SerializeField] private Transform heartUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject optionsUI;

    [SerializeField] private GameObject pointsUI;

    [HideInInspector] public AudioSource audioSource;

    // Reference to the object to spawn
    [SerializeField] private Transform fallingElement;

    [SerializeField] private AudioMixer audioMixer;
    //Spawn Position Varibles
    private const float spawnYPosition = 5f;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Fall speed of the falling elements
    public float averageFallspeed = 2;
    public float fallSpeedDifference = 1f;

    //Countdown Variables
    private float countdownAmt = 0.5f;
    private float timerElapsed;

    private float pointsPerLetterExplode = 10;

    //List containing all game characters
    public List<GameCharacter> gameCharacters;

    [HideInInspector] public KeyCode currentKeycodeDetected;

    [HideInInspector] public int lives = 1;

    private bool gameOver;
    private bool gamePaused = false;

    public delegate void PauseGameEventHandler(object sender, System.EventArgs eventArgs);
    public event PauseGameEventHandler pauseGameEvent;

    public float accelerationSpeed = 50f;
    private float pointsOfGame;
    [HideInInspector] public float pointsToSave;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Debug.Log(accelerationSpeed);
        audioSource = GetComponent<AudioSource>();
        timerElapsed = countdownAmt;
        for (int i = 1; i < lives; i++)
        {
            GameObject.Instantiate(heartUI, heartUIParent);
        }

        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
        if (!gameOver && !gamePaused)
        {
            currentKeycodeDetected = GetCurrentInputKeycode();
            timerElapsed -= Time.deltaTime;
            if (timerElapsed <= 0)
            {
                SpawnLetter();
                timerElapsed = countdownAmt;
            }
        }
    }

    private void SpawnLetter()
    {
        GameObject.Instantiate(fallingElement, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity).GetComponent<FallingElementScript>();
    }
    private KeyCode GetCurrentInputKeycode()
    {
        KeyCode keyCode = KeyCode.None;
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                return keyCode = kcode;
            }
        }
        return keyCode;
    }
    public void RemoveHealth()
    {
        lives --;
        RemoveHeartUI();
        if (lives <= 0)
        {
            EndGame();
            lives = 0;
        }
    }
    private void RemoveHeartUI()
    {
        if (heartUIParent.GetChild(0).gameObject != null)
        {
            Destroy(heartUIParent.GetChild(0).gameObject);
        }
    }
    private void EndGame()
    {
        SoundManagerScript.PlaySound(audioSource, gameOverSound);
        gameOver = true;
        gameOverUI.SetActive(true);
        DisableFallingElements();
    }
    protected virtual void OnDisableFallingElements()
    {
        if (pauseGameEvent != null)
        {
            pauseGameEvent.Invoke(this, System.EventArgs.Empty);
        }
    }
    public void DisableFallingElements()
    {
        OnDisableFallingElements();
    }
    public void TogglePauseGame()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        DisableFallingElements();
        gamePaused = !gamePaused;
    }
    private void OnFallingElementCollide(object sender, System.EventArgs eventArgs)
    {
        RemoveHealth();
    }
    public void ToggleOptions()
    {
        optionsUI.SetActive(!optionsUI.activeSelf);
    }
    public void AddPoints()
    {
        pointsOfGame += pointsPerLetterExplode;
        pointsUI.GetComponent<TextMeshProUGUI>().text = "Points: " + pointsOfGame;
    }
    public void SaveProgress()
    {
        if (SaveSystemScript.LoadProgress().bestScore <= pointsOfGame)
        {
            pointsToSave = pointsOfGame;
        }
        if (SaveSystemScript.LoadProgress().bestScore > pointsOfGame)
        {
            pointsToSave = SaveSystemScript.LoadProgress().bestScore;
        }
        SaveSystemScript.SaveProgress();
    }
}
