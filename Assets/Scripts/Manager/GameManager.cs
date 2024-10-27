using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject losePanel;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        
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
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
