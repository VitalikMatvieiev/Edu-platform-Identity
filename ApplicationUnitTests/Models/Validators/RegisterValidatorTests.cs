using FluentValidation.TestHelper;
using Identity_Application.Models.Authorization;
using Identity_Application.Models.Validators;

namespace ApplicationUnitTests.Models.Validators
{
    public class RegisterValidatorTests
    {
        private readonly RegisterValidator _validator;

        public RegisterValidatorTests()
        {
            _validator = new RegisterValidator();
        }

        [Fact]
        public void Should_HaveError_WhenUsernameIsEmpty()
        {
            var model = new RegisterVM { Username = string.Empty, Email = "test@email.com", Password = "password" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Username);
        }

        [Fact]
        public void Should_HaveError_WhenUsernameIsTooLong()
        {
            var model = new RegisterVM { Username = "testuserandmoresymbolstooobigname", Email = "test@email.com", Password = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Username);
        }

        [Fact]
        public void Should_HaveError_WhenEmailIsInvalid()
        {
            var model = new RegisterVM { Username = "testuser", Email = "invalidEmail", Password = "password" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Email);
        }

        [Fact]
        public void Should_HaveError_WhenPasswordIsTooShort()
        {
            var model = new RegisterVM { Username = "testuser", Email = "test@email.com", Password = "123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Password);
        }
    }
}