using SubContractors.Common.Mediator.Decorators;

namespace SubContractors.Common.Mediator.Attributes
{
    public class RequestTransactionAttribute : BaseDecoratorAttribute
    {
        public RequestTransactionAttribute() : base(typeof(RequestTransactionDecorator<,>))
        { }
    }
}