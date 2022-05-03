public class ThreeStarCondition : StarBaseCondition
{
    public override float pointsCoefficientForStar { get; protected set; } = 2;
    public override int starsWhenTrue
    {
        get { return 3; }
        protected set { }
    }
}
