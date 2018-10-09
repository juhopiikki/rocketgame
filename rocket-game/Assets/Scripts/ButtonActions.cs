using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour {

	public Button pauseButton;
	public Button continueButton;
	public Button restartButton;
    public Button restartButton2;

	// pause button panel
	public GameObject pausePanel;
    public GameObject lossPanel;

	// Use this for initialization
	void Start () {
		Button pauseBtn = pauseButton.GetComponent<Button>();
        Button continueBtn = continueButton.GetComponent<Button>();
        Button restartBtn = restartButton.GetComponent<Button>();
        Button restartBtn2 = restartButton2.GetComponent<Button>();

        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        pauseBtn.onClick.AddListener(pauseButtonFunction);
        continueBtn.onClick.AddListener(ContinueGame);
        restartBtn.onClick.AddListener(restartCurrentScene);
        restartBtn2.onClick.AddListener(restartCurrentScene2);
	}
	
	private void pauseButtonFunction() {
		if (!pausePanel.activeInHierarchy) {
            PauseGame();
        } else if (pausePanel.activeInHierarchy) {
            ContinueGame();   
        }
	}

	private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

	public void restartCurrentScene(){
        Scene scene = SceneManager.GetActiveScene(); 
        Time.timeScale = 1;
        pausePanel.SetActive(false);
      	SceneManager.LoadScene(scene.name);
    }

    public void restartCurrentScene2(){
        Debug.Log("RESTART 2");
        Scene scene = SceneManager.GetActiveScene(); 
        Time.timeScale = 1;
        lossPanel.SetActive(false);
        SceneManager.LoadScene(scene.name);
    }
}
