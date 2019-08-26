using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Class for Flying in a virtual environment using
/// Unity, OpenVR and Vive Input Utility
/// </summary>
public class Navigation : MonoBehaviour
{
    private const float zero = 0.0f;
    /// <summary>
    /// Scalar controlling the change in position. Can be increased
    /// and decreased.
    /// </summary>
    public float speed = 2.0F;
    /// <summary>
    /// Delta to change the speed. Actually we do not
    /// use Newtonian physics, but increase speed by
    /// adding or subracting a delta to the scalar defining
    /// the speed.
    /// </summary>
    public float speedDelta = 1.0F;
    /// <summary>
    /// Maximum value for the speed scalar.
    /// </summary>
    public float speedMaximum = 20.0f;
    /// <summary>
    /// Which controller is used to move and to control 
    /// the speed.
    /// </summary>
    public HTC.UnityPlugin.Vive.HandRole speedHand = HandRole.RightHand;

    // Use this for initialization
    void Start()
    {
    }

    /// <summary>
    /// Use the Touchpad-Buttons Up and Down to increase or decrease speed
    /// We use the Unity Event system to register the callbacks decreaseSpeed
    /// and increaseSpeed.
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(speedHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.AddListenerEx(speedHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    /// <summary>
    /// Detach the Event-Listeners for the Controller
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(speedHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.RemoveListenerEx(speedHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    /// <summary>
    /// Increase the speed. 
    /// We only change the speed by adding speedDelta,
    /// if the resulting speed is less than speedMax.
    /// </summary>
    private void increaseSpeed()
    {
        speed = Mathf.Clamp(speed + speedDelta, zero, speedMaximum);
    }

    private void decreaseSpeed()
    {
        speed = Mathf.Clamp(speed - speedDelta, zero, speedMaximum);
    }

    /// <summary>
    /// Get the Events and change the speed or the position
    /// </summary>
    void Update()
    {
        if (ViveInput.GetPress(speedHand, ControllerButton.FullTrigger)) Fly();
    }
    /// <summary>
    /// Get the transformation of our controller and use its Euler angles
    /// to move in the scene.
    /// </summary>
    private void Fly()
    {
        float navigationHandTurnX = speedHand.transform.eulerAngles.x;
        float navigationHandTurnY = speedHand.transform.eulerAngles.y;

        float rise = zero;
        float pValue = zero;

        if (navigationHandTurnX > 270.0f && navigationHandTurnX <= 360.0f)
        {
            pValue = 360.0f - navigationHandTurnX;
            rise = pValue / 30.0f;
        }
        else if (navigationHandTurnX > zero && navigationHandTurnX <= 90.0f)
        {
            pValue = zero - navigationHandTurnX;
            rise = pValue / 30.0f;
        }
        else if (navigationHandTurnX == 0) rise = 0.0f;

        float moveX = zero;
        float moveZ = zero;

        if (navigationHandTurnY >= zero && navigationHandTurnY < 90.0f)
        {
            pValue = navigationHandTurnY / 90.0f;
            moveX = 2.0f * pValue;
            moveZ = 2.0f * (1.0f - pValue);
        }
        else if (navigationHandTurnY >= 90.0f && navigationHandTurnY < 180.0f)
        {
            pValue = (navigationHandTurnY - 90.0f) / 90.0f;
            moveX = 2.0f * (1.0f - pValue);
            moveZ = -2.0f * pValue;
        }
        else if (navigationHandTurnY >= 180.0f && navigationHandTurnY < 270.0f)
        {
            pValue = (navigationHandTurnY - 180.0f) / 90.0f;
            moveX = -2.0f * pValue;
            moveZ = -2.0f * (1.0f - pValue);
        }
        else if (navigationHandTurnY >= 270.0f && navigationHandTurnY <= 360.0f)
        {
            pValue = (navigationHandTurnY - 270.0f) / 90.0f;
            moveX = -2.0f * (1.0f - pValue);
            moveZ = 2.0f * pValue;
        }

        Vector3 direction = new Vector3(moveX, rise, moveZ);

        direction = direction / (250.0f / speed);
        transform.position += direction;
    }
}
