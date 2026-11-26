using UnityEngine;
using UnityEngine.InputSystem;

public class DemoController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public GameObject southGate;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float x = 0f;
        float z = 0f;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) x -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) x += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) z -= 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) z += 1f;

        Vector3 move = new Vector3(x, 0f, z).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (kb.spaceKey.wasPressedThisFrame && southGate != null)
        {
            southGate.SetActive(!southGate.activeSelf);
        }
    }
}