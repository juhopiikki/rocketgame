 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_movements : MonoBehaviour {

	private Rigidbody2D rb;

	// scales to acceleration
	public float speedScale, brakingScale, rotationSpeedScale, rotationScale, rotationSlowingScale;

	// public vector for center of mass
	public Vector3 com;	// X: to right/left axis from ship, Y: to forward/backward from ship.

    // text object for HP
    //public Text HPtext;
    //public Slider HPSlider;
    public Image HPImage;
    public Color HPfull, HPempty, HPcollide;

	// hit points
	public float HP;
    public float MaxHP;
    private int healingHP;
    public Text countText;

    // pause button panel
    public GameObject pausePanel, pauseToggles, lossPanel, winButtons;
    public GameObject flameTextureL, flameTextureR;
    public GameObject flameTextureR_F, flameTextureL_F;

    public Toggle flipControlsToggle;
    public bool flipControls;

    public Toggle brakingEnableToggle;
    public bool brakingEnable;

    // to freeze movements on pause and after death
    public bool gameOn;

    // camera
    public GameObject mainCamera;

    // explode objects
    public GameObject explodes, HPImageUpgraded, astronaut;
    public astronaut astro;

    public GameObject spark;

	void Awake()
    {
        // pause button panel
        try {
            pausePanel = GameObject.FindGameObjectsWithTag("PauseMenu")[0];
            pauseToggles = GameObject.FindGameObjectsWithTag("PauseToggles")[0];
            winButtons = GameObject.FindGameObjectsWithTag("winButtons")[0];

            lossPanel = GameObject.FindGameObjectsWithTag("LosePanel")[0];
            // text object for HP
            //HPtext = GameObject.Find("HP text").GetComponent<Text>();
            HPImage = GameObject.Find("HealthBar").GetComponent<Image>();

            // flip controls toggle
            flipControlsToggle = GameObject.Find("FlipControlsToggle").GetComponent<Toggle>();
            flipControls = flipControlsToggle.isOn;

            // braking toggle
            brakingEnableToggle = GameObject.Find("BrakingEnableToggle").GetComponent<Toggle>();
            brakingEnable = brakingEnableToggle.isOn;

            mainCamera = GameObject.Find("Main Camera");

        } catch {
            Debug.Log("Some gameobject was not found.");
        }

        if(PlayerPrefs.GetInt("shieldUpgrade") == 1) {
            MaxHP = 200;
            HP = MaxHP;

            GameObject.Find("HPImage").SetActive(false);
            HPImageUpgraded.SetActive(true);
            HPImage = GameObject.Find("HealthBarUpgraded").GetComponent<Image>();

        }

        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        rb.centerOfMass = com;
        HP = 100;
        MaxHP = HP;
        healingHP = 50;
        Debug.Log("HP: " + HP);
        
        pausePanel.SetActive(false);
        pauseToggles.SetActive(false);
        lossPanel.SetActive(false);
        winButtons.SetActive(false);

        brakingEnableToggle.isOn = PlayerPrefs.GetInt("brakeToggle") != 0;
        flipControlsToggle.isOn = PlayerPrefs.GetInt("flipToggle") != 0;

        // give speed scales a prescale so it's easier to compare them
        speedScale /= 100;
        rotationSpeedScale /= 100;
        brakingScale /= 100;

        flipControls = flipControlsToggle.isOn;
        brakingEnable = brakingEnableToggle.isOn;

        gameOn = true;

        flameTextureR.SetActive(false);
        flameTextureL.SetActive(false);
        flameTextureR_F.SetActive(false);
        flameTextureL_F.SetActive(false);
    }

    void Start() {
        FindObjectOfType<AudioManager>().PlayMusic("music1");
    }

	void Update()
    {
        if(gameOn) {
            flipControls = flipControlsToggle.isOn;
            brakingEnable = brakingEnableToggle.isOn;

        	if(HP < 1) {
                FindObjectOfType<AudioManager>().Stop("engine");
                HPImage.fillAmount = 0;
        		// game over
                lossPanel.SetActive(true);
                explodes.transform.position = rb.transform.position;
                explodes.transform.rotation = rb.transform.rotation;
                
                Vector3 endPos = rb.transform.position;
                Vector2 endVelocity = rb.velocity;

                gameOn = false;
                // set self inactive
                gameObject.SetActive(false);
                explodes.SetActive(true);
                Rigidbody2D[] children = explodes.GetComponentsInChildren<Rigidbody2D>();;
                foreach(Rigidbody2D child in children)
                {
                    //Vector3 diff = endPos - child.transform.position;
                    Vector3 locPos = child.transform.localPosition;
                    child.velocity = endVelocity + new Vector2(locPos.y, locPos.x) * 12; //new Vector2(diff.x, diff.y);
                }
                astronaut.SetActive(false);
                astro.explode();
                FindObjectOfType<AudioManager>().Play("explosion");
        	}
        	// touch screen touches
        	bool left = false;
        	bool leftBottom = false;
        	bool right = false;
        	bool rightBottom = false;
        	bool top = false;

        	bool rightEngineBurn = false;
        	bool leftEngineBurn = false;
        	bool bothEnginesBurn = false;
        	bool brakingEnginesBurn = false;

            // touch screen touches
        	foreach (Touch touch in Input.touches) {
    	    	if(touch.position.x < Screen.width / 2) {
    	    		left = true;
    	    		if(touch.position.y < Screen.height / 2) {
    	    			leftBottom = true;
    	    		}
        		}
        		if(touch.position.x >= Screen.width / 2) {
        			right = true;
        			if(touch.position.y < Screen.height / 2) {
    	    			rightBottom = true;
    	    		}
        		}
        		if(touch.position.y >= Screen.height / 2) {
        			top = true;
        		}
    	    }

            // keyboard inputs
            if(Input.GetKey(KeyCode.LeftArrow)) {
                left = true;
                leftBottom = true;
            }
            if(Input.GetKey(KeyCode.RightArrow)) {
                right = true;
                rightBottom = true;
            }
            if(Input.GetKey(KeyCode.UpArrow)) {
                top = true;
            }

            if(flipControls) {
                bool temp = right;
                right = left;
                left = temp;

                bool temp2 = rightBottom;
                rightBottom = leftBottom;
                leftBottom = temp2;
            }

    	    if(brakingEnable) {
    	    	if(top) {
    	    		brakingEnginesBurn = true;
    	    	}
                if(leftBottom && rightBottom) {
                    bothEnginesBurn = true;
                } else if(rightBottom) {
    	    		rightEngineBurn = true;
    	    	} else if(leftBottom) {
    	    		leftEngineBurn = true;
    	    	}
    	    } else {
    	    	if(left && right) {
    	    		bothEnginesBurn = true;
    	    	} else if(left) {
    	    		leftEngineBurn = true;
    	    	} else if(right) {
    	    		rightEngineBurn = true;
    	    	}
    	    }

        	// here is the final logic for the vectors
        	if(bothEnginesBurn) {
            	flameTextureR.SetActive(true);
                flameTextureL.SetActive(true);
                rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * speedScale, rb.transform.right[0] * speedScale);
            }
            if(rightEngineBurn) {
            	flameTextureR.SetActive(true);
                rb.angularVelocity = rb.angularVelocity + 1.0f * rotationScale;
				rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
            }
            if(leftEngineBurn) {
            	flameTextureL.SetActive(true);
               	rb.angularVelocity = rb.angularVelocity - 1.0f * rotationScale;
            	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
           	}
            if(brakingEnginesBurn) {
            	rb.velocity = rb.velocity - new Vector2(-rb.transform.right[1] * brakingScale, rb.transform.right[0] * brakingScale);
            	flameTextureR_F.SetActive(true);
            	flameTextureL_F.SetActive(true);
            }

            // turn off engine flames here
            if (!bothEnginesBurn) {
                if(!rightEngineBurn) {
                    flameTextureR.SetActive(false);
                }
                if(!leftEngineBurn) {
                    flameTextureL.SetActive(false);
                }
            }
            if(!brakingEnginesBurn) {
                flameTextureR_F.SetActive(false);
                flameTextureL_F.SetActive(false);
            }
            
            // stop the engine sound if not any engine is on
            if(!bothEnginesBurn && !rightEngineBurn && !leftEngineBurn && !brakingEnginesBurn) {
                FindObjectOfType<AudioManager>().Stop("engine");
            } else {    // otherwise play sound
                FindObjectOfType<AudioManager>().Play("engine");
            }

            // slow down the rotation
            rb.angularVelocity = rotationSlowingScale * rb.angularVelocity;   
        } else {
            FindObjectOfType<AudioManager>().Stop("engine");
        }
    }

    // on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
    	FindObjectOfType<AudioManager>().PlayMusic("bang_1");
    	
        // take HP
        if (collision.otherCollider.GetType() == typeof(PolygonCollider2D))
        {
            float collisionPower = collision.relativeVelocity.magnitude;
        	HP = Mathf.Max(0, HP - collisionPower * collisionPower);

            float scaled = HP / MaxHP;
            HPImage.fillAmount = scaled;
            // flash red color
            HPImage.color = HPcollide;
            mainCamera.GetComponent<CameraController>().shake(collisionPower);

            spark.SetActive(true);
            spark.transform.position = collision.contacts[0].point;

            FindObjectOfType<AudioManager>().Play("scartch");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.GetType() == typeof(PolygonCollider2D))
        {
            float scaled = HP / MaxHP;
            HPImage.fillAmount = scaled;
            HPImage.color = Color.Lerp(HPempty, HPfull, scaled);

            spark.SetActive(false);
            FindObjectOfType<AudioManager>().Stop("scartch");
        }
    }

    // pickup for healing objects and keys
    void OnTriggerEnter2D(Collider2D collider)
    {
    //if the next line is here, the sound is played 
    //FindObjectOfType<AudioManager>().PlayMusic("pickup");
    
        if(collider.gameObject.tag == "energia") {
        	FindObjectOfType<AudioManager>().PlayMusic("pickup");
            HP = Mathf.Min(HP + healingHP, MaxHP);
            float scaled = HP / MaxHP;
            HPImage.fillAmount = scaled;
            HPImage.color = Color.Lerp(HPempty, HPfull, scaled);

            collider.gameObject.SetActive(false);
        }

        if(collider.gameObject.tag == "shield") {
            collider.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().PlayMusic("pickup");

            if(PlayerPrefs.GetInt("shieldUpgrade") == 0) {
                PlayerPrefs.SetInt("shieldUpgrade", 1);
                MaxHP = 200;
                HP = MaxHP;

                GameObject.Find("HPImage").SetActive(false);
                HPImageUpgraded.SetActive(true);
                HPImage = GameObject.Find("HealthBarUpgraded").GetComponent<Image>();

                float scaled = HP / MaxHP;
                HPImage.fillAmount = scaled;
                HPImage.color = Color.Lerp(HPempty, HPfull, scaled);                
            }
        }
        
    }

    public void freezePlayer() {
        gameOn = false;
    }
    
    public void unFreezePlayer() {
        gameOn = true;
    }   

}
