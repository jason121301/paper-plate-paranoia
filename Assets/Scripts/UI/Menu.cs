using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject menu;
    public GameObject MenuImage;
    public GameObject skipPrompt;
    public int speed;
    private bool move = false;
    private bool readyToSkip = false;
    public void PlayGame()
    {

        // move menu to the right
        move = true;

        // activate child with cutscene and scene loader
        cutscene.SetActive(true);

        Invoke("StartCutscene", 4);
        Invoke("ShowSkip", 14);
    }

    private void ShowSkip()
    {
        skipPrompt.SetActive(true);

        readyToSkip = true;
    }

    private void StartCutscene()
    {
        // turn off menu image so it doesnt overlap
        MenuImage.SetActive(false);
    }
    private void Update()
    {
        if (move)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowSkip();
            if (readyToSkip)
            {
                // switch the game scene
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }
    }

    public void QuitGame()
    {
        // bye bye
        Application.Quit();
    }
}
