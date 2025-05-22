using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteKey
{
    Z, X, C, B, N, M
}

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public NoteKey noteKey;

    private KeyCode keyToPress;

    public GameObject greatEffect, goodEffect, perfectEffect, missEffect;

    private void Start()
    {
        keyToPress = (KeyCode)System.Enum.Parse(typeof(KeyCode), noteKey.ToString());
    }

    private void Update()
    {
        // Keyboard input
        if (Input.GetKeyDown(keyToPress))
        {   
            OnExternalInput();
        }
    }

    private void OnEnable()
    {
        if (InputReceiver.instance != null)
            InputReceiver.instance.RegisterNote(this);
    }

    private void OnDisable()
    {
        if (InputReceiver.instance != null)
            InputReceiver.instance.UnregisterNote(this);
    }

    public void OnExternalInput()
    {
        if (!canBePressed) return;

        gameObject.SetActive(false);

        float yPos = Mathf.Abs(transform.position.y);
        if (yPos > 0.25f)
        {
            Debug.Log("Good");
            GameManager.instance.GoodHit();
            Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
        }
        else if (yPos > 0.05f)
        {
            Debug.Log("Great");
            GameManager.instance.GreatHit();
            Instantiate(greatEffect, transform.position, greatEffect.transform.rotation);
        }
        else
        {
            Debug.Log("Perfect");
            GameManager.instance.PerfectHit();
            Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
            InputReceiver.instance.RegisterNote(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            if (gameObject.activeInHierarchy)
            {
                canBePressed = false;
                InputReceiver.instance.UnregisterNote(this);
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
