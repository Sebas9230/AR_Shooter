using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{
    private float gameTime = 45f;
    private bool gameEnded = false;
    private bool markerDetected = false; // Variable para saber si se ha detectado algún marcador
    [SerializeField] private ARSession arSession;
    [SerializeField] private ARTrackedImageManager arTrackedImageManager; // ARTrackedImageManager para manejar los eventos de imágenes

    void OnEnable()
    {
        if (arTrackedImageManager != null)
        {
            arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    void OnDisable()
    {
        if (arTrackedImageManager != null)
        {
            arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        gameEnded = true;
        string result;
        if (NPCController.POINTS >= 5 || markerDetected) // Condición adicional
        {
            result = "Victory";
            LogManager.LogEvent("Endgame", $"Result: {result}, Score: {NPCController.POINTS}");
            SceneManager.LoadScene("Win_Game");
        }
        else
        {
            result = "Defeat";
            LogManager.LogEvent("Endgame", $"Result: {result}, Score: {NPCController.POINTS}");
            SceneManager.LoadScene("End_Game");
        }
        Debug.Log($"Game Over: {result}");
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Si se detecta al menos un marcador, actualizamos la variable
        if (eventArgs.added.Count > 0 || eventArgs.updated.Count > 0)
        {
            markerDetected = true;
        }
    }
}
