using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Vector3 zeroThrustLocalPos = new(0, 150, -400);
    public Vector3 fullThrustLocalPos = new(0, 150, -600);
    public float smoothing = 5f;

	
	internal void FixedUpdate () {
        float thrustInput = Input.GetAxis("Thrust");
        transform.localPosition = Vector3.Lerp(zeroThrustLocalPos, fullThrustLocalPos, thrustInput);
	}
}
