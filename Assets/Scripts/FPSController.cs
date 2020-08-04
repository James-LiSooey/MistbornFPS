using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    bool m_canProcessInput = true;
    Vector3 m_characterVelocity = new Vector3(0, 0, 0);
    float m_maxSpeedOnGround = 10f;
    float m_speedModifier = 1f;
    float m_movementSharpnessOnGround = 3f;

    float m_rotationSpeed = 1f;
    float m_RotationMultiplier = 1f;

    float m_CameraVerticalAngle = 0;
    GameObject m_playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, (GetLookInputsHorizontal() * m_rotationSpeed * m_RotationMultiplier), 0f), Space.Self);

        // add vertical inputs to the camera's vertical angle
        m_CameraVerticalAngle += GetLookInputsVertical() * m_rotationSpeed * m_RotationMultiplier;

        // limit the camera's vertical angle to min/max
        m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);

        // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        m_playerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);


        Vector3 worldspaceMoveInput = transform.TransformVector(GetMoveInput());
        Vector3 targetVelocity = worldspaceMoveInput * m_maxSpeedOnGround * m_speedModifier;
        m_characterVelocity = Vector3.Lerp(m_characterVelocity, targetVelocity, m_movementSharpnessOnGround * Time.deltaTime);

        transform.position += m_characterVelocity * Time.deltaTime;
    }

    public float GetLookInputsHorizontal()
    {
        return Input.GetAxisRaw("Mouse X");
    }

    public float GetLookInputsVertical()
    {
        return -Input.GetAxisRaw("Mouse Y");
    }

    public Vector3 GetMoveInput()
    {
        if (m_canProcessInput)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            movementVector = Vector3.ClampMagnitude(movementVector, 1);

            return movementVector;
        }

        return Vector3.zero;
    }
}
