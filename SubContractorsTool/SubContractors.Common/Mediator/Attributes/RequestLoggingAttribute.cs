using SubContractors.Common.Mediator.Decorators;

namespace SubContractors.Common.Mediator.Attributes
{
    public class RequestLoggingAttribute : BaseDecoratorAttribute
    {
        public RequestLoggingAttribute() : base(typeof(RequestLoggingDecorator<,>))
        { }
    }
}