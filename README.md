# Vibration Manager Project
This Project is a demo for the Vibration Manager

## Demo:

### Vibration Method:
```csharp
// speeds of left and right motors
float leftMotorSpeed = 0.5f;
float rightMotorSpeed = 0.5f;

// time to vibrate the gamepad for
float time = 1f;

// call method!
VibrateAllGamepads(leftSpeed, rightSpeed, time);
```

### Callbacks for connection and disconnection
```csharp
[SerializeField] private VibrationManager _vibrationManager;

...

_vibrationManager.onConnected += gamepad => {
    print($"Gamepad {gamepad.name} connected");
};
_vibrationManager.onDisconnected += gamepad => {
    print($"Gamepad {gamepad.name} disconnected");
};
```
