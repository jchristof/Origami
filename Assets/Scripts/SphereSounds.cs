using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSounds : MonoBehaviour {

    private AudioSource audioSource;

    private float defaultVolume = 1.0f;

    private AudioClip impactClip;

    private AudioClip rollingClip;

    private bool rolling;


	// Use this for initialization
	void Start () {
	    audioSource = gameObject.AddComponent<AudioSource>();
	    audioSource.playOnAwake = false;
	    audioSource.spatialize = true;
	    audioSource.spatialBlend = 1.0f;
	    audioSource.dopplerLevel = 0.1f;

	    defaultVolume = audioSource.volume;

	    impactClip = Resources.Load<AudioClip>("Impact");
	    rollingClip = Resources.Load<AudioClip>("Rolling");
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude >= 0.1f) {
            audioSource.clip = impactClip;
            audioSource.volume = 0.7f;
            audioSource.Play();
        }    
    }

    void OnCollisionStay(Collision collision) {
        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();

        if (!rollingClip && rigid.velocity.magnitude > 0.01f) {
            rolling = true;
            audioSource.clip = rollingClip;
            audioSource.volume = defaultVolume;
            audioSource.Play();
        }
        else if (rolling && rigid.velocity.magnitude < 0.01f) {
            rolling = false;
            audioSource.Stop();
        }
    }

    void OnCollisionExit(Collision collision) {
        if (rolling) {
            rolling = false;
            audioSource.Stop();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
