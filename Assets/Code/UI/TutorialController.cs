using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.UI
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private Canvas Ui;
        private IPlayerInput actions;
        
        [Inject]
        public void Construct(IPlayerInput actions)
        {
            this.actions = actions;

            actions.Actions.Player.Disable();
            
            actions.Actions.Menu.Any.performed += AnyOnperformed;
        }

        private void AnyOnperformed(InputAction.CallbackContext obj)
        {
            Ui.enabled = false;
            actions.Actions.Player.Enable();
            actions.Actions.Menu.Disable();
        }
    }
}