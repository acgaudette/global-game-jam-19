using UnityEngine;

public class CameraControl: MonoBehaviour {
    public Gameplay.Game game;

    public float damping = .1f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public Vector3 shakeAmount;
    public float shakeTime;
    public float offsetFactor;

    [Header("Read-only")]

    public bool shaking = false;
    public Vector3 shake;
    public float shakeTimer;
    public Vector3 lastShake;
    public Vector3 lastPosition;
    public Vector3 actualAmount;

    void LateUpdate() {
        Vector3 com = Vector3.zero;

        foreach (var tenant in game.tenants) {
            com += tenant.transform.position;
        }

        var count = game.tenants.Count;

        if (count > 0) {
            com /= game.tenants.Count;
        }

        com.x = Mathf.Clamp(com.x, minX, maxX);
        com.y = Mathf.Clamp(com.y, minY, maxY);

        // For inverse orbit
        //com = -com;

        com.z = -10;

        transform.position = Vector3.Lerp(
            lastPosition,
            com,
            Time.deltaTime * damping
        ) + shake;

        var tmpLast = lastPosition;
        lastPosition = transform.position - shake;

        // Reset z
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            -10
        );

        /*
        var targetPosition = Vector3.Lerp(
            tmpLast,
            -com,
            Time.deltaTime * damping
        );
        */

        //var offset = targetPosition * offsetFactor;
        var offset = transform.position * offsetFactor;
        offset.z = 0;

        transform.LookAt(offset);

        if (game.kicked) {
            shaking = true;
            shakeTimer = shakeTime;
            actualAmount = shakeAmount;
        }

        if (!shaking) {
            shake = Vector3.zero;
        } else {
            float dist = 0;

            for (int i = 0; i < 8; ++i) {
                Vector3 target = new Vector3(
                    Random.Range(-actualAmount.x, actualAmount.x),
                    Random.Range(-actualAmount.y, actualAmount.y),
                    Random.Range(-actualAmount.z, actualAmount.z)
                );

                float compare = Vector3.Distance(target, lastShake);

                if (compare > dist) {
                    dist = compare;
                    shake = target;
                }
            }

            lastShake = shake;
            shakeTimer -= Time.deltaTime;
            actualAmount = shakeAmount * (shakeTimer / shakeTime);

            if (shakeTimer < 0) {
                shaking = false;
            }
        }
    }
}
