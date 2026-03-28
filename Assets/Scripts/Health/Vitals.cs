public class Vitals : MortalityMonitor
{
    private Mortal death;
    private bool isDead;


    public Vitals(Mortal death)
    {
        this.death = death;
    }
    

    public void Report(float currentHealth)
    {
        if (isDead) return;

        if (currentHealth <= 0)
        {
            isDead = true;
            
            death.Die();
        }
    }
}