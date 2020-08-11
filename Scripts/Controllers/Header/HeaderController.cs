using System;
using TMPro;
using UnityEngine;

namespace rtome.Scripts.Controllers.Header
{
    public class HeaderController : MonoBehaviour
    {
        public TMP_Text leftSideText;
        public TMP_Text rightSideText;

        public void SetHeaderText(string leftSide, string rightSide = "")
        {
            leftSideText.text = leftSide;
            rightSideText.text = rightSide;
        }
    }
}