using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{    
    Movement movement;

    [SerializeField] float sceneTransionDelayTime = 2f;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision other)
    {
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
        movement.enabled = false;
        Invoke("ReloadLevel", sceneTransionDelayTime);        
    }

    void StartNextLevelSequance()
    {
        movement.enabled = false;
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
