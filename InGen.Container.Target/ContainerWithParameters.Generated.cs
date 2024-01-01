// <auto-generated/>

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using InGen.Exceptions;
using JetBrains.Annotations;

namespace InGen.Target;

[GeneratedCode("InGen", "0.0.01")]
public partial class ContainerWithParameters
    : IContainer,
        IResolver<ClassWithDependency>,
        IResolver<IDependentInterface>
{
    private IContainer _rootScope;
    protected IContainer RootScope => _rootScope ??= CreateRootScope();
    protected virtual IContainer CreateRootScope() => new Scope(this);
    
    protected readonly List<IContainer> _children = new List<IContainer>();
    
    #region IContainer

    public T Resolve<T>([CanBeNull] object id = null)
    {
        if (this is IResolver<T> resolver)
            return resolver.Resolve(id);

        throw new InvalidTypeException(this, typeof(T), id);
    }
    
    public virtual IResult<T> TryResolve<T>([CanBeNull] object id = null)
    {
        if (this is IResolver<T> resolver)
        {
            if (resolver.SupportsId(id))
                return Result.FromValue(resolver.Resolve(id));

            return Result.FromException<T>(new InvalidIdException(this, typeof(T), id));
        }

        return Result.FromException<T>(new InvalidTypeException(this, typeof(T), id));
    }

    public virtual object Resolve(Type type, [CanBeNull] object id = null)
    {
        if (type == typeof(ClassWithDependency))
            return Resolve<ClassWithDependency>(id);

        if (type == typeof(IDependentInterface))
            return Resolve<IDependentInterface>(id);
        
        throw new InvalidTypeException(this, type, id);
    }
    
    public virtual IResult<object> TryResolve(Type type, [CanBeNull] object id = null)
    {
        if (type == typeof(ClassWithDependency))
            return TryResolve<ClassWithDependency>(id);

        if (type == typeof(IDependentInterface))
            return TryResolve<IDependentInterface>(id);
        
        return Result.FromException<object>(new InvalidTypeException(this, type, id));
    }

    public virtual IContainer CreateScope()
    {
        var scope = new Scope(this);
        _children.Add(scope);
        return scope;
    }

    public virtual void Dispose()
    {
        //Dispose all IDisposable fields

        foreach (var child in _children) 
            child.Dispose();
    }

    #endregion
        
    #region Resolvers

    private ClassWithDependency __InGenTargetClassWithDependency;
    private IDependentInterface __InGenTargetIDependentInterface_For_InGenTargetClassWithDependencyStringtransient_with_single_param;
    private IDependentInterface __GetInGenTargetIDependentInterface_For_InGenTargetClassWithDependencyStringtransient_with_single_param() 
        => __InGenTargetIDependentInterface_For_InGenTargetClassWithDependencyStringtransient_with_single_param ??= new DependentClass();
    
    private DependentClass __InGenTargetDependentClass_for_InGenTargetClassWithDependencyStringscopedwithsingleparam;

    bool IResolver<ClassWithDependency>.SupportsId([CanBeNull] object id)
        => id is null or "scoped-with-none-single-param" or "scoped-with-single-param" or "transient-with-none-single-param" or "transient-with-single-param";
    ClassWithDependency IResolver<ClassWithDependency>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "scoped-with-none-single-param" => RootScope.Resolve<ClassWithDependency>(id), 
            "scoped-with-single-param" => RootScope.Resolve<ClassWithDependency>(id), 
            "transient-with-none-single-param" => new ClassWithDependency(new DependentClass()), 
            "transient-with-single-param" => new ClassWithDependency(__GetInGenTargetIDependentInterface_For_InGenTargetClassWithDependencyStringtransient_with_single_param()), 
            null => __InGenTargetClassWithDependency ??= new ClassWithDependency(Resolve<IDependentInterface>()), 
            _ => throw new InvalidIdException(this, typeof(ClassWithDependency), id)
        };
    }

    private DependentClass __InGenTargetIDependentInterface;
    bool IResolver<IDependentInterface>.SupportsId([CanBeNull] object id)
        => id is null;
    IDependentInterface IResolver<IDependentInterface>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            null => __InGenTargetIDependentInterface ??= new DependentClass(),
            _ => throw new InvalidIdException(this, typeof(IDependentInterface), id)
        };
    }
    

    #endregion
        
    [GeneratedCode("InGen", "0.0.01")]
    public class Scope : 
        IContainer,
        IResolver<ClassWithDependency>,
        IResolver<IDependentInterface>
    {
        protected ContainerWithParameters _root;
        protected readonly List<IContainer> _children = new List<IContainer>(); 

        public Scope(ContainerWithParameters root)
        {
            _root = root;
        }
        
        #region IContainer
        
        public T Resolve<T>([CanBeNull] object id)
        {
            if (this is IResolver<T> resolver)
                return resolver.Resolve(id);

            throw new InvalidIdException(this, typeof(T), id);
        }
        
        public IResult<T> TryResolve<T>(object id)
        {
            if (this is IResolver<T> resolver)
            {
                if (resolver.SupportsId(id))
                    return Result.FromValue(resolver.Resolve(id));

                return Result.FromException<T>(new InvalidIdException(this, typeof(T), id));
            }
            
            return Result.FromException<T>(new InvalidTypeException(this, typeof(T), id));
        }
        
        public virtual object Resolve(Type type, [CanBeNull] object id = null)
        {
            if (type == typeof(ClassWithDependency))
                return Resolve<ClassWithDependency>(id);

            if (type == typeof(IDependentInterface))
                return Resolve<IDependentInterface>(id);

            throw new InvalidTypeException(this, type, id);
        }

        public virtual IResult<object> TryResolve(Type type, object id)
        {
            if (type == typeof(ClassWithDependency))
                return TryResolve<ClassWithDependency>(id);

            if (type == typeof(IDependentInterface))
                return TryResolve<IDependentInterface>(id);
        
            return Result.FromException<object>(new InvalidTypeException(this, type, id));
        }
        
        public virtual IContainer CreateScope()
        {
            var scope = new Scope(_root);
            _children.Add(scope);
            return scope;
        }

        public virtual void Dispose()
        {
            //Dispose all IDisposable fields

            foreach (var child in _children) 
                child.Dispose();
        }

        #endregion
            
        #region Resolvers

        private ClassWithDependency __InGenTargetClassWithDependencyStringscopedwithnonesingleparam;
        private ClassWithDependency __InGenTargetClassWithDependencyStringscopedwithsingleparam;
        
        bool IResolver<ClassWithDependency>.SupportsId([CanBeNull] object id)
            => id is null or "scoped-with-none-single-param" or "scoped-with-single-param" or "transient-with-none-single-param" or "transient-with-single-param";

        ClassWithDependency IResolver<ClassWithDependency>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
                "scoped-with-none-single-param" => __InGenTargetClassWithDependencyStringscopedwithnonesingleparam ??= new ClassWithDependency(new DependentClass()),
                "scoped-with-single-param" => __InGenTargetClassWithDependencyStringscopedwithsingleparam ??= new ClassWithDependency(_root.__InGenTargetDependentClass_for_InGenTargetClassWithDependencyStringscopedwithsingleparam ??= new DependentClass()),
                _ => _root.Resolve<ClassWithDependency>(id)
            };
        }

        bool IResolver<IDependentInterface>.SupportsId([CanBeNull] object id)
            => id is null;

        IDependentInterface IResolver<IDependentInterface>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
                _ => _root.Resolve<IDependentInterface>(id)
            };
        } 

        #endregion
    }
}