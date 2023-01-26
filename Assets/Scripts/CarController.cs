using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float Speed { get => _speed; }
    [SerializeField] private float _speed;

    [SerializeField] private Transform[] turnPoints;
    
    [SerializeField] private int _turnIndex;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeTurnIndex(Vector2 koef)
    {

        var k = 0;
        if (koef.x > 0) k = 1;
        else if (koef.x < 0) k = -1;

        if(!(_turnIndex + k >= turnPoints.Length || _turnIndex + k < 0))
        {
            _turnIndex += k;
            transform.position = turnPoints[_turnIndex].position;
        }
    }

    private void ChangeTurnIndex(float koef)
    {

        var k = 0;
        if (koef > 0) k = 1;
        else if (koef < 0) k = -1;

        if (!(_turnIndex + k >= turnPoints.Length || _turnIndex + k < 0))
        {
            _turnIndex += k;
            transform.position = turnPoints[_turnIndex].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Obstacle>(out var o))
        {
            if(o.type == ObstacleType.Barrier || o.type == ObstacleType.Hole) 
            { 
                _animator.SetBool(o.type.ToString(), true);
                ObstacleGenerator.Instance.Stop();
                LeaderBoard.Instance.Show();
            }
            if(o.type == ObstacleType.Coin)
            {
                MoneyController.Instance.AddCoin();
                Destroy(collision.gameObject);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeTurnIndex(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeTurnIndex(1);
        }
    }

}
