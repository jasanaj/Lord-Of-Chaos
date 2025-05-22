using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDestroyController : MonoBehaviour
{
    [Header("Visual Button Sprite")]
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    [Header("Input Settings")]
    public KeyCode keyToPress = KeyCode.Space;
    public SerialController serialController;

    private Collider2D currentSphere = null;
    private string lastMessage = "";

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Keyboard Input
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;
            TryDestroySphere();
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }

        // Serial Input
        string message = serialController != null ? serialController.ReadSerialMessage() : null;
        if (!string.IsNullOrEmpty(message))
        {
            lastMessage = message.Trim();

            if (lastMessage == "L")
            {
                theSR.sprite = pressedImage;
                TryDestroySphere();
            }
        }
    }

    void TryDestroySphere()
    {
        if (currentSphere != null)
        {
            Debug.Log("Tombol ditekan dan sphere menyentuh cube — menghancurkan sphere.");
            Destroy(currentSphere.gameObject);
            currentSphere = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sphere"))
        {
            currentSphere = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == currentSphere)
        {
            currentSphere = null;
        }
    }
}