using System;
using System.Collections.Generic;

namespace CodeGenCore;

public class CodeBuilder : IScope
{
    private readonly List<string> _namespaces = new();
    private Scope _root = new(null, string.Empty);
        
    public string StartLine => _root.StartLine;
    public IScope? ParentScope => _root.ParentScope;

    public IScope AddLineToScopeStart(string line)
    {
        _root.AddLineToScopeStart(line);
        return this;
    }

    public IScope AddLine(string line)
    {
        _root.AddLine(line);
        return this;
    }

    public IScope StartScope(string startLine)
    {
        return _root.StartScope(startLine);
    }

    public void WriteTo(CodeWriter codeWriter)
    {
        foreach (string namespaceName in _namespaces) 
            codeWriter.AppendLine($"using {namespaceName};");
            
        _root.WriteTo(codeWriter);
    }

    public CodeBuilder AddUsing(string namespaceName)
    {
        _namespaces.Add(namespaceName);
        return this;
    }

    IScope? IScope.Break()
    {
        return _root.Break();
    }

    public void Clear()
    {
        _namespaces.Clear();
        _root.Clear();
        _root = new(null, string.Empty);
    }
        
    private class Scope : IScope
    {
        public string StartLine { get; }
        public IScope? ParentScope { get; }
        private readonly List<Action<CodeWriter>> _codeOrder = new();

        public Scope(IScope? parentScope, string startLine)
        {
            StartLine = startLine;
            ParentScope = parentScope;
        }
        public IScope AddLineToScopeStart(string line)
        {
            _codeOrder.Insert(0, writer => writer.AppendLine(line));
            return this;
        }
        public IScope AddLine(string line)
        {
            _codeOrder.Add(writer => writer.AppendLine(line));
            return this;
        }
        public IScope StartScope(string startLine)
        {
            var scope = new Scope(this, startLine);
            _codeOrder.Add(writer => WriteScope(scope, writer));
            return scope;
        }

        private static void WriteScope(IScope scope, CodeWriter writer)
        {
            using (writer.BeginBlockScope(scope.StartLine))
            {
                scope.WriteTo(writer);
            }
        }
        public void WriteTo(CodeWriter codeWriter)
        {
            foreach (var action in _codeOrder)
            {
                action?.Invoke(codeWriter);
            }
        }
        public IScope? Break()
        {
            return ParentScope;
        }

        public void Clear()
        {
            _codeOrder.Clear();
        } 
    }
}