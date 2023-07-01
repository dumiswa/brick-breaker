using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using Physics;


class SquareObstacle : LineSegment
{
    ColliderManager colliderManager;
    LineSegmentCollider line1;
    LineSegmentCollider line2;
    LineSegmentCollider line3;
    LineSegmentCollider line4;
    BallCollider ball1;
    BallCollider ball2;
    BallCollider ball3;
    BallCollider ball4;

    public SquareObstacle (Vec2 first, Vec2 second, Vec2 third, Vec2 fourth) : base(first, second)
    {
        colliderManager = ColliderManager.main;
        line1 = new LineSegmentCollider(this, first, second);
        line2 = new LineSegmentCollider(this, second, third);
        line3 = new LineSegmentCollider(this, third, fourth);
        line4 = new LineSegmentCollider(this, fourth, first);

        ball1 = new BallCollider(this, 0, first);
        ball2 = new BallCollider(this, 0, second);
        ball3 = new BallCollider(this, 0, third);
        ball4 = new BallCollider(this, 0, fourth);

        //Ads a solider collider to the LineSegments
        colliderManager.AddSolidCollider(line1);
        colliderManager.AddSolidCollider(line2);
        colliderManager.AddSolidCollider(line3);
        colliderManager.AddSolidCollider(line4);

        //Adds a solid collider to the BallColliders
        colliderManager.AddSolidCollider(ball1);
        colliderManager.AddSolidCollider(ball2);
        colliderManager.AddSolidCollider(ball3);
        colliderManager.AddSolidCollider(ball4);
    }

    protected override void OnDestroy()
    {
        // Remove the collider 
        colliderManager.RemoveSolidCollider(line1);
        colliderManager.RemoveSolidCollider(line2);
        colliderManager.RemoveSolidCollider(line3);
        colliderManager.RemoveSolidCollider(line4);

        colliderManager.RemoveSolidCollider(ball1);
        colliderManager.RemoveSolidCollider(ball2);
        colliderManager.RemoveSolidCollider(ball3);
        colliderManager.RemoveSolidCollider(ball4);
    }
    
    public void GetsHit()
    {
        //If the GetsHit method is called, it destroys the obstacle
        LateDestroy();
    }

    protected override void RenderSelf(GLContext glContext)
    {
        if (game != null)
        {
            //Renders a line between the specified points 
            Gizmos.RenderLine(line1.start.x, line1.start.y, line2.start.x, line2.start.y, color, lineWidth);
            Gizmos.RenderLine(line2.start.x, line2.start.y, line3.start.x, line3.start.y, color, lineWidth);
            Gizmos.RenderLine(line3.start.x, line3.start.y, line4.start.x, line4.start.y, color, lineWidth);
            Gizmos.RenderLine(line4.start.x, line4.start.y, line1.start.x, line1.start.y, color, lineWidth);
        }
    }
}
