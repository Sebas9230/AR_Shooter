using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after 3 seconds if it doesn't hit anything
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var whois = collision.gameObject.tag;
        if (whois == "Enemy")
        {
            Scoring(1);
            LogManager.LogEvent("NPC Shot", "Enemy hit by bullet");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (whois == "Ally")
        {
            Scoring(-1);
            LogManager.LogEvent("NPC Shot", "Ally hit by bullet");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Unknown object: " + whois);
            LogManager.LogEvent("Bullet Collision", "Hit unknown object: " + whois);
        }
    }


    void Scoring(int score) {
        // Update the global score and print it in the UI.
        NPCController.POINTS+=score;
        // Find the game object with an UI tag, access to its text component and update the content.
        GameObject.FindWithTag("UI").GetComponent<Text>().text = "" + (NPCController.POINTS);
    }

}
