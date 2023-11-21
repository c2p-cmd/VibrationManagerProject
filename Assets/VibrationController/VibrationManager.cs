using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;

namespace VibrationController {
    public class VibrationManager : MonoBehaviour {
        /// <summary>
        /// Public Delegate method for when a new device gets connected
        /// 
        /// <para>
        /// Tells you which "Gamepad" got connected
        /// </para>
        /// </summary>
        public UnityAction<Gamepad> onConnected;
        
        /// <summary>
        /// Public Delegate method for when a device gets disconnected
        /// <para>
        /// Tells you which "Gamepad" got disconnected
        /// </para>
        /// </summary>
        public UnityAction<Gamepad> onDisconnected;
        
        /// <summary>
        /// Coroutine to stop and start
        /// </summary>
        private Coroutine _coroutine;
        
        private void OnEnable() {
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable() {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        private void Start() {
            foreach (Gamepad gamepad in Gamepad.all) {
                onConnected?.Invoke(gamepad);
            }
        }

        /// <summary>
        /// <returns>Number of gamepads currently connected</returns> 
        /// </summary>
        public int GamepadCount => Gamepad.all.Count;
        
        public void VibrateHandheld() {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }

        /// <summary>
        /// Method to Vibrate All Connected Gamepads
        /// </summary>
        /// <param name="leftSpeed"> To set left motor's speed </param>
        /// <param name="rightSpeed"> To set right motor's speed </param>
        /// <param name="time"> Determines how long will it vibrate for </param>
        public void VibrateAllGamepads(float leftSpeed = 0.5f, float rightSpeed = 0.5f, float time = 1f) {
            if (_coroutine != null) {
                StopCoroutine(_coroutine);
            }

            foreach (Gamepad gamepad in Gamepad.all) {
                _coroutine = StartCoroutine(VibrateGamepad(gamepad, leftSpeed, rightSpeed));
            }
        }

        /// <summary>
        /// IEnumerator to Vibrate a specific Gamepad
        /// </summary>
        /// <param name="gamepad"> The gamepad object we want to vibrate </param>
        /// <param name="leftSpeed"> To set left motor's speed </param>
        /// <param name="rightSpeed"> To set right motor's speed </param>
        /// <param name="time"> Determines how long will it vibrate for </param>
        /// <returns></returns>
        private IEnumerator VibrateGamepad(IDualMotorRumble gamepad, float leftSpeed = 0.5f, float rightSpeed = 0.5f, float time = 0.5f) {
            gamepad.SetMotorSpeeds(leftSpeed, rightSpeed);
            yield return new WaitForSecondsRealtime(time);
            gamepad.SetMotorSpeeds(0f, 0f);
        }

        /// <summary>
        /// Method to Set Color of LightBar on DualShock Gamepad
        /// </summary>
        /// <param name="color"> Color we want to set on the gamepad lightbar </param>
        public void SetLightBarColor(Color color) {
            var dualShockGamepads = Gamepad.all.Where(gamepad => gamepad is DualShockGamepad).Cast<DualShockGamepad>();
            foreach (DualShockGamepad dualShock in dualShockGamepads) {
                SetLightBarColorTo(dualShock, color);
            }
        }

        /// <summary>
        /// Helper to Set Color of LightBar
        /// </summary>
        /// <param name="gamepad"> The DualShock object we want to change color </param>
        /// <param name="color"> Color we want to set on the gamepad lightbar </param>
        private void SetLightBarColorTo(IDualShockHaptics gamepad, Color color) {
            gamepad.SetLightBarColor(color);
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change) {
            if (device is not Gamepad gamepad) {
                return;
            }

            switch (change) {
                case InputDeviceChange.Removed:
                case InputDeviceChange.Disconnected:
                    onDisconnected?.Invoke(gamepad);
                    StartCoroutine(VibrateGamepad(gamepad, 0f, 0f, time: 0f));
                    break;
                case InputDeviceChange.Added:
                case InputDeviceChange.Reconnected:
                    onConnected?.Invoke(gamepad);
                    StartCoroutine(VibrateGamepad(gamepad, 0.1f, 0.1f, time: 0.25f));
                    break;
                case InputDeviceChange.Enabled:
                    break;
                case InputDeviceChange.Disabled:
                    break;
                case InputDeviceChange.UsageChanged:
                    break;
                case InputDeviceChange.ConfigurationChanged:
                    break;
                case InputDeviceChange.SoftReset:
                    break;
                case InputDeviceChange.HardReset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(change), change, null);
            }
        }
    }
}
