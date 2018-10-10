using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public Button level1;
	public Button level2;
	public Button level3;

	// Use this for initialization
	void Start () {
		Button level1Btn = level1.GetComponent<Button>();
        Button level2Btn = level2.GetComponent<Button>();
        Button level3Btn = level3.GetComponent<Button>();

        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        level1Btn.onClick.AddListener(loadLvl1);
        level2Btn.onClick.AddListener(loadLvl2);
        level3Btn.onClick.AddListener(loadLvl3);
	}

	private void loadLvl1() {
        Time.timeScale = 1;
        SceneManager.LoadScene("TutorialScene");
	}
	
	private void loadLvl2() {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene2");
	}

	private void loadLvl3() {
        Time.timeScale = 1;
        SceneManager.LoadScene("levelHard");
	}
}
