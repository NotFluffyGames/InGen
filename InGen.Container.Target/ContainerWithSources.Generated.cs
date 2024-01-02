using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using InGen.Exceptions;
using JetBrains.Annotations;

namespace InGen.Target;

public partial class ContainerWithSources
    : IContainer,
        IResolver<Foo>,
        IResolver<IFoo>,
        IResolver<ClassWithDependency>,
        IResolver<IDependentInterface>
{
    private IContainer _rootScope;
    protected IContainer RootScope => _rootScope ??= CreateRootScope();
    protected virtual IContainer CreateRootScope() => new Scope(this);
    
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
        if(type == typeof(Foo))
            return Resolve<Foo>(id);
        
        if(type == typeof(IFoo))
            return Resolve<IFoo>(id);
        
        if(type == typeof(ClassWithDependency))
            return Resolve<ClassWithDependency>(id);
        
        if(type == typeof(IDependentInterface))
            return Resolve<IDependentInterface>(id);
        
        
        throw new InvalidTypeException(this, type, id);
    }
    
    public virtual IResult<object> TryResolve(Type type, [CanBeNull] object id = null)
    {
        if(type == typeof(Foo))
            return TryResolve<Foo>(id);
        
        if(type == typeof(IFoo))
            return TryResolve<IFoo>(id);
        
        if(type == typeof(ClassWithDependency))
            return TryResolve<ClassWithDependency>(id);
        
        if(type == typeof(IDependentInterface))
            return TryResolve<IDependentInterface>(id);
        
        return Result.FromException<object>(new InvalidTypeException(this, type, id));
    }

    public virtual IContainer CreateScope() => RootScope.CreateScope();

    public virtual void Dispose()
    {
        //Dispose all IDisposable fields
        
        _rootScope?.Dispose();
    }

    #endregion
        
    #region Resolvers

    bool IResolver<Foo>.SupportsId([CanBeNull] object id)
        => id is "method" or "field" or "property" or "delegate" or "lazy";

    Foo IResolver<Foo>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "method" => GetFooMethod(),
            "field" => _getFooField,
            "property" =>  GetFooProperty,
            "delegate" => _getFooDelegate(),
            "lazy" => _getFooLazy.Value,
            _ => throw new InvalidIdException(this, typeof(Foo), id)
        };
    }
    
    bool IResolver<IFoo>.SupportsId([CanBeNull] object id)
        => id is "method" or "field" or "property" or "delegate" or "lazy";

    IFoo IResolver<IFoo>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "method" => GetIFooMethod(),
            "field" => _getIFooField,
            "property" =>  GetIFooProperty,
            "delegate" => _getIFooDelegate(),
            "lazy" => _getIFooLazy.Value,
            _ => throw new InvalidIdException(this, typeof(IFoo), id)
        };
    }
    
    bool IResolver<ClassWithDependency>.SupportsId([CanBeNull] object id)
        => id is "method" or "delegate";

    ClassWithDependency IResolver<ClassWithDependency>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            "method" => GetClassWithDependencyMethod(Resolve<IDependentInterface>()),
            "delegate" => _getClassWithDependencyDelegate(Resolve<IDependentInterface>()),
            _ => throw new InvalidIdException(this, typeof(ClassWithDependency), id)
        };
    }
    
    bool IResolver<IDependentInterface>.SupportsId([CanBeNull] object id)
        => id is null;

    IDependentInterface IResolver<IDependentInterface>.Resolve([CanBeNull] object id)
    {
        return id switch
        {
            null => new DependentClass(),
            _ => throw new InvalidIdException(this, typeof(IDependentInterface), id)
        };
    }

    #endregion
        
    [GeneratedCode("InGen", "0.0.01")]
    public class Scope : 
        IContainer,
        IResolver<Foo>,
        IResolver<IFoo>,
        IResolver<ClassWithDependency>,
        IResolver<IDependentInterface>
    {
        protected ContainerWithSources _root;
        protected readonly List<IContainer> _children = new List<IContainer>(); 

        public Scope(ContainerWithSources root)
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
            if(type == typeof(Foo))
                return Resolve<Foo>(id);
        
            if(type == typeof(IFoo))
                return Resolve<IFoo>(id);
        
            if(type == typeof(ClassWithDependency))
                return Resolve<ClassWithDependency>(id);
        
            if(type == typeof(IDependentInterface))
                return Resolve<IDependentInterface>(id);

            throw new InvalidTypeException(this, type, id);
        }

        public virtual IResult<object> TryResolve(Type type, object id)
        {
            if(type == typeof(Foo))
                return TryResolve<Foo>(id);
        
            if(type == typeof(IFoo))
                return TryResolve<IFoo>(id);
        
            if(type == typeof(ClassWithDependency))
                return TryResolve<ClassWithDependency>(id);
        
            if(type == typeof(IDependentInterface))
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

        bool IResolver<Foo>.SupportsId([CanBeNull] object id)
            => id is "method" or "field" or "property" or "delegate" or "lazy";

        Foo IResolver<Foo>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
                _ => _root.Resolve<Foo>(id)
            };
        }
    
        bool IResolver<IFoo>.SupportsId([CanBeNull] object id)
            => id is "method" or "field" or "property" or "delegate" or "lazy";

        IFoo IResolver<IFoo>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
                _ => _root.Resolve<IFoo>(id)
            };
        }
    
        bool IResolver<ClassWithDependency>.SupportsId([CanBeNull] object id)
            => id is "method" or "delegate";

        ClassWithDependency IResolver<ClassWithDependency>.Resolve([CanBeNull] object id)
        {
            return id switch
            {
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