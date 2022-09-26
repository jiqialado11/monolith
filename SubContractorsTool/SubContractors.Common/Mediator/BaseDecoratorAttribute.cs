using System;

namespace SubContractors.Common.Mediator
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BaseDecoratorAttribute : Attribute
    {
        public BaseDecoratorAttribute(Type decoratorType)
        {
            DecoratorType = decoratorType;
        }

        public Type DecoratorType { get; }
    }
}