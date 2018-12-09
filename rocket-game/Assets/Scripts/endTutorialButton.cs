using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endTutorialButton : MonoBehaviour {

	public Button continueButton;

	// Use this for initialization
	void Start () {
		Button continueBtn = continueButton.GetComponent<Button>();
        continueBtn.onClick.AddListener(continueButtonFunction);
	}
	
	private void continueButtonFunction() {
		SceneManager.LoadScene("MainMenu");
	}
}
