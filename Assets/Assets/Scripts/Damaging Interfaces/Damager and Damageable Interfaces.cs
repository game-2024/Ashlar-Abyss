using UnityEngine;


public interface IDamageable
{
    public void TakeDamage(int damage_taken);
}

public interface IDamager
{
    public void DealDamage(Collider collided_with);
}
