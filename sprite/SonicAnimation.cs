using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameSprite.sprite.animation;

namespace MonoGameSprite.sprite;

public class SonicAnimation {
    private Texture2D _texture;

    private readonly ComposedAnimation _idle = new();
    private readonly ComposedAnimation _bored = new(2);

    private ComposedAnimation _currentAnimation;

    private void initIdleAnimation() {
        Animation idleAnimation = new(false);
        idleAnimation.addFrame(setRectangle(0, 0), 60);
        _idle.addAnimation(idleAnimation);
    }

    private void initBored1Animation() {
        Animation idleAnimation = new(false);
        idleAnimation.addFrame(setRectangle(0, 0), 120);
        _bored.addAnimation(idleAnimation);
        
        Animation bored1 = new(false);
        bored1.addFrame(setRectangle(1, 0), 5);
        bored1.addFrame(setRectangle(2, 0), 5);
        bored1.addFrame(setRectangle(3, 0), 5);
        bored1.addFrame(setRectangle(4, 0), 5);
        bored1.addFrame(setRectangle(5, 0), 5);
        _bored.addAnimation(bored1);

        Animation bored1Loop = new(false, 12);
        bored1Loop.addFrame(setRectangle(6, 0), 5);
        bored1Loop.addFrame(setRectangle(7, 0), 5);
        bored1Loop.addFrame(setRectangle(8, 0), 5);
        bored1Loop.addFrame(setRectangle(9, 0), 5);
        bored1Loop.addFrame(setRectangle(10, 0), 5);
        _bored.addAnimation(bored1Loop);
        _bored.addAnimation(bored1.ReverseAnimation);
    }

    public void loadContent(ContentManager contentManager) {
        _texture = contentManager.Load<Texture2D>("sprites/sonic");

        initIdleAnimation();
        initBored1Animation();

        _currentAnimation = _bored;
    }

    public void replay() {
        _currentAnimation.reset();
    }

    public void update() {
        _currentAnimation.update();
    }

    public void draw(SpriteBatch spriteBatch, Rectangle destinationRectangle) {
        spriteBatch.Draw(_texture, destinationRectangle, _currentAnimation.SourceRectangle, Color.White);
    }

    private const int FrameWidth = 48;
    private const int FrameHeight = 48;

    private Rectangle setRectangle(int coordX, int coordY) {
        return new Rectangle(coordX * FrameWidth, coordY * FrameHeight, FrameWidth, FrameHeight);
    }
}