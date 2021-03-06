using UnityEngine;
[CreateAssetMenu(menuName = "Conditions/Star/OneStarCondition", fileName = "OneStarCondition")]
public class OneStarCondition : StarBaseCondition
{
    public override float pointsCoefficientForStar { get; protected set; } = 1;

    public override int starsWhenTrue 
    {
        get { return 1; }
        protected set { }
    }
}
