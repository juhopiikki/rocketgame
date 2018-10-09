using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_movements : MonoBehaviour {

	private Rigidbody2D rb;

	// scales to acceleration
	public float speedScale;
	public float rotationSpeedScale;
	public float rotationScale;

	public ButtonActions buttonActions;

	// public vector for center of mass
	public Vector3 com;	// X: to right/left axis from ship, Y: to forward/backward from ship.

	// text object for HP
	public Text HPtext;

	// hit points
	public float HP;

	// pause button panel
	public GameObject pausePanel;
    public GameObject lossPanel;

	void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        rb.centerOfMass = com;
        HP = 10;
        Debug.Log("HP: " + HP);
        HPtext.text = "HP: " + HP.ToString() + "/10";
        pausePanel.SetActive(false);
        lossPanel.SetActive(false);
        // give speed scales a prescale so it's easier to compare them
        speedScale /= 100;
        rotationSpeedScale /= 100;
    }

	void Update()
    {
    	if(HP < 1) {
    		// game over
            lossPanel.SetActive(true);
    		//buttonActions.restartCurrentScene();
    	}
    	// touch screen touches
    	bool left = false;
    	bool right = false;
    	foreach (Touch touch in Input.touches) {
	    	if(touch.position.x < Screen.width / 2) {
	    		left = true;
    		}
    		if(touch.position.x >= Screen.width / 2) {
    			right = true;
    		}
	    }
	    if(left && right) {
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * speedScale, rb.transform.right[0] * speedScale);
	    } else if (left) {
        	rb.angularVelocity = rb.angularVelocity - 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
    	} else if (right) {
        	rb.angularVelocity = rb.angularVelocity + 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
    	}

//    	Debug.Log("angular: " + rb.angularVelocity);
//    	Debug.Log("rotation: " + rb.rotation);
//    	Debug.Log("forward: " + rb.transform.forward);
//    	Debug.Log("right: " + rb.transform.right);
        //if (Input.GetKey(KeyCode.UpArrow))
		if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))                
        {
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * speedScale, rb.transform.right[0] * speedScale);
        	//rb.AddForce(rb.transform.right * 1);
//        	rb.velocity = rb.velocity + new Vector2(rb.trasform.rotation, 0.2f);
//        	rb.angularVelocity = rb.angularVelocity + 0.2f; 
//        	rb.velocity = rb.velocity + new Vector2(0.0f, 0.2f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        	rb.angularVelocity = rb.angularVelocity + 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
        	//rb.velocity = rb.velocity + new Vector2(-0.2f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
        	rb.angularVelocity = rb.angularVelocity - 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
        	// rb.velocity = rb.velocity + new Vector2(0.2f, 0.0f);
        }

        // press space for pause
        if(Input.GetKeyDown (KeyCode.Space)) 
        {
        	Debug.Log("Space pressed");
            if (!pausePanel.activeInHierarchy) 
            {
                PauseGame();
            } else if (pausePanel.activeInHierarchy) 
            {
                 ContinueGame();   
            }
        }
    }

    // on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        // take HP
        HP = Mathf.Max(0, HP - Mathf.RoundToInt(collision.relativeVelocity.magnitude));
        HPtext.text = "HP: " + HP.ToString() + "/10";
        Debug.Log("HP: " + HP);
    }

    private void PauseGame()
    {
    	Debug.Log("Pause game");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    } 
    private void ContinueGame()
    {
    	Debug.Log("Continue game");
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
        
}
