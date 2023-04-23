using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    public RotateCommand(SpriteRenderer spriteRenderer, Vector2 dir)
    {
        _dir = dir;
        _spriteRenderer = spriteRenderer;
    }

    public void Execute()
    {
        if (_spriteRenderer == null) return;
        if(_dir.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_dir.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    SpriteRenderer _spriteRenderer;
    public Vector2 _dir;
}
