using SubContractors.Common.Mediator.Decorators;

namespace SubContractors.Common.Mediator.Attributes
{
    public class RequestValidationAttribute : BaseDecoratorAttribute
    {
        public RequestValidationAttribute() : base(typeof(RequestValidationDecorator<,>))
        { }
    }
}