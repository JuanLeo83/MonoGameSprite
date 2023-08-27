using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation; 

public class TransitionAnimation : IAnimation {
    private readonly IAnimation _to;
    private readonly IAnimation _animation;

    public event NotifyNextAnimation OnAnimationFinished;

    public TransitionAnimation(IAnimation animation, IAnimation to) {
        _animation = animation;
        _to = to;
    }
    
    public void reset() {
        _animation.reset();
    }

    public void update() {
        _animation.update();
        if (!hasFinished()) return;
        
        OnAnimationFinished?.Invoke(_to);
        reset();
    }

    public bool hasFinished() => _animation.hasFinished();

    public Rectangle getSourceRectangle() => _animation.getSourceRectangle();
}