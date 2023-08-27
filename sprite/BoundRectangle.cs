using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameSprite.sprite; 

public class BoundRectangle {
    private readonly Texture2D _bound;
    private Color _color = Color.GreenYellow;
    private int _lineWidth = 1;

    private Rectangle _leftBound;
    private Rectangle _topBound;
    private Rectangle _rightBound;
    private Rectangle _bottomBound;

    public BoundRectangle(GraphicsDevice graphicsDevice) {
        _bound = new Texture2D(graphicsDevice, 1, 1);
        _bound.SetData(new[] { Color.White });
    }

    public void setColor(Color color) {
        _color = color;
    }

    public void setWidth(int width) {
        _lineWidth = width;
    }

    public void update(int posX, int posY, int width, int height) {
        _leftBound = new Rectangle(posX, posY, _lineWidth, height + _lineWidth);
        _topBound = new Rectangle(posX, posY, width + _lineWidth, _lineWidth);
        _rightBound = new Rectangle(posX + width, posY, _lineWidth, height + _lineWidth);
        _bottomBound = new Rectangle(posX, posY + height, width + _lineWidth, _lineWidth);
    }

    public void draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_bound, _leftBound, _color);
        spriteBatch.Draw(_bound, _topBound, _color);
        spriteBatch.Draw(_bound, _rightBound, _color);
        spriteBatch.Draw(_bound, _bottomBound, _color);
    }
    
}