using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using Physics;


class Paddle : LineSegment
{
    ColliderManager colliderManager;
    LineSegmentCollider lineCollider;
    BallCollider startCap;
    BallCollider endCap;


    float paddleHalfWidth;
    float minX;
    float maxX;
   
  
    public Paddle(Vec2 pStart, Vec2 pEnd, Ball ball) : base(pStart, pEnd)
    {
        colliderManager = ColliderManager.main;
        lineCollider = new LineSegmentCollider(this, pStart , pEnd);
        startCap = new BallCollider(this, 0, pStart);
        endCap = new BallCollider(this, 0, pEnd);

        colliderManager.AddSolidCollider(lineCollider);
        colliderManager.AddSolidCollider(startCap);
        colliderManager.AddSolidCollider(endCap);

        //Get the half of the paddle and its minimum and maximum X
        paddleHalfWidth = (end.x - start.x) / 2f;
        minX = paddleHalfWidth;
        maxX = game.width - paddleHalfWidth;      
    }

    void UpdateScreenPosition()
    {
        //Clamps the paddle movement on the X
        Vec2 center = new Vec2(Mathf.Clamp(Input.mouseX, minX, maxX), 650);
        float halfWidth = (end.x - start.x) / 2f;

        // Update the line collider's start and end positions
        lineCollider.start = new Vec2(center.x + halfWidth, start.y);
        lineCollider.end = new Vec2(center.x - halfWidth, end.y);

        // Update the start and end cap colliders' positions
        startCap.position = new Vec2(center.x + halfWidth, start.y);
        endCap.position = new Vec2(center.x - halfWidth, end.y);

        // Update the visual representation of the Paddle
        start.x = center.x - halfWidth;
        end.x = center.x + halfWidth;
    }


    void Update()
    {
        UpdateScreenPosition();
      
    } 

    protected override void OnDestroy()
    {
        //Removes the SolidCollider
        colliderManager.RemoveSolidCollider(lineCollider);
    }
}
