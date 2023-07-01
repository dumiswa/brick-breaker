using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using Physics;


class LineSegmentSolid : LineSegment
{
    ColliderManager colliderManager;
    LineSegmentCollider lineCollider;
    BallCollider startCap;
    BallCollider endCap;
    public LineSegmentSolid(Vec2 pStart, Vec2 pEnd) : base(pStart, pEnd)
    {
        colliderManager = ColliderManager.main;

        lineCollider = new LineSegmentCollider(this, pStart, pEnd);

        //Start and end point of the LineSegmentCollider
        startCap = new BallCollider(this, 0, pStart);
        endCap = new BallCollider(this, 0, pEnd);

        //Ads a solidCollider to the LineSegment
        colliderManager.AddSolidCollider(lineCollider);
        colliderManager.AddSolidCollider(startCap);
        colliderManager.AddSolidCollider(endCap);

    }

    protected override void OnDestroy()
    {
        //Removes the SolidCollider
        colliderManager.RemoveSolidCollider(lineCollider);     
    }
}
