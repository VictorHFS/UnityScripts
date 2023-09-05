using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float Amount = 1;
    [SerializeField] private float Strenght = 1;
    [SerializeField] private float NoiseStrenght = 1;
    [SerializeField] private Transform Focus;
    private Camera camera;
    private float time = 0;
    private Vector3 position;
    public bool walking = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        position = camera.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time = Time.time * Strenght;
        time = Mathf.Sin(time);
        if (walking)
        {
            UpdateWalkingPosition();
        } else
        {
            UpdateStandingPosition();
        }

        camera.transform.LookAt(Focus);
    }

    private void UpdateWalkingPosition()
    {
        Vector3 side = camera.transform.TransformDirection((Vector3.left * time).normalized);
        Vector3 up = camera.transform.TransformDirection(Vector3.up);
        Vector3 target = (up + side) * Amount;
        target *= Mathf.PerlinNoise(Time.time, 1) * NoiseStrenght;
        camera.transform.position = Vector3.Lerp(position, position + target, Mathf.Abs(time));
    }

    private void UpdateStandingPosition()
    {
        Vector3 up = camera.transform.TransformDirection(Vector3.up) * Amount/6 ;
        Vector3 target = up;
        target *= Mathf.PerlinNoise(Time.time, 1) * NoiseStrenght;
        camera.transform.position = Vector3.Lerp(position, position + target, Mathf.Abs(time));
    }
}
