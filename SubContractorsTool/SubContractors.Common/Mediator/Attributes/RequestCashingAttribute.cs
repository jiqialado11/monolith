using SubContractors.Common.Mediator.Decorators;

namespace SubContractors.Common.Mediator.Attributes
{
    public class RequestCashingAttribute : BaseDecoratorAttribute
    {
        public RequestCashingAttribute() : base(typeof(RequestCachingDecorator<,>))
        {

        }
    }
}
