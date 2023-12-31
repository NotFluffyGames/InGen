// <auto-generated/>

using System;
using System.CodeDom.Compiler;
using InGen.Exceptions;
using JetBrains.Annotations;

namespace InGen.Target;

[GeneratedCode("InGen", "0.0.01")]
public partial class ChildContainer : 
    IContainer,
    IResolver<SingleInChild>,
    IResolver<ScopedInChild>,
    IResolver<TransientInChild>,
    IResolver<WithIdOnlyInChild>
{
    #region IContainer

    public override object Resolve(Type type, object id = null)
    {
        if (type == typeof(SingleInChild))
            return Resolve<SingleInChild>(id);

        if (type == typeof(ScopedInChild))
            return Resolve<ScopedInChild>(id);

        if (type == typeof(TransientInChild))
            return Resolve<TransientInChild>(id);

        if (type == typeof(WithIdOnlyInChild))
            return Resolve<WithIdOnlyInChild>(id);
        
        return base.Resolve(type, id);
    }

    public override IResult<object> TryResolve(Type type, [CanBeNull] object id = null)
    {
        if (type == typeof(SingleInChild))
            return TryResolve<SingleInChild>(id);

        if (type == typeof(ScopedInChild))
            return TryResolve<ScopedInChild>(id);
        
        if (type == typeof(TransientInChild))
            return TryResolve<TransientInChild>(id);

        if (type == typeof(WithIdOnlyInChild))
            return TryResolve<WithIdOnlyInChild>(id);
        
        return base.TryResolve(type, id);
    }

    public override IContainer CreateScope()
    {
        var scope = new Scope(this);
        _children.Add(scope);
        return scope;
    }

    protected override IContainer CreateRootScope() => new Scope(this);
    
    public override void Dispose()
    {
        //Dispose all IDisposable fields

        base.Dispose();
    }

    #endregion
    
    #region Resolvers
    
    private SingleInChild __singleInChild;
    private SingleInChild __singleInChildStringstring_id;
    
    bool IResolver<SingleInChild>.SupportsId([CanBeNull] object id)
        => id is null or "string_id";

    SingleInChild IResolver<SingleInChild>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "string_id" => __singleInChildStringstring_id ??= new SingleInChild(),
            null => __singleInChild ??= new SingleInChild(),
            _ => throw new InvalidIdException(this, typeof(SingleInChild), id)
        };
    }

    bool IResolver<ScopedInChild>.SupportsId([CanBeNull] object id)
        => (RootScope as IResolver<ScopedInChild>).SupportsId(id);
    
    ScopedInChild IResolver<ScopedInChild>.Resolve([CanBeNull] object id) 
        => RootScope.Resolve<ScopedInChild>(id);

    bool IResolver<TransientInChild>.SupportsId([CanBeNull] object id)
        => id is null or "string_id";
    
    TransientInChild IResolver<TransientInChild>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "string_id" => CreateTransient(),
            null => new TransientInChild(),
            _ => throw new InvalidIdException(this, typeof(TransientInChild), id)
        };
    }

    private WithIdOnlyInChild __withIdOnlyInChildInt5;

    bool IResolver<WithIdOnlyInChild>.SupportsId([CanBeNull] object id)
        => id is 5;
    
    WithIdOnlyInChild IResolver<WithIdOnlyInChild>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            5 => __withIdOnlyInChildInt5 ??= new WithIdOnlyInChild(),
            _ => throw new InvalidIdException(this, typeof(WithIdOnlyInChild), id)
        };
    }

    #endregion
    
    public new class Scope : 
        Container.Scope,
        IResolver<SingleInChild>,
        IResolver<ScopedInChild>,
        IResolver<TransientInChild>,
        IResolver<WithIdOnlyInChild>
    {
        private ChildContainer _root;

        public Scope(ChildContainer root) : base(root)
        {
            _root = root;
        }
        
        #region IContainer

        public override object Resolve(Type type, object id = null)
        {
            if (type == typeof(SingleInChild))
                return Resolve<SingleInChild>(id);

            if (type == typeof(ScopedInChild))
                return Resolve<ScopedInChild>(id);

            if (type == typeof(TransientInChild))
                return Resolve<TransientInChild>(id);

            if (type == typeof(WithIdOnlyInChild))
                return Resolve<WithIdOnlyInChild>(id);
        
            return base.Resolve(type, id);
        }

        public override IResult<object> TryResolve(Type type, [CanBeNull] object id = null)
        {
            if (type == typeof(SingleInChild))
                return TryResolve<SingleInChild>(id);

            if (type == typeof(ScopedInChild))
                return TryResolve<ScopedInChild>(id);

            if (type == typeof(TransientInChild))
                return TryResolve<TransientInChild>(id);

            if (type == typeof(WithIdOnlyInChild))
                return TryResolve<WithIdOnlyInChild>(id);
        
            return base.TryResolve(type, id);
        }
        
        public override IContainer CreateScope()
        {
            var scope = new Scope(_root);
            _children.Add(scope);
            return scope;
        }

        public override void Dispose()
        {
            //Dispose all IDisposable fields

            base.Dispose();
        }

        #endregion
        
        #region Resolvers

        bool IResolver<SingleInChild>.SupportsId([CanBeNull] object id)
            => (_root as IResolver<ScopedInChild>).SupportsId(id);

        SingleInChild IResolver<SingleInChild>.Resolve([CanBeNull] object id)
            => _root.Resolve<SingleInChild>(id);

        private ScopedInChild _inGenContainerTargetScopedInClass;

        bool IResolver<ScopedInChild>.SupportsId([CanBeNull] object id)
            => id is null;
        
        ScopedInChild IResolver<ScopedInChild>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
                null => _inGenContainerTargetScopedInClass ??= new ScopedInChild(),
                _ => throw new InvalidIdException(this, typeof(ScopedInChild), id)
            };
        }

        bool IResolver<TransientInChild>.SupportsId([CanBeNull] object id)
            => (_root as IResolver<TransientInChild>).SupportsId(id);
        
        TransientInChild IResolver<TransientInChild>.Resolve(object id)
            => _root.Resolve<TransientInChild>(id);

        bool IResolver<WithIdOnlyInChild>.SupportsId([CanBeNull] object id)
            => (_root as IResolver<WithIdOnlyInChild>).SupportsId(id);
        
        WithIdOnlyInChild IResolver<WithIdOnlyInChild>.Resolve(object id)
            => _root.Resolve<WithIdOnlyInChild>(id);

        #endregion
    }
}