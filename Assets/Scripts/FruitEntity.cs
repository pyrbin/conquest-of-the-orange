using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FruitMovement))]
[RequireComponent(typeof(RotateWithVelocity))]
[RequireComponent(typeof(GrowWithVelocity))]
[RequireComponent(typeof(Painter))]
public class FruitEntity : MonoBehaviour
{
    public FruitMovement Movement { get; private set; }
    public RotateWithVelocity Rotator { get; private set; }
    public GrowWithVelocity Grower { get; private set; }
    public Painter Painter { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Movement = GetComponent<FruitMovement>();
        Rotator = GetComponent<RotateWithVelocity>();
        Grower = GetComponent<GrowWithVelocity>();
        Painter = GetComponent<Painter>();
    }

    // Update is called once per frame
    private void Update()
    {
        Painter.RadiusMult = Grower.Target.transform.localScale.x;
    }
}
