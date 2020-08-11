using System;
using UnityEngine;
using UnityEngine.UI;

namespace rtome.Scripts.Controllers.Footer
{
    public class FooterController : MonoBehaviour
    {
        public Button homeButton;

        public EventHandler<EventArgs> ReturnHomeRequest { get; set; }
        
        private void OnHomeButtonPressed()
        {
            ReturnHomeRequest?.Invoke(this, EventArgs.Empty);
        }

        private void Start()
        {
            homeButton.onClick.AddListener(OnHomeButtonPressed);
        }

        private void OnDestroy()
        {
            homeButton.onClick.RemoveAllListeners();
        }
    }
}