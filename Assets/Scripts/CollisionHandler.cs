using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{    
    [SerializeField] float sceneTransionDelayTime = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successPartical;
    [SerializeField] ParticleSystem crashPartical;


    Movement movement;
    AudioSource audioSourceForPlayer;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        movement = GetComponent<Movement>();
        audioSourceForPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessCheats();
    }

    void ProcessCheats()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartNextLevelSequance();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) return;
        
        switch (other.transform.tag)
        {
            case "Friendly": break;
            case "Finish":
                StartNextLevelSequance();
                Debug.Log("Winner");
                break;
            default:
                StartCrashSequance();
                Debug.Log("Crashed");
                break;
        }
    }

    void StartCrashSequance()
    {
        isTransitioning = true;
        movement.enabled = false;
        audioSourceForPlayer.Stop();
        crashPartical.Play();
        audioSourceForPlayer.PlayOneShot(crashSound);            
        Invoke("ReloadLevel", sceneTransionDelayTime);
    }

    void StartNextLevelSequance()
    {
        isTransitioning = true;
        movement.enabled = false;
        audioSourceForPlayer.Stop();
        successPartical.Play();
        audioSourceForPlayer.PlayOneShot(successSound);
        Invoke("LoadNextLevel", sceneTransionDelayTime);       
    }

    void LoadNextLevel()
    {
        LoadLevel(true);
    }

    void ReloadLevel()
    {
        LoadLevel();
    }

    void LoadLevel(bool nextLevel = false)
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (nextLevel)
        {
            ++currentLevelIndex;
        }
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = 0;
        }
        SceneManager.LoadScene(currentLevelIndex);
    }
}
