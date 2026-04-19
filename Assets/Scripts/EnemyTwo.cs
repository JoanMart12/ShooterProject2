using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public float speed = 3f;
    public float moveSideSpeed = 2f;

    private float direction = 1f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        transform.Translate(Vector3.right * direction * moveSideSpeed * Time.deltaTime);

        if (transform.position.x > 9f)
            direction = -1f;

        if (transform.position.x < -9f)
            direction = 1f;
    }
}