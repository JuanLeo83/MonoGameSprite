using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameSprite.sprite;

namespace MonoGameSprite;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SonicAnimation _sonicAnimation;
    private Rectangle _destinationRectangle;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        _sonicAnimation = new SonicAnimation();
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

        _sonicAnimation.update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Gray);

        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        _sonicAnimation.draw(_spriteBatch, _destinationRectangle);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}