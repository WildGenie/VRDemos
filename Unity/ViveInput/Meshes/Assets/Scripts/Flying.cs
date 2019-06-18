using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Flying : MonoBehaviour
{
    public GameObject head;
    public GameObject rightHand;
    public int speed;

    // Use this for initialization
    void Start()
    {
    }

    private void Awake()
    {
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.DPadUp, ButtonEventType.Down, decreaseSpeed);
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.DPadDown, ButtonEventType.Down, increaseSpeed);
    }

    private void increaseSpeed()
    {
        if (speed > 5) speed -= 5;
    }

    private void decreaseSpeed()
    {
        if (speed < 80) speed += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.FullTrigger) || ViveInput.GetPress(HandRole.LeftHand, ControllerButton.FullTrigger)) flying();
    }

    private void flying()
    {
        float rightHandTurnX = rightHand.transform.eulerAngles.x;
        float rightHandTurnY = rightHand.transform.eulerAngles.y;

        float rise = 0.0f;
        float pValue = 0.0f;

        if (rightHandTurnX > 270 && rightHandTurnX <= 360)
        {
            pValue = 360 - rightHandTurnX;
            rise = pValue / 30;
        }
        else if (rightHandTurnX > 0 && rightHandTurnX <= 90)
        {
            pValue = 0 - rightHandTurnX;
            rise = pValue / 30;
        }
        else if (rightHandTurnX == 0) rise = 0.0f;

        float moveX = 0.0f;
        float moveZ = 0.0f;

        if (rightHandTurnY >= 0 && rightHandTurnY < 90)
        {
            pValue = rightHandTurnY / 90;
            moveX = 2.0f * pValue;
            moveZ = 2.0f * (1 - pValue);
        }
        else if (rightHandTurnY >= 90 && rightHandTurnY < 180)
        {
            pValue = (rightHandTurnY - 90) / 90;
            moveX = 2.0f * (1 - pValue);
            moveZ = -2.0f * pValue;
        }
        else if (rightHandTurnY >= 180 && rightHandTurnY < 270)
        {
            pValue = (rightHandTurnY - 180) / 90;
            moveX = -2.0f * pValue;
            moveZ = -2.0f * (1 - pValue);
        }
        else if (rightHandTurnY >= 270 && rightHandTurnY <= 360)
        {
            pValue = (rightHandTurnY - 270) / 90;
            moveX = -2.0f * (1 - pValue);
            moveZ = 2.0f * pValue;
        }

        Vector3 direction = new Vector3(moveX, rise, moveZ);

        direction = direction / (250 / speed);
        transform.position += direction;
    }

    private void supermanFlying()
    {
        Vector3 direction = (rightHand.transform.position - head.transform.position) / speed;
        transform.position += direction;
    }
}
