using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation;

public class Animation {
    private readonly List<AnimationFrame> _frames = new();
    private int _animationAge;
    private int _lifespan = -1;
    private readonly bool _isLoop;
    private readonly int _loopTimes;
    private int _repeatLoopCounter;

    public int Lifespan {
        get {
            if (_lifespan >= 0) return _lifespan;

            _lifespan = 0;
            foreach (var frame in _frames) {
                _lifespan += frame.Lifespan;
            }

            return _lifespan;
        }
    }

    public AnimationFrame CurrentFrame {
        get {
            AnimationFrame currentFrame = null;
            var framesLifespan = 0;

            foreach (var frame in _frames) {
                if (framesLifespan + frame.Lifespan >= _animationAge) {
                    currentFrame = frame;
                    break;
                }

                framesLifespan += frame.Lifespan;
            }

            return currentFrame ?? _frames.LastOrDefault();
        }
    }

    public bool HasFinished => !_isLoop && 
                               (_loopTimes == 0 || (_loopTimes > 0 && _repeatLoopCounter >= _loopTimes)) &&
                               _animationAge >= Lifespan;

    public Animation(bool isLoop, int loopTimes = 0) {
        _isLoop = isLoop;
        _loopTimes = loopTimes;
    }

    public void addFrame(Rectangle sourceRectangle, int lifespan) {
        _frames.Add(new AnimationFrame(sourceRectangle, lifespan));
    }

    public void addFrame(AnimationFrame animationFrame) {
        _frames.Add(animationFrame);
    }

    public void update() {
        _animationAge++;

        switch (_isLoop) {
            case true when _animationAge > Lifespan:
                _animationAge = 0;
                break;
            case false when _loopTimes > 0 && _animationAge > Lifespan && _repeatLoopCounter < _loopTimes:
                _animationAge = 0;
                _repeatLoopCounter++;
                break;
        }
    }

    public void reset() {
        _animationAge = 0;
        _repeatLoopCounter = 0;
    }

    public Animation ReverseAnimation {
        get {
            var newAnimation = new Animation(_isLoop);
            for (var i = _frames.Count - 1; i >= 0; i--) {
                newAnimation.addFrame(_frames[i]);
            }

            return newAnimation;
        }
    }
}