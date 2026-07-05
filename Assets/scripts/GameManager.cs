using UnityEngine;
using UnityEngine.SceneManagement; // Sahneleri deðiþtirebilmek için bu kütüphane þarttýr

public class GameManager : MonoBehaviour
{
    // Oyun sahnesindeki Game Over panelini buraya baðlayacaðýz
    public GameObject gameOverPanel;

    void Start()
    {
        // Eðer oyun sahnesindeysek ve panel atanmýþsa baþlangýçta gizli olduðundan emin oluyoruz
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Ana menüdeki BAÞLA butonuna basýlýnca çalýþacak fonksiyon
    public void StartGame()
    {
        // Buraya týrnak içinde oyun sahnenin tam adýný yazmalýsýn
        SceneManager.LoadScene("Game");
    }

    // Gemi yok olduðunda çaðrýlacak fonksiyon
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Game Over panelini görünür yapar
        }
    }

    
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ExitGame()
    {
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}