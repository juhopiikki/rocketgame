using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWin : MonoBehaviour {

	public GameObject player;
    public bool fade;
    private SpriteRenderer render;
    public ParticleSystem render1, render2;

    void Start() {
        fade = false;
        render = player.GetComponent<SpriteRenderer>();
    }

	// on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerPrefs.SetInt("lvl1", 1);
        fade = true;
        // SceneManager.LoadScene("MainMenu");     
    }

    void Update() {
        if (fade) {
            if(render.color.a > 0) {
                Color colour = render.color;
                colour.a -= Time.deltaTime / 6.0f;
                render.color = colour;

                var main = render1.main;
                main.startColor = new ParticleSystem.MinMaxGradient(colour);

                var main2 = render2.main;
                main2.startColor = new ParticleSystem.MinMaxGradient(colour);

            } else {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}
