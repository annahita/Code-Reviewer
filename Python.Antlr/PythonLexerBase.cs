using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace Python.Antlr
{
    public abstract class PythonLexerBase : Lexer
    {
        // The stack that keeps track of the indentation level.
        private readonly Stack<int> _indents = new Stack<int>();

        //Edited 2020-6-29
        private readonly ICharStream _input;
        private IToken[] _buffer = new IToken[32];

        // A circular buffer where extra tokens are pushed on (see the NEWLINE and WS lexer rules).
        private int _firstTokensInd;
        private IToken _lastToken;
        private int _lastTokenInd;

        // The amount of opened braces, brackets and parenthesis.
        private int _opened;

        protected PythonLexerBase(ICharStream charStream)
            : base(charStream)
        {
            _input = charStream;
        }

        protected PythonLexerBase(ICharStream charStream, TextWriter output, TextWriter errorOutput) : base(charStream)
        {
            _input = charStream;
        }

        public static int TabSize { get; set; } = 8;

        public override void Emit(IToken token)
        {
            base.Emit(token);

            if (!(_buffer[_firstTokensInd] is null))
            {
                IncTokenInd(ref _lastTokenInd);

                if (_lastTokenInd == _firstTokensInd)
                {
                    // Enlarge buffer
                    var newArray = new IToken[_buffer.Length * 2];
                    var destInd = newArray.Length - (_buffer.Length - _firstTokensInd);

                    Array.Copy(_buffer, 0, newArray, 0, _firstTokensInd);
                    Array.Copy(_buffer, _firstTokensInd, newArray, destInd, _buffer.Length - _firstTokensInd);

                    _firstTokensInd = destInd;
                    _buffer = newArray;
                }
            }

            _buffer[_lastTokenInd] = token;
            _lastToken = token;
        }

        public override IToken NextToken()
        {
            // Check if the end-of-file is ahead and there are still some DEDENTS expected.
            if (_input.LA(1) == Eof && _indents.Count > 0)
            {
                if (_buffer[_lastTokenInd] is null || _buffer[_lastTokenInd].Type != PythonLexer.LINE_BREAK)
                    // First emit an extra line break that serves as the end of the statement.
                    Emit(PythonLexer.LINE_BREAK);

                // Now emit as much DEDENT tokens as needed.
                while (_indents.Count != 0)
                {
                    Emit(PythonLexer.DEDENT);
                    _indents.Pop();
                }
            }

            var next = base.NextToken();

            if (_buffer[_firstTokensInd] is null) return next;

            var result = _buffer[_firstTokensInd];
            _buffer[_firstTokensInd] = null;

            if (_firstTokensInd != _lastTokenInd) IncTokenInd(ref _firstTokensInd);

            return result;
        }

        protected void HandleNewLine()
        {
            Emit(PythonLexer.NEWLINE, Hidden, Text);

            var next = (char) _input.LA(1);

            // Process whitespaces in HandleSpaces
            if (next != ' ' && next != '\t' && IsNotNewLineOrComment(next)) ProcessNewLine(0);
        }

        protected void HandleSpaces()
        {
            var next = (char) _input.LA(1);

            if ((_lastToken == null || _lastToken.Type == PythonLexer.NEWLINE) && IsNotNewLineOrComment(next))
            {
                // Calculates the indentation of the provided spaces, taking the
                // following rules into account:
                //
                // "Tabs are replaced (from left to right) by one to eight spaces
                //  such that the total number of characters up to and including
                //  the replacement is a multiple of eight [...]"
                //
                //  -- https://docs.python.org/3.1/reference/lexical_analysis.html#indentation

                var indent = 0;

                foreach (var c in Text)
                    indent += c == '\t' ? TabSize - indent % TabSize : 1;

                ProcessNewLine(indent);
            }

            Emit(PythonLexer.WS, Hidden, Text);
        }

        protected void IncIndentLevel()
        {
            _opened++;
        }

        protected void DecIndentLevel()
        {
            if (_opened > 0) --_opened;
        }

        private bool IsNotNewLineOrComment(char next)
        {
            return _opened == 0 && next != '\r' && next != '\n' && next != '\f' && next != '#';
        }

        private void ProcessNewLine(int indent)
        {
            Emit(PythonLexer.LINE_BREAK);

            var previous = _indents.Count == 0 ? 0 : _indents.Peek();

            if (indent > previous)
            {
                _indents.Push(indent);
                Emit(PythonLexer.INDENT);
            }
            else
            {
                // Possibly emit more than 1 DEDENT token.
                while (_indents.Count != 0 && _indents.Peek() > indent)
                {
                    Emit(PythonLexer.DEDENT);
                    _indents.Pop();
                }
            }
        }

        private void IncTokenInd(ref int ind)
        {
            ind = (ind + 1) % _buffer.Length;
        }

        private void Emit(int tokenType, int channel = DefaultTokenChannel, string text = "")
        {
            //Edited 2020-6-29
            var _tokenFactorySourcePair = new Tuple<ITokenSource, ICharStream>(this, _input);
            IToken token =
#if LIGHT_TOKEN
                new PT.PM.AntlrUtils.LightToken((PT.PM.AntlrUtils.LightInputStream) _tokenFactorySourcePair.Item2,
                    tokenType,
                    channel, -1, CharIndex - text.Length, CharIndex);
#else
                new CommonToken(_tokenFactorySourcePair, tokenType, channel, CharIndex - text.Length, CharIndex)
                {
                    Line = Line,
                    Column = Column,
                    Text = text
                };
#endif
            Emit(token);
        }
    }
}