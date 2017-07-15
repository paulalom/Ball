using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    public Ball ball;
    public Rigidbody ballPhys;
    public Camera eyes;
    public float armLength = 0.6f;
    public float throwForce = 3f;
    bool hasBall = true;


    Vector3 mouseStartPostion;
    Vector3 startRotation;
    bool isRotating = false;
    public Vector2 RotationSensitivity = new Vector2(0.5f, 0.5f);
    public Vector2 TranslationSensitivity = new Vector2(1f, 1f);
    public float zoomSensitivity = 15f;

    // Use this for initialization
    void Start () {
	
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
    }

    void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ballPhys.velocity = Vector3.zero;
            ballPhys.angularVelocity = Vector3.zero;
            ballPhys.Sleep();
            
            ballPhys.AddForce(eyes.ScreenPointToRay(Input.mousePosition).direction * throwForce, ForceMode.Impulse);
            hasBall = !hasBall;
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
                Vector3 currentMousePosition = Input.mousePosition;
                transform.rotation = Quaternion.Euler(new Vector3(startRotation.x - (currentMousePosition.y - mouseStartPostion.y) * RotationSensitivity.y,
                                                                  startRotation.y + (currentMousePosition.x - mouseStartPostion.x) * RotationSensitivity.x,
                                                                  0));
            }
        }
        else {
            isRotating = false;
        }
        transform.Translate(new Vector3(
                                        Input.GetAxis("Horizontal"),
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
}
