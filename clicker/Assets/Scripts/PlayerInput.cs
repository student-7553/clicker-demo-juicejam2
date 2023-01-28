using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput current;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(this);
    }

    [SerializeField]
    private InputActionReference leftMouseClick,
        escKeyboardClick;

    private void OnEnable()
    {
        leftMouseClick.action.performed += OnLeftMouseClick;
        escKeyboardClick.action.performed += OnRightMouseClick;
    }

    private void OnDisable()
    {
        leftMouseClick.action.performed -= OnLeftMouseClick;
        escKeyboardClick.action.performed -= OnRightMouseClick;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext obj) { }

    private void OnRightMouseClick(InputAction.CallbackContext obj) { }
}
