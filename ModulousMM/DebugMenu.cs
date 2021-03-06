﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModulousMM
{
    public class KonamiSequence
    {
        readonly Keys[] _code = { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A };

        private int _offset;
        private readonly int _length, _target;

        public KonamiSequence()
        {
            _length = _code.Length - 1;
            _target = _code.Length;
        }

        public bool IsCompletedBy(Keys key)
        {
            _offset %= _target;

            if (key == _code[_offset]) _offset++;
            else if (key == _code[0])  _offset = 2;  // repeat index

            return _offset > _length;
        }
    }
}
