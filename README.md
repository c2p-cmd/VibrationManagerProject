# Vibration Manager Project
This Project is a demo for the Vibration Manager

## Installation:

Go to [releases](https://github.com/c2p-cmd/VibrationManagerProject/releases) find the latest one and download the '.unitypackage' file and open that with your Unity Project 

## Demo:

### Vibration Method:
```csharp
using VibrationController;

...

[SerializeField] private VibrationManager _vibrationManager;

...

// speeds of left and right motors
float leftMotorSpeed = 0.5f;
float rightMotorSpeed = 0.5f;

// time to vibrate the gamepad for
float time = 1f;

// call method!
_vibrationManager.VibrateAllGamepads(leftSpeed, rightSpeed, time);
```

### Callbacks for connection and disconnection
```csharp
using VibrationController;

...

[SerializeField] private VibrationManager _vibrationManager;

...

_vibrationManager.onConnected += gamepad => {
    print($"Gamepad {gamepad.name} connected");
};
_vibrationManager.onDisconnected += gamepad => {
    print($"Gamepad {gamepad.name} disconnected");
};
```

### Change Color of LightBar (For DualShocks only)
```csharp
using VibrationController;

...

[SerializeField] private VibrationManager _vibrationManager;

...

// color to set on the lightbar
Color color = Color.cyan;

// call method!
_vibrationManager.SetLightBarColor(color);
```
