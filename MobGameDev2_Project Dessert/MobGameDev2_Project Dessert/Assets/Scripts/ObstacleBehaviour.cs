using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How long to wait before restarting the game")]
    public float waitTime = 2.0f;

    public GameObject explosion;
    public GameObject GameOverMenu, HUD;

    public PlayerBehaviour Player;

    private AudioSource GameOver;
    private void Start()
    {
        GameOver = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            GameOver.Play();

            collision.gameObject.SetActive(false);

            var go = GetGameOverMenu();
            var hud = GetHUD();

            go.SetActive(true);
            hud.SetActive(false);
        }

    }

    private void PlayerTouch()
    {

        if (explosion != null)
        {
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }
        Destroy(this.gameObject);

    }

    GameObject GetGameOverMenu()
    {
        var canvas = GameObject.Find("Canvas").transform;
        return canvas.Find("GameOverMenu").gameObject;


    }
    GameObject GetHUD()
    {
        var canvas = GameObject.Find("Canvas").transform;
        return canvas.Find("SafeAreaHolder").gameObject;

    }

}
