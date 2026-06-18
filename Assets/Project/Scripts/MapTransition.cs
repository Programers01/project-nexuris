using UnityEngine;
using Unity.Cinemachine;
public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] int tp = 4;

    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += tp;
                break;
            case Direction.Down:
                newPos.y -= tp;
                break;
            case Direction.Left:
                newPos.x += tp;
                break;
            case Direction.Right:
                newPos.x -= tp;
                break;
        }

        player.transform.position = newPos;
    }
}
