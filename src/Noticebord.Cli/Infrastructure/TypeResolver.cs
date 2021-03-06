
using System;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Noticebord.Cli.Infrastructure
{
    public sealed class TypeResolver : ITypeResolver, IDisposable
    {
        private readonly IServiceProvider _provider;

        public TypeResolver(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public object? Resolve(Type? type)
        {
            if (type is null) return null;
            return _provider.GetRequiredService(type);
        }

        public void Dispose()
        {
            if (_provider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}