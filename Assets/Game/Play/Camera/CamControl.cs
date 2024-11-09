using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Coroutine myCoroutine;
    private bool holdButton;
    [Space]
    [SerializeField] private byte minDist;
    [SerializeField] private byte maxDist;
    [Space]
    [SerializeField] private float moveSpeed;
    [Space]
    [SerializeField] private float rotationDuration;
    private float timeElapsed;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 angle = new Vector3(0, 90, 0);


    private void MoveCam(InputAction.CallbackContext context)
    {
        holdButton = true;
        StartCoroutine(HoldCam());
    }

    private void StopCam(InputAction.CallbackContext context)
    {
        holdButton = false;
    }

    private IEnumerator HoldCam()
    {
        Vector3 lastPosition = Input.mousePosition;
        while (holdButton)
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            lastPosition = Input.mousePosition;

            float moveX = delta.x * moveSpeed;
            float moveY = delta.y * moveSpeed;

            Vector3 moveDirection = transform.right * (moveX - moveY) + transform.forward * (moveY + moveX);
            
            //newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); //заготовка под ограничения дальности движения
            //newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            transform.position -= moveDirection;

            yield return null;
        }
    }


    private void CamZoom(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = GameKeyboard.gameInput.Player.Scroll.ReadValue<Vector2>();
        Vector3 newPos = cam.transform.position + cam.transform.forward * scrollValue.y/60;

        float distance = Vector3.Distance(newPos, transform.position);

        if (minDist <= distance && distance <= maxDist)
        {
            cam.transform.position = newPos;
        }
    }


    private void RotateCamera(int rotatePos)
    {
        if (rotatePos == 1)
        {
            if (myCoroutine == null)
            {
                myCoroutine = StartCoroutine(Rotate(-angle));
            }
            else
            {
                startRotation = transform.rotation;
                endRotation = endRotation * Quaternion.Euler(-angle);
                timeElapsed = 0f;
            }
        }
        else
        {
            if (myCoroutine == null)
            {
                myCoroutine = StartCoroutine(Rotate(angle));
            }
            else
            {
                startRotation = transform.rotation;
                endRotation = endRotation * Quaternion.Euler(angle);
                timeElapsed = 0f;
            }
        }
    }

    private IEnumerator Rotate(Vector3 targetAngle)
    {
        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(targetAngle);
   
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.rotation = endRotation;
        myCoroutine = null;
    }


    void Start() //тут должен был быть OnEnable, но почему то нулевой референс возникает тут и у Pause
    {
        GameKeyboard.gameInput.Player.Mouse_1.started += MoveCam;
        GameKeyboard.gameInput.Player.Mouse_1.canceled += StopCam;

        GameKeyboard.gameInput.Player.Scroll.started += CamZoom;

        GameKeyboard.gameInput.Player.Q.started += context => RotateCamera(0);
        GameKeyboard.gameInput.Player.E.started += context => RotateCamera(1);
    }

    void OnDestroy()
    {
        GameKeyboard.gameInput.Player.Mouse_1.started -= MoveCam;
        GameKeyboard.gameInput.Player.Mouse_1.canceled -= StopCam;

        GameKeyboard.gameInput.Player.Scroll.started -= CamZoom;

        GameKeyboard.gameInput.Player.Q.started -= context => RotateCamera(0);
        GameKeyboard.gameInput.Player.E.started -= context => RotateCamera(1);
    }
}
