﻿using Microsoft.Xna.Framework;

namespace MonoGameSprite.sprite.animation;

public class AnimationFrame {
    public Rectangle SourceRectangle { get; private set; }
    public int Lifespan { get; private set; }

    public AnimationFrame(Rectangle sourceRectangle, int lifespan) {
        SourceRectangle = sourceRectangle;
        Lifespan = lifespan;
    }
}