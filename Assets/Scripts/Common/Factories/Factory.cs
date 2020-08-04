using System;

namespace Common.Factories
{
    public static class Factory
    {
        public static IFactory<TArg, TOut> Create<TArg, TOut>(Func<TArg, TOut> factoryMethod)
        {
            return new AnonymousFactory<TArg, TOut>(factoryMethod);
        }

        public static IFactory<TArg1, TArg2, TOut> Create<TArg1, TArg2, TOut>(Func<TArg1, TArg2, TOut> factoryMethod)
        {
            return new AnonymousFactory<TArg1, TArg2, TOut>(factoryMethod);
        }

        private class AnonymousFactory<TArg, TOut> : IFactory<TArg, TOut>
        {
            private readonly Func<TArg, TOut> _factoryMethod;

            public AnonymousFactory(Func<TArg, TOut> factoryMethod)
            {
                _factoryMethod = factoryMethod;
            }

            public TOut Create(TArg arg)
            {
                return _factoryMethod.Invoke(arg);
            }
        }

        private class AnonymousFactory<TArg1, TArg2, TOut> : IFactory<TArg1, TArg2, TOut>
        {
            private readonly Func<TArg1, TArg2, TOut> _factoryMethod;

            public AnonymousFactory(Func<TArg1, TArg2, TOut> factoryMethod)
            {
                _factoryMethod = factoryMethod;
            }

            public TOut Create(TArg1 arg1, TArg2 arg2)
            {
                return _factoryMethod.Invoke(arg1, arg2);
            }
        }
    }
}