
public class Sneakers : Equipment
{
    protected override void OnEnable()
    {
        MoveCharacter moveScript = transform.root.GetComponent<MoveCharacter>();
        moveScript.moveDistanse = moveScript.moveDistanse + value;
    }

    protected override void OnDisable()
    {
        MoveCharacter moveScript = transform.root.GetComponent<MoveCharacter>();
        moveScript.moveDistanse = moveScript.moveDistanse - value;
    }
}
