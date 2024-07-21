using MoonWorks.Input;
using MoonWorks.Math.Float;
using Riateu;
using Riateu.Components;
using Riateu.Physics;

namespace Pong;
public class Paddle : Entity 
{
    private SpriteRenderer sprite;
    private KeyCode up;
    private KeyCode down;
    private PhysicsComponent area;
    private bool left;
    public const float Speed = 100.0f;

    public Paddle(KeyCode up, KeyCode down, bool left = true) 
    {
        sprite = new SpriteRenderer(Resource.AtlasTexture, Resource.Atlas["pong/paddle"]);
        sprite.FlipX = !left;
        AddComponent(sprite);

        AddComponent(area = new PhysicsComponent(new AABB(this, 0, 0, 4, 24)));
        this.up = up;
        this.down = down;
        this.left = left;
    }

    public override void Update(double delta)
    {
        float deltaFloat = (float)delta;
        var axis = GetAxis(up, down);

        PosY += axis * Speed * deltaFloat;
        if (PosY > PingPongGame.ViewportHeight - 24) 
        {
            PosY = PingPongGame.ViewportHeight - 24;
        } 
        else if (PosY < 0) 
        {
            PosY = 0;
        }
        sbyte lVal = left ? (sbyte)-1 : (sbyte)1;

        if (area.CheckAll<Ball>(Vector2.Zero, out Ball ball)) 
        {
            if (lVal == ball.Velocity.X)
            {
                ball.Velocity.X *= -1;
                ball.Velocity.Y += 100 * 0.005f;
            }
        }
        
        base.Update(delta);
    }

    private int GetAxis(KeyCode up, KeyCode down) 
    {
        if (Input.Keyboard.IsDown(up)) 
        {
            return -1;
        }
        if (Input.Keyboard.IsDown(down)) 
        {
            return 1;
        }
        return 0;
    }
}