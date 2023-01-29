using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private InputActionReference leftMouseClick,
        escKeyboardClick,
        spaceKeyboardClick;

    private void OnEnable()
    {
        leftMouseClick.action.performed += OnLeftMouseClick;
        escKeyboardClick.action.performed += OnEscKeyboardClick;
        spaceKeyboardClick.action.performed += OnSpaceKeyboardClick;
    }

    private void OnDisable()
    {
        leftMouseClick.action.performed -= OnLeftMouseClick;
        escKeyboardClick.action.performed -= OnEscKeyboardClick;
        spaceKeyboardClick.action.performed -= OnSpaceKeyboardClick;
    }

    private void OnLeftMouseClick(InputAction.CallbackContext obj)
    {
        if (PlayerInfo.current == null)
        {
            return;
        }
        PlayerInfo.current.handlePlayerClick();
    }

    private void OnEscKeyboardClick(InputAction.CallbackContext obj) { }

    private void OnSpaceKeyboardClick(InputAction.CallbackContext obj)
    {
        if (PlayerInfo.current == null)
        {
            return;
        }
        PlayerInfo.current.handleTick();
    }
}
