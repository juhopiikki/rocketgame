using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlTutorialBackground : MonoBehaviour {

	private float offset;

	private int leftPressed, rightPressed, bothPressed, enough, enough2, upPressed;

    public GameObject fingerL, fingerR, fingerUp, halfScreenCoverL, halfScreenCoverR, screenUp;
    public GameObject continueButton;

    private bool phase1, phase2, phase3, phase4;

	void Awake() {
		fingerL = GameObject.Find("fingerL");
		fingerR = GameObject.Find("fingerR");
		halfScreenCoverL = GameObject.Find("halfScreenL");
		halfScreenCoverR = GameObject.Find("halfScreenR");

		fingerR.SetActive(false);
		fingerUp.SetActive(false);
		halfScreenCoverR.SetActive(false);
		screenUp.SetActive(false);
		continueButton = GameObject.Find("ContinueButton");
		continueButton.SetActive(false);
		Button continueBtn = continueButton.GetComponent<Button>();
		continueBtn.onClick.AddListener(loadMenu);

        leftPressed = 0;
        rightPressed = 0;
        upPressed = 0;
        bothPressed = 0;
        enough = 40;
        phase1 = true;
        phase2 = false;
        phase3 = false;
        phase4 = false;
	}

	// Update is called once per frame
	void LateUpdate () {
		// touch screen touches
    	bool left = false;
    	bool right = false;
    	bool up = false;

        // keyboard inputs
        if(Input.GetKey(KeyCode.LeftArrow)) {
            left = true;
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            right = true;
        }
        if(Input.GetKey(KeyCode.UpArrow)) {
            up = true;
        }

    	foreach (Touch touch in Input.touches) {
	    	if(touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2) {
	    		left = true;
    		}
    		if(touch.position.x >= Screen.width / 2 && touch.position.y < Screen.height / 2) {
    			right = true;
    		}
    		if(touch.position.y >= Screen.height / 2) {
    			up = true;
    		}
	    }

		if(phase1) {
			if(leftPressed > enough) {
				phase1 = false;
				phase2 = true;

				fingerL.SetActive(false);
	        	halfScreenCoverL.SetActive(false);

	        	fingerR.SetActive(true);
	        	halfScreenCoverR.SetActive(true);
			} else if(left) {
				leftPressed += 1;
			}
		}
		if(phase2) {
			if(rightPressed > enough) {
				phase2 = false;
				phase3 = true;

	        	fingerR.SetActive(false);
	        	halfScreenCoverR.SetActive(false);

	        	fingerUp.SetActive(true);
        		screenUp.SetActive(true);
			} else if(right) {
				rightPressed += 1;	
			}
		} 
		if(phase3) {
			if(PlayerPrefs.GetInt("brakeToggle") == 0) {
				upPressed = enough + 1;
			}
			if(upPressed > enough) {
				phase3 = false;
				phase4 = true;

	        	fingerUp.SetActive(false);
        		screenUp.SetActive(false);

        		fingerR.SetActive(true);
				fingerL.SetActive(true);
				halfScreenCoverR.SetActive(true);
				halfScreenCoverL.SetActive(true);
			} else if(up) {
				upPressed += 1;
			}
		} 
		if(phase4) {
			if(bothPressed > enough) {
				fingerR.SetActive(false);
				fingerL.SetActive(false);
				halfScreenCoverR.SetActive(false);
				halfScreenCoverL.SetActive(false);

				continueButton.SetActive(true);
			} else if(left && right) {
				bothPressed += 1;
			}
		}
	}

	private void loadMenu() {
        PlayerPrefs.SetInt("lvl2", 1);
        SceneManager.LoadScene("OIKEA_LEVEL");
	}

}
