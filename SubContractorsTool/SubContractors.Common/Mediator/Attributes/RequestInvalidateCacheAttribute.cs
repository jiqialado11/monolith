using SubContractors.Common.Mediator.Decorators;

namespace SubContractors.Common.Mediator.Attributes
{
    public class RequestInvalidateCacheAttribute : BaseDecoratorAttribute
    {
        public string _key;
        public RequestInvalidateCacheAttribute() : base(typeof(RequestInvalidateCacheDecorator<,>))
        {
        }
    }
}
