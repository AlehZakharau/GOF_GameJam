using UnityEngine;

namespace Code.UI
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private EWindows windowType;
        [SerializeField] private Canvas windowCanvas;
        public EWindows WindowType => windowType;

        public void OpenWindow()
        {
            windowCanvas.enabled = true;
        }

        public void CloseWindow()
        {
            windowCanvas.enabled = false;
        }
    }
}