using System;
using UnityEditor;

namespace Code
{
    public interface IPlayerInput
    {
        public Actions Actions { get; }
        public void Disable();
        public void Enable();
    }
    public class PlayerInput : IPlayerInput, IDisposable
    {
        private readonly Actions actions;
        public Actions Actions => actions;

        public PlayerInput()
        {
            actions = new Actions();
            actions.Enable();
        }
        
        public void Disable()
        {
            actions.Disable();
        }

        public void Enable()
        {
            actions.Enable();
        }

        public void Dispose()
        {
            actions?.Dispose();
        }
    }
}