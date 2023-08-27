using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation; 

public interface IAnimation {
    public void reset();
    public void update();
    public bool hasFinished();

    public Rectangle getSourceRectangle();
}