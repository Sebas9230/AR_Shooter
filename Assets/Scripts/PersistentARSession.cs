using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PersistentARSession : MonoBehaviour
{
    private static PersistentARSession instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Evita duplicados al volver a cargar la escena.
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Mantiene este objeto entre escenas.
    }

    /*
    private void OnLevelWasLoaded(int level)
    {
        // Reinicia la sesión de AR al cargar una nueva escena.
        var arSession = FindObjectOfType<ARSession>();
        if (arSession != null)
        {
            arSession.Reset();
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
