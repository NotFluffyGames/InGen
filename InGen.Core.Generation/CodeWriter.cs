using System;
using System.Text;

namespace CodeGenCore;

public interface ICodeWriter
{
    void AppendLine(string value = "");
    IDisposable BeginIndentScope();
    IDisposable BeginBlockScope(string? startLine = null);
    void Clear();
}

public class CodeWriter : ICodeWriter
{
    private ICodeWriter _codeWriter = new InternalCodeWriter();

    public void AppendLine(string value = "") => _codeWriter.AppendLine(value);
    public IDisposable BeginIndentScope() => _codeWriter.BeginIndentScope();
    public IDisposable BeginBlockScope(string? startLine = null) => _codeWriter.BeginBlockScope(startLine);

    public void Clear()
    {
        _codeWriter.Clear();
        _codeWriter = new InternalCodeWriter();
    }

    public override string ToString() => _codeWriter.ToString();

    private class InternalCodeWriter : ICodeWriter
    {
        private readonly StringBuilder _buffer = new();
        private int _indentLevel;

        public void AppendLine(string? value = "")
        {
            if (string.IsNullOrEmpty(value))
                _buffer.AppendLine();
            else
                _buffer.AppendLine($"{new string(' ', _indentLevel * 4)} {value}");
        }

        public IDisposable BeginIndentScope() => new IndentScope(this);
        public IDisposable BeginBlockScope(string? startLine = null) => new BlockScope(this, startLine);

        private void IncreaseIndent()
        {
            _indentLevel++;
        }

        private void DecreaseIndent()
        {
            if (_indentLevel > 0)
                _indentLevel--;
        }

        private void BeginBlock()
        {
            AppendLine("{");
            IncreaseIndent();
        }

        private void EndBlock()
        {
            DecreaseIndent();
            AppendLine("}");
        }

        public void Clear()
        {
            _buffer.Clear();
            _indentLevel = 0;
        }

        public override string ToString() => _buffer.ToString();

        private readonly struct IndentScope : IDisposable
        {
            private readonly InternalCodeWriter _source;

            public IndentScope(InternalCodeWriter source)
            {
                _source = source;
                source.IncreaseIndent();
            }

            public void Dispose()
            {
                _source.DecreaseIndent();
            }
        }

        private readonly struct BlockScope : IDisposable
        {
            private readonly InternalCodeWriter _source;

            public BlockScope(InternalCodeWriter source, string? startLine = null)
            {
                _source = source;
                source.AppendLine(startLine);
                source.BeginBlock();
            }

            public void Dispose()
            {
                _source.EndBlock();
            }
        }
    }
}