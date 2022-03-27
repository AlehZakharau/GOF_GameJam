using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private EContext context;

        private IMediator mediator;
        public void Construct(IMediator mediator)
        {
            this.mediator = mediator;
        }
        private void Start()
        {
            button.onClick.AddListener(PushButton);
        }

        private void PushButton()
        {
            mediator.Notify(context);
            Debug.Log($"Context: {context}");
        }
    }
}