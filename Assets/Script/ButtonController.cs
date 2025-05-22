using UnityEngine;

public enum DrumKey
{
    Z, X, C, B, N, M
}

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public DrumKey drumKey;  // Pilih dari enum
    private KeyCode keyToPress;

    private bool isPressed = false;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        keyToPress = (KeyCode)System.Enum.Parse(typeof(KeyCode), drumKey.ToString());
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Press();
        }

        if (Input.GetKeyUp(keyToPress))
        {
            Release();
        }
    }

    public void Press()
    {
        if (!isPressed)
        {
            theSR.sprite = pressedImage;
            isPressed = true;

            // Tambahkan efek seperti hancurkan note, dll di sini
        }
    }

    public void Release()
    {
        if (isPressed)
        {
            theSR.sprite = defaultImage;
            isPressed = false;
        }
    }

    public DrumKey GetDrumKey()
    {
        return drumKey;
    }
}
