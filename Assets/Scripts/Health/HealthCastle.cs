public class HealthCastle : HealthBase
{
    public static HealthCastle Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public override void Die()
    {
        //Game Over
    }
}