using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Ball ball;
    public Rigidbody ballPhys;
    public Camera eyes;
    public float armLength = 0.6f;
    [HideInInspector]
    public float throwForce = 0f;
    [HideInInspector]
    public float maxThrowForce = 10f;
    public bool hasBall;
    bool throwing = false;
    public Text debugText;
    public GameObject parent;
    public GameObject modelPrefab;

    public int playerId;

    Vector3 mouseStartPostion;
    Vector3 startRotation;
    bool isRotating = false;
    Vector2 RotationSensitivity = new Vector2(0.25f, 0.25f);
    Vector2 TranslationSensitivity = new Vector2(.05f, .05f);
    float zoomSensitivity = 1.5f;

    // Use this for initialization
    void Start () {
        if (modelPrefab != null)
        {
            GameObject.Instantiate(modelPrefab, gameObject.transform);
        }
        throwForce = 0;
        
        //debugText.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {
        CheckInput();

        if (hasBall)
        {
            HoldBall();
        }
        else
        {
            CheckIfBallInReach();
        }
        
	}

    void CheckIfBallInReach()
    {
        
    }

    void HoldBall()
    {
        Ray ray = eyes.ScreenPointToRay(Input.mousePosition);
        ball.transform.position = eyes.transform.position + (ray.direction * armLength);
        ballPhys.velocity = Vector3.zero;
        ballPhys.angularVelocity = Vector3.zero;
    }

    void CheckInput()
    {
        //CheckDeviceInput();
        //CheckDesktopInput();
    }
    

    Vector3 Clamp(Vector3 vector, float min)
    {
        Vector3 outVec = new Vector3(
        (vector.x < min && vector.x > -min) ? 0 : vector.x,
        (vector.y < min && vector.y > -min) ? 0 : vector.y,
        (vector.z < min && vector.z > -min) ? 0 : vector.z);
        return outVec;
    }

    void CheckDesktopInput()
    {
        if (hasBall && Input.GetKey(KeyCode.Mouse0))
        {
            throwForce = (throwForce == maxThrowForce) ? throwForce : throwForce + .1f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ballPhys.AddForce(eyes.ScreenPointToRay(Input.mousePosition).direction * throwForce, ForceMode.Impulse);
            hasBall = !hasBall;
            throwForce = 0;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            RotatePlayerLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            RotatePlayerRight();
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (!isRotating)
            {
                isRotating = true;
                mouseStartPostion = Input.mousePosition;
                startRotation = transform.rotation.eulerAngles;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Vector3 currentMousePosition = Input.mousePosition;
                transform.rotation = Quaternion.Euler(new Vector3(startRotation.x - (currentMousePosition.y - mouseStartPostion.y) * RotationSensitivity.y,
                                                                  startRotation.y + (currentMousePosition.x - mouseStartPostion.x) * RotationSensitivity.x,
                                                                  0));
            }
        }
        else
        {
            isRotating = false;
        }
        transform.Translate(new Vector3(
                                        TranslationSensitivity.x
                                            * Input.GetAxis("Horizontal"),
                                        TranslationSensitivity.x
                                            * Input.GetAxis("Vertical")
                                            * Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.PI / 180),
                                        TranslationSensitivity.y
                                            * Input.GetAxis("Vertical")
                                            * Mathf.Cos(transform.rotation.eulerAngles.x * Mathf.PI / 180)
                                            + Input.GetAxis("Mouse ScrollWheel")
                                            * zoomSensitivity),
                            Space.Self);
    }

    string VectorToString(Vector3 vec)
    {
        return vec.x + ", " + vec.y + ", " + vec.z;
    }


    void RotatePlayerLeft()
    {
        transform.Rotate(new Vector3(0, -1f, 0));
    }

    void RotatePlayerRight()
    {
        transform.Rotate(new Vector3(0, 1f, 0));
    }
}
