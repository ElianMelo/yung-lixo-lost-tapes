using UnityEngine;

public class SelfDestroyTutorial : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.Tab;

    private void Update() { if (Input.GetKeyDown(keyToPress)) Destroy(gameObject); }
}
