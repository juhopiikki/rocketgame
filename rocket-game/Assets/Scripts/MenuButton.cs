using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    public Button level1, level2, level3, level4, back, back2, reset, hack;
	public Button levelSelect, settings, startgame;
    // Toggles
    public Toggle musicToggle, soundToggle;

    public GameObject levelButtons, menuButtons, quitButton, settingsButtons;

	// Use this for initialization
	void Start () {
		Button level1Btn = level1.GetComponent<Button>();
        Button level2Btn = level2.GetComponent<Button>();
        Button level3Btn = level3.GetComponent<Button>();
        Button level4Btn = level4.GetComponent<Button>();
        Button backBtn = back.GetComponent<Button>();
        Button backBtn2 = back2.GetComponent<Button>();
        Button resetBtn = reset.GetComponent<Button>();
        Button hackBtn = hack.GetComponent<Button>();

        Button levelSelectBtn = levelSelect.GetComponent<Button>();
        Button settingsBtn = settings.GetComponent<Button>();
        Button startgameBtn = startgame.GetComponent<Button>();
        Button quitgameBtn = quitButton.GetComponent<Button>();

        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        level1Btn.onClick.AddListener(loadLvl1);
        level2Btn.onClick.AddListener(loadLvl2);
        level3Btn.onClick.AddListener(loadLvl3);
        level4Btn.onClick.AddListener(loadLvl4);
        backBtn.onClick.AddListener(delegate{goBack(1);});
        backBtn2.onClick.AddListener(delegate{goBack(2);});
        resetBtn.onClick.AddListener(resetAll);
        hackBtn.onClick.AddListener(hackAll);

        levelSelectBtn.onClick.AddListener(selectLevel);
        settingsBtn.onClick.AddListener(settingsFunc);
        startgameBtn.onClick.AddListener(startGame);
        quitgameBtn.onClick.AddListener(quitTheGame);

        if(!PlayerPrefs.HasKey("brakeToggle")) {
            PlayerPrefs.SetInt("brakeToggle", 1);
        }
        if(!PlayerPrefs.HasKey("flipToggle")) {
            PlayerPrefs.SetInt("flipToggle", 0);
        }
        if(!PlayerPrefs.HasKey("musicOn")) {
            PlayerPrefs.SetInt("musicOn", 1);
        }
        if(!PlayerPrefs.HasKey("soundOn")) {
            PlayerPrefs.SetInt("soundOn", 1);
        }
        if(!PlayerPrefs.HasKey("lvl1")) {
            PlayerPrefs.SetInt("lvl1", 0);
        }
        if(!PlayerPrefs.HasKey("lvl2")) {
            PlayerPrefs.SetInt("lvl2", 0);
        }
        if(!PlayerPrefs.HasKey("lvl3")) {
            PlayerPrefs.SetInt("lvl3", 0);
        }
        if(!PlayerPrefs.HasKey("lvl4")) {
            PlayerPrefs.SetInt("lvl4", 0);
        }
        if(!PlayerPrefs.HasKey("shieldUpgrade")) {
            PlayerPrefs.SetInt("shieldUpgrade", 0);
        }

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

        if(PlayerPrefs.GetInt("lvl1") == 1) {
            level2.interactable = true;
        } else {
            level2.interactable = false;
        }
        if(PlayerPrefs.GetInt("lvl2") == 1) {
            level3.interactable = true;
        } else {
            level3.interactable = false;
        }
        if(PlayerPrefs.GetInt("lvl3") == 1) {
            level4.interactable = true;
        } else {
            level4.interactable = false;
        }
    }

	private void loadLvl1() {
        Time.timeScale = 1;
        SceneManager.LoadScene("EscapeScene");
	}

	private void loadLvl2() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");            
	}
	
	private void loadLvl3() {
        Time.timeScale = 1;
        SceneManager.LoadScene("OIKEA_LEVEL");
	}

	private void loadLvl4() {
        if(PlayerPrefs.GetInt("lvl3") == 1) {
            Time.timeScale = 1;
            SceneManager.LoadScene("METEOROIDS");
        }
	}

    private void selectLevel() {
        levelButtons.SetActive(true);
        menuButtons.SetActive(false);
    }

    void goBack(int varr) {
        if(varr == 1) {
            levelButtons.SetActive(false);
            menuButtons.SetActive(true);            
        }
        if (varr == 2) {
            settingsButtons.SetActive(false);
            menuButtons.SetActive(true);

            PlayerPrefs.SetInt("musicOn", musicToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("soundOn", soundToggle.isOn ? 1 : 0);
        }
    }

    private void settingsFunc() {
        settingsButtons.SetActive(true);
        menuButtons.SetActive(false);   
    }

    // start the level where player is
    private void startGame() {
        if(PlayerPrefs.GetInt("lvl3") == 1) {
            SceneManager.LoadScene("METEOROIDS");
        } else if(PlayerPrefs.GetInt("lvl1") == 1) { // go to OIKEA_LEVEL, not tutorial
            SceneManager.LoadScene("OIKEA_LEVEL");
        } else {
            SceneManager.LoadScene("EscapeScene");
        }
    }

    public void quitTheGame() {
        Application.Quit();
    }

    private void resetAll() {
        PlayerPrefs.SetInt("brakeToggle", 1);
        PlayerPrefs.SetInt("flipToggle", 0);
        PlayerPrefs.SetInt("musicOn", 1);
        PlayerPrefs.SetInt("soundOn", 1);
        PlayerPrefs.SetInt("lvl1", 0);
        PlayerPrefs.SetInt("lvl2", 0);
        PlayerPrefs.SetInt("lvl3", 0);
        PlayerPrefs.SetInt("lvl4", 0);
        PlayerPrefs.SetInt("shieldUpgrade", 0);
        SceneManager.LoadScene("MainMenu");
    }

    private void hackAll() {
        PlayerPrefs.SetInt("lvl1", 1);
        PlayerPrefs.SetInt("lvl2", 1);
        PlayerPrefs.SetInt("lvl3", 1);
        PlayerPrefs.SetInt("lvl4", 1);
        PlayerPrefs.SetInt("shieldUpgrade", 0);
        SceneManager.LoadScene("MainMenu");
    }
}
