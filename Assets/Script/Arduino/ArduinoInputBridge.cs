using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInputBridge : MonoBehaviour
{
    public SerialController serialController;

    private Dictionary<KeyCode, bool> pressed = new();
    private Dictionary<KeyCode, bool> down = new();
    private Dictionary<KeyCode, bool> up = new();

    void Update()
    {
        string message = serialController.ReadSerialMessage();
        if (message == null) return;

        Debug.Log("Message dari Arduino: " + message);
        HandleArduinoInput(message);
    }

    void LateUpdate()
    {
        // Reset one-frame flags
        down.Clear();
        up.Clear();
    }

    private void HandleArduinoInput(string message)
    {
        KeyCode key = KeyCode.None;
        bool isPress = false;

        switch (message)
        {
            case "Z_PRESSED": key = KeyCode.Z; isPress = true; break;
            case "Z_RELEASED": key = KeyCode.Z; isPress = false; break;
            case "X_PRESSED": key = KeyCode.X; isPress = true; break;
            case "X_RELEASED": key = KeyCode.X; isPress = false; break;
            case "C_PRESSED": key = KeyCode.C; isPress = true; break;
            case "C_RELEASED": key = KeyCode.C; isPress = false; break;
            case "B_PRESSED": key = KeyCode.B; isPress = true; break;
            case "B_RELEASED": key = KeyCode.B; isPress = false; break;
            case "N_PRESSED": key = KeyCode.N; isPress = true; break;
            case "N_RELEASED": key = KeyCode.N; isPress = false; break;
            case "M_PRESSED": key = KeyCode.M; isPress = true; break;
            case "M_RELEASED": key = KeyCode.M; isPress = false; break;
        }


        if (key != KeyCode.None)
        {
            bool wasPressed = pressed.ContainsKey(key) && pressed[key];
            pressed[key] = isPress;

            if (isPress && !wasPressed) down[key] = true;
            if (!isPress && wasPressed) up[key] = true;
        }
    }

    public bool GetKeyDown(KeyCode key) => down.ContainsKey(key) && down[key];
    public bool GetKeyUp(KeyCode key) => up.ContainsKey(key) && up[key];
    public bool GetKey(KeyCode key) => pressed.ContainsKey(key) && pressed[key];
}
