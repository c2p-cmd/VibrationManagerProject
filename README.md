# Vibration Manager Project
This Project is a demo for the Vibration Manager

## Installation:

Go to [releases](https://github.com/c2p-cmd/VibrationManagerProject/releases) find the latest one and download the '.unitypackage' file and open that with your Unity Project 

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
