using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1.0f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;


    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionsEnabled = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        HandleCheatKeys();  
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || !collisionsEnabled) return;

            switch (other.gameObject.tag)
            {
                case "Finish":
                    Debug.Log("Landed!");
                    StartSuccessSequence();
                    break;
                case "Friendly":
                    Debug.Log("Friendly collision");
                    break;
                default:
                    Debug.Log("Crash!");
                    StartCrashSequence();
                    break;
            }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();


        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }

    void StartSuccessSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void HandleCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollisions();
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            LoadNextLevel();
        }
    }

    void ToggleCollisions()
    {
        collisionsEnabled = !collisionsEnabled;
        Debug.Log($"CollisionsEnabled: {collisionsEnabled}");
    }
}
