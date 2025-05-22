using UnityEngine;

public class SerialInputToKeyMainMenu : MonoBehaviour
{
    public SerialController serialController;

    void Update()
    {
        string message = serialController.ReadSerialMessage();
        if (message == null) return;

        switch (message)
        {
            case "UP":
                SimulateKey(KeyCode.UpArrow);
                break;
            case "DOWN":
                SimulateKey(KeyCode.DownArrow);
                break;
            case "ENTER":
                SimulateKey(KeyCode.Return);
                break;
        }
    }

    void SimulateKey(KeyCode key)
    {
        // Caranya: bisa trigger lewat custom event, send ke input manager, atau langsung inject state.
        // Cara paling simpel, panggil method controller langsung (lihat penyesuaian di bawah).
        MainMenuController controller = FindObjectOfType<MainMenuController>();
        if (controller != null)
        {
            if (key == KeyCode.UpArrow) controller.SendMessage("MoveUp");
            else if (key == KeyCode.DownArrow) controller.SendMessage("MoveDown");
            else if (key == KeyCode.Return) controller.SendMessage("SelectMenuItem");
        }
    }
}
