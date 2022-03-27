using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

namespace Code.UI
{
    public class TutorialService : MonoBehaviour
    {
        [SerializeField] private MediatorUI mediator;
        [SerializeField] private Toggle tutorialCheck;
        [SerializeField] private TMP_Text supportText;
        //[SerializeField] private MilkCollector milkCollector;
        
        private Actions actions;
        private bool isTutorial;
        private int milkCount;
        private void Start()
        {
            supportText.gameObject.SetActive(false);
            //actions = PlayerInput.Instance.Actions;
            if (!tutorialCheck.isOn)
            {
                isTutorial = true;
                mediator.Notify(EContext.Tutorial);
            }
            //milkCollector.onAddingMilk += OnEatingMilk;
        }

        private void OnEatingMilk()
        {
            milkCount++;
            if(milkCount > 3) return;
            switch (milkCount)
            {
                case 1:
                    supportText.text = $"Press 'Space' to jump";
                    StartCoroutine(ShowSupportText());
                    break;
                case 2:
                    supportText.text = $"You unlocked 'Double Jump'";
                    StartCoroutine(ShowSupportText());
                    break;
                case 3:
                    supportText.text = $"You unlocked 'Triple Jump'";
                    StartCoroutine(ShowSupportText());
                    break;
            }
        }

        // private void Update()
        // {
        //     //var any = actions.Menu.PressAny.triggered;
        //
        //     if (any && isTutorial)
        //     {
        //         isTutorial = false;
        //         navigation.Notify(EContext.Continue);
        //     }
        // }

        private IEnumerator ShowSupportText()
        {
            supportText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            supportText.gameObject.SetActive(false);
        }
    }
}