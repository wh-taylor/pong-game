using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static float longitude = 0.0f;
    public static float altitude = 0.0f;

    public static float longitudeSpeed = -4.0f;
    float altitudeSpeed = 0.0f;

    const float speedIncrement = 0.005f;
    const float altitudeSpeedClamp = 16.0f;

    // Start is called before the first frame update
    void Start()
    {
        altitudeSpeed = Random.Range(-1.5f,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        longitude += longitudeSpeed * Time.deltaTime;
        altitude += altitudeSpeed * Time.deltaTime;

        altitude = Mathf.Clamp(altitude, -4.75f, 4.75f);

        if (Mathf.Abs(altitude) == 4.75f)
            altitudeSpeed = -altitudeSpeed;

        transform.position = new Vector3(longitude, altitude, 0.0f);
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(trigger.transform.gameObject.transform.position.x)) return;
        if (!(trigger.name == "Left Paddle" | trigger.name == "Right Paddle")) return;
        
        longitudeSpeed = -longitudeSpeed;

        if (longitudeSpeed >= 0.0f)
            longitudeSpeed += speedIncrement;
        else
            longitudeSpeed -= speedIncrement;

        altitudeSpeed += 3 * (transform.position.y - trigger.transform.gameObject.transform.position.y);
        
        altitudeSpeed = Mathf.Clamp(altitudeSpeed, -altitudeSpeedClamp, altitudeSpeedClamp);
    }
}