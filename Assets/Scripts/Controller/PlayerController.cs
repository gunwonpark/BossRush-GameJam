using System;
using UnityEngine;

// Rotate around the Boss
// Shoot bullets
// ... and more
public class PlayerController : BaseController
{
    private Transform target;
    [SerializeField]
    private float distance = 10f; // Distance from target
    [SerializeField]
    private float currentAngle = 0f; // Current angle around target
    [SerializeField]
    private float angleSpeed = 1f; // Speed of rotation

    private Action onPlayerMove;
    public float CurrentAngle
    {
        get => currentAngle;
        set
        {
            currentAngle = value;
            onPlayerMove?.Invoke();
        }
    }

    public override void Init()
    {
        onPlayerMove += RotateAroundTarget;
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            CurrentAngle += 1f * angleSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            CurrentAngle -= 1f * angleSpeed * Time.deltaTime;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        RotateAroundTarget();
    }

    private void RotateAroundTarget()
    {
        float nextX = target.position.x + Mathf.Cos(currentAngle) * distance;
        float nextZ = target.position.z + Mathf.Sin(currentAngle) * distance;

        Vector3 nextPosition = new Vector3(nextX, 0, nextZ);
        this.transform.position = nextPosition;
    }
}
