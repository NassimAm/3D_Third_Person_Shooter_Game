using UnityEngine;

public class KeyboardInput : InputController
{
    public override void CheckInput()
    {
        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Horizontal");

        Up = (vert > 0);
        Down = (vert < 0);
        Left = (horz < 0);
        Right = (horz > 0);

        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Run = Input.GetKey(KeyCode.LeftShift);
        shouldRespawn = Input.GetButton("Fire3");

        slot1 = Input.GetKey(KeyCode.Alpha1);
        slot2 = Input.GetKey(KeyCode.Alpha2);
        slot3 = Input.GetKey(KeyCode.Alpha3);
        slot4 = Input.GetKey(KeyCode.Alpha4);
        slot5 = Input.GetKey(KeyCode.Alpha5);
        slot6 = Input.GetKey(KeyCode.Alpha6);
        slot7 = Input.GetKey(KeyCode.Alpha7);
        slot8 = Input.GetKey(KeyCode.Alpha8);
        slot9 = Input.GetKey(KeyCode.Alpha9);

    }

    public void LateUpdate()
    {
        CheckInput();
    }
}
