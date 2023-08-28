using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameSprite.sprite.animation;

namespace MonoGameSprite.sprite;

public class SonicAnimation {
    private const int FrameWidth = 48;
    private const int FrameHeight = 48;
    
    private readonly BoundRectangle _boundRectangle;
    private Texture2D _texture;

    private readonly ComposedAnimation _idle = new(2);
    private readonly ComposedAnimation _lookUp = new();
    private TransitionAnimation _lookDown;

    private IAnimation _currentAnimation;
    private SpriteEffects _orientation;

    public SonicAnimation(GraphicsDevice graphicsDevice = null) {
        _boundRectangle = new BoundRectangle(graphicsDevice);
    }

    private void initIdleAnimation() {
        Animation idleAnimation = new(false);
        idleAnimation.addFrame(setRectangle(0, 0), 60 * 3);
        _idle.addAnimation(idleAnimation);

        Animation bored1 = new(false);
        bored1.addFrame(setRectangle(1, 0), 5);
        bored1.addFrame(setRectangle(2, 0), 5);
        bored1.addFrame(setRectangle(3, 0), 5);
        bored1.addFrame(setRectangle(4, 0), 5);
        bored1.addFrame(setRectangle(5, 0), 5);
        _idle.addAnimation(bored1);

        Animation bored1Loop = new(false, 12);
        bored1Loop.addFrame(setRectangle(6, 0), 5);
        bored1Loop.addFrame(setRectangle(7, 0), 5);
        bored1Loop.addFrame(setRectangle(8, 0), 5);
        bored1Loop.addFrame(setRectangle(9, 0), 5);
        bored1Loop.addFrame(setRectangle(10, 0), 5);
        _idle.addAnimation(bored1Loop);

        Animation bored2 = new(false);
        bored2.addFrame(setRectangle(11, 0), 5);
        bored2.addFrame(setRectangle(12, 0), 5);
        bored2.addFrame(setRectangle(13, 0), 5);
        bored2.addFrame(setRectangle(14, 0), 5);
        bored2.addFrame(setRectangle(15, 0), 5);
        bored2.addFrame(setRectangle(16, 0), 5);
        bored2.addFrame(setRectangle(0, 1), 5);
        bored2.addFrame(setRectangle(1, 1), 5);
        _idle.addAnimation(bored2);

        Animation bored2Loop = new(false, 4);
        bored2Loop.addFrame(setRectangle(2, 1), 5);
        bored2Loop.addFrame(setRectangle(3, 1), 5);
        bored2Loop.addFrame(setRectangle(4, 1), 5);
        bored2Loop.addFrame(setRectangle(5, 1), 5);
        bored2Loop.addFrame(setRectangle(6, 1), 5);
        bored2Loop.addFrame(setRectangle(7, 1), 5);
        bored2Loop.addFrame(setRectangle(8, 1), 5);
        _idle.addAnimation(bored2Loop);

        Animation boredEnd = new(false);
        boredEnd.addFrame(setRectangle(9, 1), 5);
        boredEnd.addFrame(setRectangle(10, 1), 5);
        boredEnd.addFrame(setRectangle(11, 1), 5);
        boredEnd.addFrame(setRectangle(12, 1), 15);
        _idle.addAnimation(boredEnd);

        _idle.addAnimation(bored1.ReverseAnimation);
    }

    private void initLookUpAnimation() {
        Animation idleAnimation = new(false);
        idleAnimation.addFrame(setRectangle(0, 0), 5);
        _lookUp.addAnimation(idleAnimation);

        Animation first = new(false);
        first.addFrame(setRectangle(13, 1), 5);
        first.addFrame(setRectangle(14, 1), 5);
        first.addFrame(setRectangle(15, 1), 5);
        _lookUp.addAnimation(first);

        Animation last = new(true);
        last.addFrame(setRectangle(16, 1), 5);
        last.addFrame(setRectangle(17, 1), 60 * 5);
        _lookUp.addAnimation(last);
    }

    private void initLookDown() {
        Animation lookDown = new(false);
        lookDown.addFrame(setRectangle(18, 1), 5);
        lookDown.addFrame(setRectangle(19, 1), 5);
        _lookDown = new TransitionAnimation(lookDown, _idle);
        _lookDown.OnAnimationFinished += lookDown_OnAnimationFinished;
    }

    private void lookDown_OnAnimationFinished(IAnimation nextAnimation) {
        playAnimation(nextAnimation);
    }

    public void loadContent(ContentManager contentManager) {
        _texture = contentManager.Load<Texture2D>("sprites/sonic");

        initIdleAnimation();
        initLookUpAnimation();
        initLookDown();

        _currentAnimation = _idle;
    }

    public void replay() {
        _currentAnimation.reset();
    }

    public void playLookUp() {
        playAnimation(_lookUp);
    }

    public void playLookDown() {
        playAnimation(_lookDown);
    }

    public void lookAtRight() {
        _orientation = SpriteEffects.None;
    }

    public void lookAtLeft() {
        _orientation = SpriteEffects.FlipHorizontally;
    }

    private void playAnimation(IAnimation animation) {
        if (_currentAnimation != null && _currentAnimation != animation) {
            _currentAnimation.reset();
        }

        _currentAnimation = animation;
    }

    public void update(int posX, int posY, int width, int height) {
        _currentAnimation.update();
        _boundRectangle.update(posX + (int)(width * 0.25f), posY, (int)(width * 0.5f), height);
    }

    public void draw(SpriteBatch spriteBatch, Rectangle destinationRectangle) {
        spriteBatch.Draw(_texture, destinationRectangle, _currentAnimation.getSourceRectangle(), Color.White, 0f,
            Vector2.Zero, _orientation, 0f);
        
        _boundRectangle.draw(spriteBatch);
    }

    private Rectangle setRectangle(int coordX, int coordY) {
        return new Rectangle(coordX * FrameWidth, coordY * FrameHeight, FrameWidth, FrameHeight);
    }
}