using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Main;

public class Player
{
    public AnimatedSprite playerSprite;
    public Inventory inventory = new();

    public Vector2 position;
    Vector2 targetPosition;

    public float speed = 300;

    public Player(Vector2 position)
    {
        this.position = position;
        targetPosition = position;

        playerSprite = new AnimatedSprite("Assets/Textures/Entity/player.png", position, GameConfig.Scale);
        
        playerSprite.AddAnimation("idle_down", new Animation(
            new Vector2(16, 16), 0, 0.2f, new Vector2(0, 6)
        ));
        playerSprite.AddAnimation("idle_up", new Animation(
            new Vector2(16, 16), 1, 0.2f, new Vector2(0, 6)
        ));
        playerSprite.AddAnimation("idle_left", new Animation(
            new Vector2(16, 16), 2, 0.2f, new Vector2(0, 6)
        ));

        playerSprite.AddAnimation("walk_down", new Animation(
            new Vector2(16, 16), 4, 0.2f, new Vector2(0, 4)
        ));
        playerSprite.AddAnimation("walk_up", new Animation(
            new Vector2(16, 16), 5, 0.2f, new Vector2(0, 4)
        ));
        playerSprite.AddAnimation("walk_left", new Animation(
            new Vector2(16, 16), 6, 0.1f, new Vector2(0, 8)
        ));

        playerSprite.Play("idle_down");

        inventory.Add(TileId.Log, 1);
        inventory.Add(TileId.Plank, 1);
        inventory.Add(TileId.Table, 1);
    } 

    public void Update(Camera2D camera, TileMap tileMap, float delta)
    {
        Vector2 mouseScreenPos = Raylib.GetMousePosition();
        Vector2 mouseWorldPos = Raylib.GetScreenToWorld2D(mouseScreenPos, camera);
        Vector2 direction = Vector2.Zero;

        bool isMoving = false;

        string lastFacingDirection;

        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            direction.Y -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            direction.Y += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            direction.X -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            direction.X += 1;
        }

        float wheel = Raylib.GetMouseWheelMove();

        if (wheel > 0)
        {
            inventory.holdingSlot -= 1;
            if (inventory.holdingSlot < 0)
            {
                inventory.holdingSlot = inventory.slots.Length - 1;
            }
        }

        if (wheel < 0)
        {
            inventory.holdingSlot += 1;
            if (inventory.holdingSlot >= inventory.slots.Length - 1)
            {
                inventory.holdingSlot = 0;
            }
        }

        if (direction.Y < 0)
        {
            playerSprite.Play("walk_up");
            playerSprite.flipHorizontal = false;
            isMoving = true;
            lastFacingDirection = playerSprite.currentAnimation;
        } else if (direction.Y > 0)
        {
            playerSprite.Play("walk_down");
            playerSprite.flipHorizontal = false;
            isMoving = true;
            lastFacingDirection = playerSprite.currentAnimation;
        } else if (direction.X < 0)
        {
            playerSprite.Play("walk_left");
            playerSprite.flipHorizontal = false;
            isMoving = true;
            lastFacingDirection = playerSprite.currentAnimation;
        } else if (direction.X > 0)
        {
            playerSprite.Play("walk_left");
            playerSprite.flipHorizontal = true;
            isMoving = true;
            lastFacingDirection = playerSprite.currentAnimation;
        }

        if (!isMoving)
        {
            string idleAnimation = playerSprite.currentAnimation.Replace("walk", "idle");
            playerSprite.Play(idleAnimation);
        }

        if (direction.LengthSquared() > 0)
        {
            direction = Vector2.Normalize(direction);
        }

        playerSprite.Update(delta);

        Vector2 velocity = direction * speed * delta;
        Vector2 nextX = new(targetPosition.X + velocity.X, targetPosition.Y);

        if (!tileMap.IsSolidAtRect(GetHitbox(nextX)))
        {
            targetPosition.X = nextX.X;
        }

        Vector2 nextY = new(targetPosition.X, targetPosition.Y + velocity.Y);

        if (!tileMap.IsSolidAtRect(GetHitbox(nextY)))
        {
            targetPosition.Y = nextY.Y;
        }

        position = Vector2.Lerp(position, targetPosition, 12.0f * delta);

        playerSprite.position = position;
    }

    public Rectangle GetHitbox(Vector2 position)
    {
        return new Rectangle(position.X - 16, position.Y - 16, 32, 32);
    }

    public Rectangle GetInteractionHitbox(Vector2 position)
    {
        return new Rectangle(position.X - 28, position.Y - 28, 60, 60);
    }

    public void Draw()
    {
        playerSprite.Draw();
    }

    public void Unload()
    {
        playerSprite.Unload();
    }
}