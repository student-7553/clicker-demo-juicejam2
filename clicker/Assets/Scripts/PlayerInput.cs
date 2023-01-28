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
    }

    [SerializeField]
    private InputActionReference leftMouseClick,
        escKeyboardClick;

    private void OnEnable()
    {
        leftMouseClick.action.performed += OnLeftMouseClick;
        escKeyboardClick.action.performed += OnEscClick;
    }

    private void OnDisable()
    {
        leftMouseClick.action.performed -= OnLeftMouseClick;
        escKeyboardClick.action.performed -= OnEscClick;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext obj)
    {
        if (PlayerInfo.current == null)
        {
            return;
        }
        PlayerInfo.current.handlePlayerClick();
    }

    private void OnEscClick(InputAction.CallbackContext obj) { }
}
