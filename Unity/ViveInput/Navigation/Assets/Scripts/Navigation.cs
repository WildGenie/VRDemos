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
    public GameObject head;
    public GameObject navigationHand;
    public float speed = 5.0F;
    public HTC.UnityPlugin.Vive.HandRole speedHand = HandRole.RightHand;

    // Use this for initialization
    void Start()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        ViveInput.AddListenerEx(speedHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.AddListenerEx(speedHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(speedHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.RemoveListenerEx(speedHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    private void increaseSpeed()
    {
        if (speed > 5.0f) speed -= 5.0f;
    }

    private void decreaseSpeed()
    {
        if (speed < 80.0f) speed += 5.0f;
    }

    // Update is called once per frame
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
        float navigationHandTurnX = navigationHand.transform.eulerAngles.x;
        float navigationHandTurnY = navigationHand.transform.eulerAngles.y;

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

    private void SupermanFlying()
    {
        Vector3 direction = (navigationHand.transform.position - head.transform.position) / speed;
        transform.position += direction;
    }
}
