using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour {

    // buttons
	public Button pauseButton, continueButton, restartButton, restartButton2;
    public Button mainMenuButton1, mainMenuButton2, QuitButton, nextLvlButton;
    public GameObject tutorialContinueButton;

	// button panels
	public GameObject pausePanel, pauseToggles;
    public GameObject lossPanel;

    // player object
    public GameObject player;

    // Toggles
    public Toggle flipControlsToggle, brakingEnableToggle, musicToggle, soundToggle;

	// Use this for initialization
	void Start () {
		Button pauseBtn = pauseButton.GetComponent<Button>();
        Button continueBtn = continueButton.GetComponent<Button>();
        Button restartBtn = restartButton.GetComponent<Button>();
        Button restartBtn2 = restartButton2.GetComponent<Button>();
        Button menuBtn1 = mainMenuButton1.GetComponent<Button>();
        Button menuBtn2 = mainMenuButton2.GetComponent<Button>();
        Button QuitBtn = QuitButton.GetComponent<Button>();
        Button nextLvlBtn = nextLvlButton.GetComponent<Button>();

        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        pauseBtn.onClick.AddListener(pauseButtonFunction);
        continueBtn.onClick.AddListener(ContinueGame);
        restartBtn.onClick.AddListener(restartCurrentScene);
        restartBtn2.onClick.AddListener(restartCurrentScene2);
        menuBtn1.onClick.AddListener(toMainMenuScene);
        menuBtn2.onClick.AddListener(toMainMenuScene);
        QuitBtn.onClick.AddListener(quitTheGame);
        nextLvlBtn.onClick.AddListener(nextLvl);

        if(PlayerPrefs.GetInt("musicOn") == 1) {
            musicToggle.isOn = true;
        } else {
            musicToggle.isOn = false;            
        }
        if(PlayerPrefs.GetInt("soundOn") == 1) {
            soundToggle.isOn = true;
        } else {
            soundToggle.isOn = false;            
        }

        musicToggle.onValueChanged.AddListener((value) => pauseMusic(value));
        soundToggle.onValueChanged.AddListener((value) => muteSounds(value));

        tutorialContinueButton.SetActive(false);

        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void pauseMusic(bool isOn) {
        if(isOn) {
            PlayerPrefs.SetInt("musicOn", 1);
            FindObjectOfType<AudioManager>().UnPauseMusic("music1");
        } else{
            PlayerPrefs.SetInt("musicOn", 0);
            FindObjectOfType<AudioManager>().PauseMusic("music1");
        }
    }
	
    public void muteSounds(bool isOn) {
        if(isOn) {
            PlayerPrefs.SetInt("soundOn", 1);
        } else {
            PlayerPrefs.SetInt("soundOn", 0);
        }
    }

	private void pauseButtonFunction() {
		if (!pausePanel.activeInHierarchy && !lossPanel.activeInHierarchy) {
            PauseGame();
        } else if (pausePanel.activeInHierarchy) {
            ContinueGame();   
        }
	}

	private void PauseGame()
    {
        FindObjectOfType<AudioManager>().Stop("engine");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseToggles.SetActive(true);
        player.GetComponent<Player_movements>().freezePlayer();
    }

    private void ContinueGame()
    {
        PlayerPrefs.SetInt("brakeToggle", brakingEnableToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("flipToggle", flipControlsToggle.isOn ? 1 : 0);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseToggles.SetActive(false);
        player.GetComponent<Player_movements>().unFreezePlayer();
    }

	public void restartCurrentScene() {
        PlayerPrefs.SetInt("brakeToggle", brakingEnableToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("flipToggle", flipControlsToggle.isOn ? 1 : 0);
        Scene scene = SceneManager.GetActiveScene(); 
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseToggles.SetActive(false);
      	SceneManager.LoadScene(scene.name);
    }

    public void restartCurrentScene2() {
        PlayerPrefs.SetInt("brakeToggle", brakingEnableToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("flipToggle", flipControlsToggle.isOn ? 1 : 0);
        Scene scene = SceneManager.GetActiveScene(); 
        Time.timeScale = 1;
        lossPanel.SetActive(false);
        SceneManager.LoadScene(scene.name);
    }

    public void nextLvl() {
        PlayerPrefs.SetInt("brakeToggle", brakingEnableToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("flipToggle", flipControlsToggle.isOn ? 1 : 0);

        SceneManager.LoadScene("METEOROIDS");
        Time.timeScale = 1;
        lossPanel.SetActive(false);
        
    }

    public void toMainMenuScene() {
        PlayerPrefs.SetInt("brakeToggle", brakingEnableToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("flipToggle", flipControlsToggle.isOn ? 1 : 0);
/*        Debug.Log("brake: ", brakingEnableToggle ? 1 : 0);
        Debug.Log("brake: ", brakingEnableToggle);
        Debug.Log("flip: ", brakingEnableToggle ? 1 : 0);
        Debug.Log("flip: ", brakingEnableToggle);*/
        Time.timeScale = 1;
        lossPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void quitTheGame() {
        Application.Quit();
    }
}
