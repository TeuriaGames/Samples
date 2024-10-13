using System;
using System.Numerics;
using Riateu;
using Riateu.Audios;
using Riateu.Components;
using Riateu.Physics;

namespace Pong;

public class Ball : Entity 
{
    public Vector2 Velocity;
    private AnimatedSprite sprite;
    private const float SpeedLimitX = 80f;
    private const float SpeedLimitY = 5f;
    public float Speed = 100.0f;

    public Ball() 
    {
        sprite = AnimatedSprite.Create(Resource.Animations["pong/ball"]);
        sprite.FPS = 10;
        sprite.Play("idle");
        AddComponent(sprite);

        AddComponent(new Collision(new AABB(this, 0, 0, 8, 8)));
    }

    public override void Update(double delta)
    {
        float fDelta = (float)delta;
        Velocity.X = MathUtils.Clamp(Velocity.X, -SpeedLimitX, SpeedLimitX);
        Velocity.Y = MathUtils.Clamp(Velocity.Y, -SpeedLimitY, SpeedLimitY);

        int velX = (int)Math.Round(Velocity.X * fDelta * Speed);
        int velY = (int)Math.Round(Velocity.Y * fDelta * Speed);

        PosX += velX;
        PosY += velY;

        if (PosY > PingPongGame.ViewportHeight - 8) 
        {
            Velocity.Y = -1;
            Audio.PlaySound(Resource.BounceSound);
        }
        else if (PosY < 0) 
        {
            Velocity.Y = 1;
            Audio.PlaySound(Resource.BounceSound);
        }
        base.Update(delta);
    }
}