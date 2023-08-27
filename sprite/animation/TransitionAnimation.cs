using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation; 

public class TransitionAnimation : IAnimation {
    private IAnimation _from;
    private IAnimation _to;

    private Animation _animation;
    
    public void reset() {
        
    }

    public void update() {
        throw new System.NotImplementedException();
    }

    public bool hasFinished() {
        throw new System.NotImplementedException();
    }

    public Rectangle getSourceRectangle() {
        throw new System.NotImplementedException();
    }
}