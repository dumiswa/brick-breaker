using GXPEngine.Core;
using GXPEngine;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TriangleObstacle : LineSegment
{
    ColliderManager colliderManager;
    LineSegmentCollider line1;
    LineSegmentCollider line2;
    LineSegmentCollider line3;

    BallCollider ball1;
    BallCollider ball2;
    BallCollider ball3;

    int hits = 0;
    public TriangleObstacle(Vec2 first, Vec2 second, Vec2 third ) : base(first, second)
    {
        colliderManager = ColliderManager.main;
        line1 = new LineSegmentCollider(this, first, second);
        line2 = new LineSegmentCollider(this, second, third);
        line3 = new LineSegmentCollider (this, third, first);
  

        ball1 = new BallCollider(this, 0, first);
        ball2 = new BallCollider(this, 0, second);
        ball3 = new BallCollider(this, 0, third);

        //Adds a solidCollider to the lineSegment
        colliderManager.AddSolidCollider(line1);
        colliderManager.AddSolidCollider(line2);
        colliderManager.AddSolidCollider(line3);

        //Adds a solidCollider to the BallColliders
        colliderManager.AddSolidCollider(ball1);
        colliderManager.AddSolidCollider(ball2);
        colliderManager.AddSolidCollider(ball3);
    }

    protected override void OnDestroy()
    {
        // Remove the SolidCollider
        colliderManager.RemoveSolidCollider(line1);
        colliderManager.RemoveSolidCollider(line2);
        colliderManager.RemoveSolidCollider(line3);

        colliderManager.RemoveSolidCollider(ball1);
        colliderManager.RemoveSolidCollider(ball2);
        colliderManager.RemoveSolidCollider(ball3);
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
            Gizmos.RenderLine(line3.start.x, line3.start.y, line1.start.x, line1.start.y, color, lineWidth);
        }
    }
}
