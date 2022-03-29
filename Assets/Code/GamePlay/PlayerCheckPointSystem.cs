using UnityEngine;

namespace Code.GamePlay
{
    public interface IPlayerCheckPointSystem
    {
        public void SetCheckPoint(Vector3 checkPointPosition);
        public void SetStartLevelCheckPoint(Vector3 checkPointPosition);
        public void ReturnToCheckPoint();
        public void ReturnToStart();

    }
    public class PlayerCheckPointSystem : IPlayerCheckPointSystem
    {
        private readonly DogView dogView;
        public PlayerCheckPointSystem(DogView dogView)
        {
            this.dogView = dogView;
        }

        private Vector3 currentCheckPoint;
        private Vector3 startLevelCheckPoint;

        public void SetCheckPoint(Vector3 checkPointPosition)
        {
            currentCheckPoint = checkPointPosition;
        }

        public void SetStartLevelCheckPoint(Vector3 checkPointPosition)
        {
            startLevelCheckPoint = checkPointPosition;
        }

        public void ReturnToCheckPoint()
        {
            dogView.transform.position = currentCheckPoint;
        }

        public void ReturnToStart()
        {
            dogView.transform.position = startLevelCheckPoint;
        }

    }
}