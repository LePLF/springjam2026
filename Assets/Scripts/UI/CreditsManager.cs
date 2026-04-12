using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    [Header("Paramètres")]
    [SerializeField] private string menuSceneName = "Menu2.0";

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            LoadMenu();

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            LoadMenu();
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}

