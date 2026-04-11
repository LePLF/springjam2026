using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credit");
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}


