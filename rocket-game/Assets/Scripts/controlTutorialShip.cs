using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controlTutorialShip : MonoBehaviour {

	private Rigidbody2D rb;	
	public float speedScale;
	private bool fired, talked;

    public GameObject fireParticles, smokeParticles, mainCamera, skipButtonGO, oldMan, oldManText;
    public Button skipButton;
    public float gravityScale;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;		
		fired = false;
		talked = false;
		fireParticles.SetActive(false);
		smokeParticles.SetActive(false);

        Button skipBtn = skipButton.GetComponent<Button>();
        skipBtn.onClick.AddListener(skipEscape);
        skipButtonGO.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		bool touch = false;
		if(Input.touchCount > 0 || Input.GetKeyDown (KeyCode.Space) 
			|| Input.GetKey(KeyCode.UpArrow) 
			|| Input.GetKey(KeyCode.RightArrow)
			|| Input.GetKey(KeyCode.LeftArrow)) {
			touch = true;
		}
		if(fired) {
			rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * speedScale, rb.transform.right[0] * speedScale);			
			speedScale += 0.0001f;
		}
		if(!fired && touch && talked) {
			skipButtonGO.SetActive(true);
			FindObjectOfType<AudioManager>().Play("liftoff");
			rb.gravityScale = gravityScale;
			fired = true;
			fireParticles.SetActive(true);
			smokeParticles.SetActive(true);
		}
		if(!fired && !talked && touch) {
			talked = true;
			oldMan.SetActive(false);
			oldManText.SetActive(false);
		}
	}

	void LateUpdate () {
		if(fired) {
			mainCamera.transform.position += new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f), 0);
		}
	}

	private void skipEscape() {
        PlayerPrefs.SetInt("lvl1", 1);
        SceneManager.LoadScene("MainMenu");
	}
}
