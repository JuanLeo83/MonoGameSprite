using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation;

public class ComposedAnimation : IAnimation {
    private readonly List<Animation> _animations = new();
    private int _currentAnimation;
    private readonly int _loopTimes;
    private int _repeatLoopCounter;

    private Animation CurrentAnimation { get; set; }

    public bool HasFinished => (_loopTimes > 0 && _repeatLoopCounter >= _loopTimes) || 
                               (_loopTimes == 0 && CurrentAnimation == _animations.Last() && CurrentAnimation.HasFinished);

    public ComposedAnimation(int loopTimes = 0) {
        _loopTimes = loopTimes;
    }
    
    public void addAnimation(Animation animation) {
        _animations.Add(animation);

        if (_animations.Count == 1) {
            CurrentAnimation = _animations[_currentAnimation];
        }
    }

    private void changeToNextAnimation() {
        _currentAnimation++;
        CurrentAnimation.reset();
        if (_currentAnimation >= _animations.Count) {
            _currentAnimation = 0;
            _repeatLoopCounter++;
        }
        CurrentAnimation = _animations[_currentAnimation];
    }

    public void update() {
        if (HasFinished) return;
        
        CurrentAnimation.update();

        if (CurrentAnimation.HasFinished && !HasFinished) {
            changeToNextAnimation();
        }
    }

    public bool hasFinished() => HasFinished;

    public void reset() {
        CurrentAnimation.reset();
        _currentAnimation = 0;
        _repeatLoopCounter = 0;
        CurrentAnimation = _animations[_currentAnimation];
    }

    public Rectangle getSourceRectangle() => CurrentAnimation.CurrentFrame.SourceRectangle;
}