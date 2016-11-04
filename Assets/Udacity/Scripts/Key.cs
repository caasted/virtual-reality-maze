using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
    //Create a reference to the KeyPoofPrefab and Door
    public GameObject PoofReference;
    public GameObject DoorReference;

    private enum State
    {
        Idle,
        Focused,
        Clicked,
        Collected
    }

    [SerializeField]
    private State _state = State.Idle;
    private Color _color_original = new Color(0.0f, 0.0f, 1.0f, 0.8f);
    private Color _color = Color.white;
    private float _animated_lerp = 1.0f;
    private AudioSource _audio_source = null;
    private Material _material = null;

    [Header("State Blend Speeds")]
    public float lerp_idle = 0.0f;
    public float lerp_focus = 0.0f;
    public float lerp_clicked = 0.0f;

    [Header("Material")]
    public Material material = null;
    public Color color_hilight = new Color(0.5f, 0.5f, 1.0f, 0.8f);

    [Header("Sounds")]
    public AudioClip key_click = null;
    
    void Awake()
    {
        _material = Instantiate(material);
        _color_original = _material.color;
        _color = _color_original;
        _audio_source = gameObject.GetComponent<AudioSource>();
        _audio_source.clip = key_click;
        _audio_source.playOnAwake = false;
    }

    void Update()
	{
        //Bonus: Key Animation
        switch (_state)
        {
            case State.Idle:
                Idle();
                break;

            case State.Focused:
                Focused();
                break;

            case State.Clicked:
                Clicked();
                break;

            case State.Collected:
                Collected();
                break;

            default:
                break;
        }

        gameObject.GetComponentInChildren<MeshRenderer>().material.color = _color;
    }

    public void Enter()
    {
        _state = _state == State.Idle ? State.Focused : _state;
    }

    public void Exit()
    {
        _state = State.Idle;
    }

    public void Click()
    {
        _state = _state == State.Focused ? State.Clicked : _state;

        _audio_source.Play();

        // Instatiate the KeyPoof Prefab where this key is located
        // Make sure the poof animates vertically
        Object.Instantiate(PoofReference, new Vector3(-2f, 2.5f, -12f), Quaternion.identity * Quaternion.Euler(-90f, 0f, 0f));
    }

    private void Idle()
    {
        Color color = Color.Lerp(_color_original, color_hilight, _animated_lerp);
        _color = Color.Lerp(_color, color, lerp_idle);
    }

    public void Focused()
    {
        Color color = Color.Lerp(_color_original, color_hilight, _animated_lerp);
        _color = Color.Lerp(_color, color, lerp_focus);
    }

    public void Clicked()
	{
        _color = Color.Lerp(_color, color_hilight, lerp_clicked);

        // Call the Unlock() method on the Door

        // Destroy the key. Check the Unity documentation on how to use Destroy
        Destroy(gameObject);

        _state = _state == State.Clicked ? State.Collected : _state;
    }

    public void Collected()
    {
        // Empty state for after object is destroyed
    }
}
