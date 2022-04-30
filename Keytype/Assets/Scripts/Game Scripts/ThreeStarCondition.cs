public class ThreeStarCondition : StarBaseCondition
{
    public override int starsWhenTrue
    {
        get { return 3; }
        protected set { }
    }
    public override bool CheckCondition(object dataParam)
    {
        base.CheckCondition(dataParam);
        if (gameManager.lives == 3)
        {
            return true;
        }
        return false;
    }
}