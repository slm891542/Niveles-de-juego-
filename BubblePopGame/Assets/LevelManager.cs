using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPrefabs; // Prefabs de plataformas o escenarios
    public Transform spawnPoint;
    public int currentLevel = 0;
    public float difficultyMultiplier = 1.0f; // Aumenta la dificultad progresivamente

    private GameObject currentLevelObject;

    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {
        if (currentLevelObject != null)
        {
            Destroy(currentLevelObject);
        }

        int prefabIndex = levelIndex % levelPrefabs.Length; // Usa el módulo para repetir niveles si quieres
        currentLevelObject = Instantiate(levelPrefabs[prefabIndex], spawnPoint.position, Quaternion.identity);

        // Aumentar la dificultad (ejemplo: aumentar velocidad del jugador o gravedad)
        difficultyMultiplier = 1.0f + (levelIndex * 0.2f);
    }

    public void NextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public void RestartGame()
    {
        currentLevel = 0;
        LoadLevel(currentLevel);
    }

    public void GameOver()
    {
        // Reinicia toda la escena o reinicia solo el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
