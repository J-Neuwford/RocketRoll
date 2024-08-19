using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key down.");
            Application.Quit();
        }
    }
}
