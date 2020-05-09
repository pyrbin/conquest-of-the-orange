using UnityEngine;

public class RandomController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    public float xMin = 0f;
    public float xMax = 10f;
    public float yMin = 0f;
    public float yMax = 10f;

    // Start is called before the first frame update
    private void Awake()
    {
        TargetPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((FruitEntity.transform.position - TargetPos).magnitude < 1)
            TargetPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        FruitEntity.Movement.MoveTo(TargetPos);
    }


}
