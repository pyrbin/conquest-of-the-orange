using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FruitMovement))]
[RequireComponent(typeof(RotateWithVelocity))]
[RequireComponent(typeof(GrowWithVelocity))]
[RequireComponent(typeof(Painter))]
public class FruitEntity : MonoBehaviour
{
    public float MaxFlagRotation = 65;
    public FruitMovement Movement { get; private set; }
    public RotateWithVelocity Rotator { get; private set; }
    public GrowWithVelocity Grower { get; private set; }
    public Painter Painter { get; private set; }
    public Squishes Squishes { get; private set; }

    public SpriteRenderer Flag { get; private set; }
    public SpriteRenderer Model { get; private set; }

    public Color Color => Painter.PaintingData.Color;

    private float flagStartRot = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        Movement = GetComponent<FruitMovement>();
        Rotator = GetComponent<RotateWithVelocity>();
        Grower = GetComponent<GrowWithVelocity>();
        Painter = GetComponent<Painter>();
        Squishes = GetComponent<Squishes>();

        Flag = transform.Find("Body/Flag").GetComponent<SpriteRenderer>();
        Model = transform.Find("Body/Sprite").GetComponent<SpriteRenderer>();

        Flag.color = Color;
        flagStartRot = Flag.transform.rotation.z;
    }

    // Update is called once per frame
    private void Update()
    {
        Painter.RadiusMult = Grower.Target.transform.localScale.x;

        if (Movement.rigid.velocity.x > 0)
        {
            Flag.flipX = true;
            Flag.transform.rotation = Quaternion.Euler(0, 0, flagStartRot + (Movement.MaxSpeedPerc * MaxFlagRotation));
        }
        else
        {
            Flag.flipX = false;

            Flag.transform.rotation = Quaternion.Euler(0, 0, flagStartRot - (Movement.MaxSpeedPerc * MaxFlagRotation));
        }
    }

    public void DisplaySquishIndicator(bool dangerous)
    {
        transform.Find("Body/Indicator").gameObject.SetActive(true);
        transform.Find("Body/Indicator").GetComponent<SpriteRenderer>().color = dangerous ? Color.red : Color.green;
    }

    public void HideSquishIndicator()
    {
        transform.Find("Body/Indicator").gameObject.SetActive(false);
    }

    public void OnSquish()
    {
        Destroy(transform.parent.gameObject);
        Game.Find().LevelManager.MainCamera.ShakeCamera(0.55f, 0.8f);
        AudioManager.Find().PlayOneShotSound(AudioManager.SoundType.SQUISH);
    }
}
