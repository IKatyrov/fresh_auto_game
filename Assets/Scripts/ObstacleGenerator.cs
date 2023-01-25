using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private Transform[] obstacleGenPoints;
    [SerializeField] private Transform[] lerpPositions;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private float time;

    public float Speed;
    public static ObstacleGenerator Instance;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator.speed = Speed;
        StartCoroutine(Gen());
        StartCoroutine(PlusSpeed());
    }

    public void Stop()
    {
        StopCoroutine(Gen());
        Speed = 0;
        _animator.SetBool("can", false);
    }

    private IEnumerator Gen()
    {
        GenerateObstacles();
        yield return new WaitForSeconds(time);
        StartCoroutine(Gen());
    }

    private IEnumerator PlusSpeed()
    {
        Speed++;
        _animator.speed = Speed;
        yield return new WaitForSeconds(20f);
        StartCoroutine(PlusSpeed());
    }


    private void GenerateObstacles()
    {

        var count = Random.Range(0, obstacleGenPoints.Length);


        var indexes = new List<int>();

        for (var i = 0; i < count;)
        {
            var index = Random.Range(0, obstacleGenPoints.Length);
            var a = false;
            foreach (var j in indexes)
            {
                if (j == index)
                {
                    a = true;
                }
            }

            if (a)
            {
                continue;
            }

            indexes.Add(index);

            var y = Random.Range(0, obstaclesPrefabs.Length);


            var prefab = obstaclesPrefabs[y];
            var pos = obstacleGenPoints[index].position;

            var o = Instantiate(prefab,
                pos, Quaternion.identity);

            o.GetComponent<Obstacle>().SetDirection(obstacleGenPoints[index]);

            i++;




        }

    }


}
