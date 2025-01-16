using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class S_BehaviorsFunction
{
    /// <summary>
    /// Arching behaviour is used when a projectile trajectory is a upward curve
    /// </summary>
    /// <param name="floatVariables">
    /// index[0] must be velocity
    /// index[1] must be the arching Force
    /// index[2] must be the AOE radius
    /// 
    /// </param>
    /// <param name="vectorVariables">
    /// index[0] must be the targetPos
    /// </param>
    /// <returns>
    /// Returns a struct ready to be used for the behaviour of the projectile
    /// </returns>
    public static ProjectileBehaviour ArchingBehaviour(List<float>floatVariables,List<Vector2> vectorVariables)
    {
        ProjectileBehaviour behaviour = new ProjectileBehaviour();
        behaviour.constFloatVariables=floatVariables;
        behaviour.constVectorVariables=vectorVariables;
        
        behaviour.TrajectoryFunction=(originalPos, hit, timer) => MathMethods.Bezier(originalPos, (originalPos + hit) / 2 + new Vector2(0, floatVariables[0]), hit, timer);
        behaviour.VerifyEndTrajectory=(pos, timer) =>
        {
            if (timer > 0.98f)
            {
                return true;
            }

            return false;
        };
        behaviour.EnemiesDetectionFunction=()=>
        {

            
            return Physics2D.OverlapCircleAll(vectorVariables[0], floatVariables[1], (1 << 8)).ToList();
        };
        
        return behaviour;
        
    }

    /// <summary>
    /// Linear behaviour is used when a projectile goes in a straight line to his target
    /// </summary>
    /// <param name="floatVariables">
    /// index[0] must be the projectile velocity
    /// 
    /// 
    /// </param>
    /// <param name="vectorVariables">
    /// index[0] must be the targetPos
    /// index[1] must be the originPos
    /// </param>
    /// <returns>
    /// Returns a struct ready to be used for the behaviour of the projectile
    /// </returns>
    public static ProjectileBehaviour LinearBehaviour(List<float> floatVariables, List<Vector2> vectorVariables)
    {
        ProjectileBehaviour behaviour = new ProjectileBehaviour();
        behaviour.constFloatVariables=floatVariables;
        behaviour.constVectorVariables=vectorVariables;
        behaviour.TrajectoryFunction=(originalPos, pos, speed) => pos + ((vectorVariables[0] - vectorVariables[1]).normalized * (Time.deltaTime * floatVariables[0]));
        behaviour.VerifyEndTrajectory = (pos, timer) =>
        {
            if ((pos - vectorVariables[1]).magnitude >= (vectorVariables[0] - vectorVariables[1]).magnitude)
            {
                return true;
            }

            return false;
        };
        behaviour.EnemiesDetectionFunction=()=>
        {
            List<Collider2D> enemy = new();
            enemy.Add(Physics2D.OverlapCircle(vectorVariables[1], 0.01f, (1 << 8)));
            return enemy;
        };
        return behaviour;
    }

    /// <summary>
    /// index[0] is Linear
    /// index[1] is Arching
    /// </summary>
    public static Func<List<float>, List<Vector2>, ProjectileBehaviour>[] projectileFunctions =
    {
        (Func<List<float>, List<Vector2>, ProjectileBehaviour>)LinearBehaviour,
        (Func<List<float>, List<Vector2>, ProjectileBehaviour>)ArchingBehaviour
    };
    

}

public struct ProjectileBehaviour
{
    /// <summary>
    /// index[0] must be velocity
    /// index[1] must be the arching Force for Arching
    /// index[2] must be the AOE radius for Arching
    /// </summary>
    public List<float> constFloatVariables;
    
    /// <summary>
    /// index[0] must be the targetPos
    /// index[1] must be the originPos for Linear
    /// </summary>
    public List<Vector2> constVectorVariables;
    
    /// <summary>
    /// in general arg1 is originPos, args2 is targetPos and args3 is timer.
    /// </summary>
    public Func<Vector2,Vector2,float,Vector2> TrajectoryFunction;
    
    /// <summary>
    /// in general arg1 is pos, args2 is timer.
    /// </summary>
    public Func<Vector2,float,bool> VerifyEndTrajectory;
    
    public Func<List<Collider2D>> EnemiesDetectionFunction;

}
