using UnityEngine;

namespace Code.GamePlay.Triggers
{
    public interface ITriggersDetector
    {
        public void SetCheckPoint(Vector3 checkPointPosition);
        public void DetectTrigger(ETriggers trigger);

    }
    
    public class TriggersDetector : ITriggersDetector
    {
        private readonly IPlayerCheckPointSystem playerCheckPointSystem;

        public TriggersDetector(IPlayerCheckPointSystem playerCheckPointSystem)
        {
            this.playerCheckPointSystem = playerCheckPointSystem;
        }
        
        public void SetCheckPoint(Vector3 checkPointPosition)
        {
            playerCheckPointSystem.SetCheckPoint(checkPointPosition);
        }

        public void DetectTrigger(ETriggers trigger)
        {
            switch (trigger)
            {
                case ETriggers.Fall:
                    playerCheckPointSystem.ReturnToCheckPoint();
                    break;
                case ETriggers.FinishLevel:
                    //camera Move
                    break;
            }
        }
    }
}