using Code.GamePlay.Camera;
using UnityEngine;

namespace Code.GamePlay.Triggers
{
    public interface ITriggersDetector
    {
        public void DetectTriggerSetPosition(ETriggers triggers, Vector3 newPosition);
        public void DetectTrigger(ETriggers trigger);

    }
    
    public class TriggersDetector : ITriggersDetector
    {
        private readonly IPlayerCheckPointSystem playerCheckPointSystem;
        private readonly IPlayerInput playerInput;
        private readonly CameraView cameraView;

        public TriggersDetector(IPlayerCheckPointSystem playerCheckPointSystem,
            IPlayerInput playerInput, CameraView cameraView)
        {
            this.playerCheckPointSystem = playerCheckPointSystem;
            this.playerInput = playerInput;
            this.cameraView = cameraView;
            
        }
        
        public void DetectTriggerSetPosition(ETriggers triggers, Vector3 newPosition)
        {
            switch (triggers)
            {
                case ETriggers.CheckPoint:
                    playerCheckPointSystem.SetCheckPoint(newPosition);
                    break;
                case ETriggers.FinishLevel:
                    playerInput.Actions.Player.Disable();
                    cameraView.OnCameraEndMovement += OnCameraEndMovement;
                    cameraView.MoveCamera(newPosition);
                    break;
            }
        }

        public void DetectTrigger(ETriggers trigger)
        {
            switch (trigger)
            {
                case ETriggers.Fall:
                    playerCheckPointSystem.ReturnToCheckPoint();
                    break;
            }
        }

        private void OnCameraEndMovement()
        {
            playerInput.Actions.Player.Enable();
            cameraView.OnCameraEndMovement -= OnCameraEndMovement;
        }
    }
}