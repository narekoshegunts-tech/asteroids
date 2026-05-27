using UnityEngine;

namespace Game.Scripts.CustomPhysics
{
    public class SimulateCollisionService
    {
        public void Simulate(CustomPhysicsFacade2D facade1, CustomPhysicsFacade2D facade2, 
            out Vector2 velocityAfterCollision1, out Vector2 velocityAfterCollision2)
        {
            float mass1 = facade1.Mass;
            float mass2 = facade2.Mass;

            Vector2 velocity1 = facade1.GetVelocity();
            Vector2 velocity2 = facade2.GetVelocity();
            
            velocityAfterCollision1 = ((mass1 - mass2) * velocity1 + 2 * mass2 * velocity2) / (mass1 + mass2);
            velocityAfterCollision2 = ((mass2 - mass1) * velocity2 + 2 * mass1 * velocity1) / (mass1 + mass2);
        }
    }
}