using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	[SerializeField] private GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape)) 
        {
        	Debug.Log("Escape pressed");
            if (!pausePanel.activeInHierarchy) 
            {
                PauseGame();
            }
            if (pausePanel.activeInHierarchy) 
            {
                 ContinueGame();   
            }
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
}
