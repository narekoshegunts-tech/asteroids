using UnityEngine;

namespace Game.Scripts.CustomPhysics
{
    public class SimulateCollisionService
    {
        // ЗСИ - m1v1 + m2v2 = m1u1 + m2u2'
        // ЗСЭ - m1v1^2 + m2v2^2 = m1u1^2 + m2u2^2
        
        // u1 = ((m1-m2)v1 + 2m2v2) / (m1 + m2)
        // u2 = ((m2-m1)v2 + 2m1v1) / (m1 + m2)

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