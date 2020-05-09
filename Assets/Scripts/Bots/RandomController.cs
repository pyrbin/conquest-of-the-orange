using UnityEngine;

public class RandomController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }


    // Start is called before the first frame update
    private void Awake()
    {
        TargetPos = getNewPosition();
        FruitEntity = GetComponentInChildren<FruitEntity>();
        LevelManager levelManager = Game.Find().LevelManager;
    }

    // Update is called once per frame
    private void Update()
    {
        if ((FruitEntity.transform.position - TargetPos).magnitude < 2)
            TargetPos = getNewPosition();
        FruitEntity.Movement.MoveTo(TargetPos);
    }


    private Vector2 getNewPosition() { 

        Vector2 origin = GetComponentInChildren<Rigidbody2D>().position;
        Vector2 randomDirection = new Vector3(Random.Range(-5f, 5f), Random.Range(5f, -5f));

        RaycastHit2D hit = Physics2D.Raycast(origin, randomDirection, randomDirection.magnitude);

        if (hit) {
            print(hit.collider.transform.position);
            return hit.collider.transform.position;
        }
        return randomDirection + origin;
    }

}
