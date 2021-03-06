using UnityEngine;

public class FallingTimerElement : FallingElementBase
{
    protected override Sound correctKeySound { get { return GameManagerScript.instance.normalLetterCorrectKeySound; } }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void CorrectKey()
    {
        if (correctKeyStarted) return;
        GameManagerScript.instance.ChangeSpeed(GameManagerScript.instance.timerFallSpeed);
        base.CorrectKey();
    }

    protected override void Collide()
    {
        base.Collide();
    }

    protected override void TouchedByBombBlast()
    {
        CorrectKey();
    }

    public override void OnFXAnimationEnd()
    {
        GameManagerScript.instance.ChangeSpeed(GameManagerScript.instance.normalFallspeed);
        base.OnFXAnimationEnd();
    }
}
