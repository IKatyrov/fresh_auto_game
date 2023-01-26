using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleGenerator generator;
    private Transform direction;
    private Rigidbody2D rb;
    public ObstacleType type;

    public static bool IsWorking = false;

    private void Start()
    {
        generator = ObstacleGenerator.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Transform t)
    {
        direction = t;
    }


    private void FixedUpdate()
    {
        if (!IsWorking)
        {
            return;
        }

        if(direction != null)
        {
            var step = generator.Speed * Time.deltaTime * -direction.up;
            rb.MovePosition(transform.position + step);
        }
        if(transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }

}

public enum ObstacleType
{
    Barrier, Hole, Coin
}
