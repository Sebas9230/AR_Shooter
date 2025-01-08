using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Habilitar y desbloquear el cursor
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        Cursor.visible = true; // Hace visible el cursor
    }
    public void ExitGame()
    {
        // Cierra el juego si se ejecuta como aplicación
        Application.Quit();

        // Debug para el editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
