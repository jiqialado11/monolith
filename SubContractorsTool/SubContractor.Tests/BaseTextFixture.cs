using AutoMapper;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Common.Mapping;

namespace SubContractor.Tests
{
    public abstract class BaseTestFixture
    {
        protected static IMapper Mapper = MapperConfigurationProvider.Get()
                                                                     .CreateMapper();

        protected MockRepository MockRepository;
        protected Mock<IPublisher> Publisher;

        [SetUp]
        public virtual void SetUp()
        {
            MockRepository = new MockRepository(MockBehavior.Strict);
            Publisher = new Mock<IPublisher>();
        }

        [TearDown]
        public virtual void TearDown()
        {
            MockRepository.VerifyAll();
        }
    }
}