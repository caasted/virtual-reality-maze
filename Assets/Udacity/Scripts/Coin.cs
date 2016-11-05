using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Create a reference to the Poof Prefab
    public GameObject PoofReference;

    private enum State
    {
        Idle,
        Focused,
        Clicked,
        Collected
    }

    [SerializeField]
    private State _state = State.Idle;
    private AudioSource _audio_source = null;
    private Material _material = null;
    private float _click_start_time = 0.0f;

    [Header("Material")]
    public Material material = null;

    [Header("Sounds")]
    public AudioClip coin_click = null;

    void Awake()
    {
        _material = Instantiate(material);
        _audio_source = gameObject.GetComponent<AudioSource>();
        _audio_source.clip = coin_click;
        _audio_source.playOnAwake = false;
    }

    void Update()
    {
        switch (_state)
        {
            case State.Clicked:
                Clicked();
                break;

            case State.Collected:
                Collected();
                break;

            default:
                break;
        }
    }

    public void Click()
    {
        _click_start_time = Time.time; // Record time of click event

        _audio_source.Play();

        // Instatiate the Poof Prefab where the coin is located
        // Make sure the poof animates vertically
        Object.Instantiate(PoofReference, gameObject.transform.position, Quaternion.identity * Quaternion.Euler(-90f, 0f, 0f));

        // Shrink the coin so that it vanishes, but isn't destroyed until the sound finishes playing
        gameObject.transform.localScale = Vector3.one * 0f;

        _state = State.Clicked;
    }

    public void Clicked()
    {
        if (Time.time > _click_start_time + 3)
        {
            _state = State.Collected;
        }
    }

    public void Collected()
    {
        // Destroy coin object
        Destroy(gameObject);
    }
}
