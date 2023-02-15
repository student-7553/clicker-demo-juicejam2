using UnityEngine;
using UnityEngine.InputSystem;

public delegate void ButtonClicked();

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
    }

    [SerializeField]
    private InputActionReference leftMouseClick,
        escKeyboardClick,
        spaceKeyboardClick;

    public ButtonClicked leftMouseClickEvent;

    private void OnEnable()
    {
        leftMouseClick.action.performed += OnLeftMouseClick;
        escKeyboardClick.action.performed += OnEscKeyboardClick;
    }

    private void OnDisable()
    {
        leftMouseClick.action.performed -= OnLeftMouseClick;
        escKeyboardClick.action.performed -= OnEscKeyboardClick;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext obj)
    {
        leftMouseClickEvent?.Invoke();
    }

    private void OnEscKeyboardClick(InputAction.CallbackContext obj) { }
}
