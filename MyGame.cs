using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using Physics;
using TiledMapParser;

public class MyGame : Game
{
	Ball ball;
	Paddle paddle;
    AimToShoot aimToShoot;
    Canvas loseTextCanvas;



    public MyGame() : base(600, 750, false, false)
	{
		CreateScene();       
	}


	void CreateScene()
	{
        //Add screen margins 
        AddChild(new LineSegmentSolid(new Vec2(width + 25, height - 2), new Vec2(-252, height - 2))); //bottom
        AddChild(new LineSegmentSolid(new Vec2(-252, 2), new Vec2(width + 252, 2))); //top
        AddChild(new LineSegmentSolid(new Vec2(2, height + 30), new Vec2(2, -30))); //left
        AddChild(new LineSegmentSolid(new Vec2(width -2, -30), new Vec2(width -2, height + 30))); //right

        //Add obstacles
        AddChild(new SquareObstacle(new Vec2(100, 100), new Vec2(100, 150), new Vec2(150, 150), new Vec2(150, 100)));
        AddChild(new SquareObstacle(new Vec2(500, 500), new Vec2(500, 550), new Vec2(550, 550), new Vec2(550, 500)));
        AddChild(new SquareObstacle(new Vec2(300, 100), new Vec2(300, 150), new Vec2(350, 150), new Vec2(350, 100)));
        AddChild(new SquareObstacle(new Vec2(380, 380), new Vec2(380, 430), new Vec2(430, 430), new Vec2(430, 380)));

        AddChild(new TriangleObstacle(new Vec2(380, 20), new Vec2 (380, 70), new Vec2(430, 70)));
        AddChild(new TriangleObstacle(new Vec2(200, 400), new Vec2(250, 400), new Vec2(250, 450)));


        //Add the ball
        ball = new Ball(17, new Vec2(width / 2, 620));
        
        //Add the aim
        aimToShoot = new AimToShoot();
		aimToShoot.SetOrigin(aimToShoot.width/2, aimToShoot.height/2 );
		aimToShoot.SetXY(ball.x, ball.y);
		AddChild(aimToShoot);

        //Add the paddle 
        paddle = new Paddle(new Vec2(width / 2 - 50, 700), new Vec2(width / 2 + 50, 700), ball);
        
    }

    float RotateToMouse()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 ballPos = new Vec2(width / 2, 620);
        Vec2 differenceVector = ballPos - mousePos;

        // Calculate the delta rotation based on the difference vector
        float deltaRotation = differenceVector.GetAngleDegrees();
        float targetRotation = deltaRotation;

        // Normalize the rotation to the range of 0 to 360 degrees
        if (targetRotation < 0)
            targetRotation += 360;

        // Clamp the rotation to the range of 10 to 170 degrees
        targetRotation = Mathf.Clamp(targetRotation, 10, 170);

        return targetRotation;
    }



    void Shoot()
    {
        //Rotates the aim
        if(aimToShoot != null)
        {
            aimToShoot.rotation = RotateToMouse() - 180;
        }

        if (Input.GetMouseButtonDown(0))
        {
            AddChild(ball);
            AddChild(paddle);

            aimToShoot.LateDestroy();
           
            //Adds velocity to the ball
            ball.velocity = Vec2.GetUnitVectorDegree(RotateToMouse() -180) * 10;
        }
    }

    //Create text and add it to the canvas for the LoseCondition
    void CreateLoseTextCanvas()
    {
        loseTextCanvas = new Canvas(width, height);
        loseTextCanvas.graphics.DrawString("You lose!", new Font("Arial", 40), Brushes.Red, width / 2 - 125, height / 2 - 20);
        AddChild(loseTextCanvas);
    }

    void LoseCondition()
    {

        if (ball.y > 717 )
        {
            // Destroy the ball if it goes under the paddle (ball.y > paddle.y + 17)
            ball.LateDestroy();
            paddle.LateDestroy();

            CreateLoseTextCanvas();
          
        }
    }

  

    void Update ()
	{
        Shoot();
        LoseCondition();
    }

	static void Main() 
    {
		new MyGame().Start();
	}
}