using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "locked" that can be checked in Update()
    private bool _locked = true;

    private AudioSource _audio_source = null;

    [Header("Sounds")]
    public AudioClip door_locked = null;
    public AudioClip door_opened = null;

    void Awake()
    {
        _audio_source = gameObject.GetComponent<AudioSource>();
        _audio_source.clip = door_locked;
        _audio_source.playOnAwake = false;
        _audio_source.Play();
    }

    void Update() {
        // If the door is unlocked and it is not fully raised
        if (_locked != true && gameObject.transform.position.y < 19.0f)
        {
            // Animate the door raising up
            transform.position = gameObject.transform.position + new Vector3(0.0f, 10.0f * Time.deltaTime, 0.0f);
        }
    }

    public void Unlock()
    {
        // You'll need to set "locked" to true here
        // (I'm not sure why the note here says "true", should be false)
        _locked = false;

        _audio_source.clip = door_opened;
        _audio_source.Play();
    }
}
