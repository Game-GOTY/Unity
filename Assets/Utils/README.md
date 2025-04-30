### Utils


## Set timer class
```csharp
// Start a timer that will execute after 3 seconds

setTimeout(int second, action x)

int timerId = TimerUtility.SetTimeout(3f, () => {
    Debug.Log("This will execute after 3 seconds");
});

// Cancel the timer before it executes
TimerUtility.ClearTimeout(timerId);
```