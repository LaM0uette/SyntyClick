using System;
using UnityEngine;

namespace PlayerController
{
    public class InputReader : MonoBehaviour
    {
        public Action ClickAction { get; set; }

        private void OnClick() => ClickAction?.Invoke();
    }
}
