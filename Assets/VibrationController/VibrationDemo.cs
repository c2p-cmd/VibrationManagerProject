using UnityEngine;
using UnityEngine.UI;

namespace VibrationController {
    public class VibrationDemo : MonoBehaviour {
        [SerializeField] private VibrationManager _vibrationManager;
        [SerializeField] private Text textGo;
        [SerializeField] private Slider leftSlider;
        [SerializeField] private Slider rightSlider;
        [SerializeField] private Slider timeSlider;

        private void Start() {
            textGo.text = _vibrationManager.GamepadCount.ToString();
            _vibrationManager.onConnected += gamepad => {
                textGo.text += $"Gamepad {gamepad.name} connected";
            };
            _vibrationManager.onDisconnected += gamepad => {
                textGo.text += $"Gamepad {gamepad.name} disconnected";
            };
        }

        public void Vibrate() {
            _vibrationManager.VibrateAllGamepads(leftSlider.value, rightSlider.value, timeSlider.value);
        }
    }
}