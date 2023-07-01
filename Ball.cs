using System;
using System.Drawing;
using GXPEngine;
using Physics;

public class Ball : EasyDraw
{
    public static bool drawDebugLine = false;
    public static bool wordy = false;
    

    public Vec2 velocity;
    public Vec2 position;

    public readonly int radius;
    public readonly bool moving;

    ColliderManager colliderManager;
    BallCollider ballCollider;
    
   

    public Ball(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        

        colliderManager = ColliderManager.main;
        ballCollider = new BallCollider(this, radius, pPosition, moving);
        colliderManager.AddSolidCollider(ballCollider);

        Update();
        SetOrigin(radius, radius);
        Draw(255, 255, 255);
    }

    //Draws the ball
    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    //Calls the ResolveCollision method
    void ResolveCollision(CollisionInfo col)
    {
        //If it collides with the SquareBrick it calls the GetsHit method
        if (col.other.owner is SquareObstacle squareBrick)
        {
            squareBrick.GetsHit();
        }

        if (col.other.owner is TriangleObstacle triangleObstacle)
        {
            triangleObstacle.GetsHit();
        }

        //Reflects the velocity
        velocity.Reflect(col.normal, BallCollider.bounciness);

        Console.WriteLine(col.other + "Collided");
    }

     void Update()
    {
        //Updates the CollisionInfo
        CollisionInfo collision = colliderManager.MoveUntilCollision(ballCollider, velocity);
        if (collision != null)
        {
            //If there is a collision it resolves it
            ResolveCollision(collision);
        }

        //Updates the ball's position alongside with it's collider
        position = ballCollider.position;
        x = position.x;
        y = position.y;

    }

 
}
