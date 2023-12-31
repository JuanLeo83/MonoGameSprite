﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameSprite.sprite;

namespace MonoGameSprite;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SonicAnimation _sonicAnimation;
    private Rectangle _destinationRectangle;

    private KeyboardState _lastKeyboardState;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        _sonicAnimation = new SonicAnimation(GraphicsDevice);
        _destinationRectangle = new Rectangle(100, 100, 48 * 4, 48 * 4);

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sonicAnimation.loadContent(Content);
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
            _sonicAnimation.replay();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
            _sonicAnimation.playLookUp();
        }

        if (isKeyJustReleased(Keys.Up)) {
            _sonicAnimation.playLookDown();
        }

        if (isKeyJustPressed(Keys.Left)) {
            _sonicAnimation.lookAtLeft();
        }

        if (isKeyJustPressed(Keys.Right)) {
            _sonicAnimation.lookAtRight();
        }

        _sonicAnimation.update(
            _destinationRectangle.X,
            _destinationRectangle.Y,
            _destinationRectangle.Width,
            _destinationRectangle.Height);

        _lastKeyboardState = Keyboard.GetState();

        base.Update(gameTime);
    }

    private bool isKeyJustReleased(Keys key) {
        return _lastKeyboardState.IsKeyDown(key) && Keyboard.GetState().IsKeyUp(key);
    }

    private bool isKeyJustPressed(Keys key) {
        return _lastKeyboardState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Gray);

        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        _sonicAnimation.draw(_spriteBatch, _destinationRectangle);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}