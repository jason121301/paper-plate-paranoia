using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject losePanel;
    public GameObject winPanel;
    public AudioSource backgroundMusic;
    public AudioSource winMusic;
    public AudioSource loseMuisc;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

    }

    public void EndGame()
    {
        backgroundMusic.Stop();
        losePanel.SetActive(true);
        loseMuisc.Play();
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        UI.SetActive(false);
        winMusic.Play();
        Time.timeScale = 0;
    }
}
