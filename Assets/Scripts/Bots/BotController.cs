using UnityEngine;

public class BotController : MonoBehaviour
{
    public FruitEntity FruitEntity { get; private set; }

    [HideInInspector]
    public Vector3 TargetPos { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        FruitEntity = GetComponentInChildren<FruitEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        FruitEntity.Movement.MoveTo(TargetPos);
    }


}
