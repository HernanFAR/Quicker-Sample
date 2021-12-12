using Domain.Contexts.UserBoundedContext.Builders;
using Domain.Contexts.UserBoundedContext.ETOs;
using FluentAssertions;
using FluentValidation;
using System;
using Xunit;

namespace Domain.UnitTests.Contexts.UserBoundedContext.Core
{
    public class ApplicationUserTests
    {
        [Fact]
        public void ApplicationUserCreateBuilder_Success_Should_CreateEntity_Detail_RandomId()
        {
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const int expEventCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            applicationUser.Id.Should().NotBe(default(Guid));
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);

            applicationUser.GetEvents().Should().HaveCount(expEventCount);
            applicationUser.GetEvents().Should().ContainSingle(e => e.AfterSave && ((ApplicationUserHasBeenCreated)e.EventData).ApplicationUserId == applicationUser.Id);
        }

        [Fact]
        public void ApplicationUserCreateBuilder_Success_Should_CreateEntity_Detail_DefinedId()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const int expEventCount = 1;

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);

            applicationUser.GetEvents().Should().HaveCount(expEventCount);
            applicationUser.GetEvents().Should().ContainSingle(e => e.AfterSave && ((ApplicationUserHasBeenCreated)e.EventData).ApplicationUserId == applicationUser.Id);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdateAllProperties()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newName = "newName";
            const string newSubName = "newSubName";
            const string newUserName = "newUserName";
            const string newEmail = "newEmail";
            const string newPhoneNumber = "newPhoneNumber";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .ToEmail(newEmail)
                .ToName(newName).ToUserName(newUserName)
                .Continue
                .ToPhoneNumber(newPhoneNumber).ToSubName(newSubName)
                .UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(newName);
            applicationUser.SubName.Should().Be(newSubName);
            applicationUser.UserName.Should().Be(newUserName);
            applicationUser.Email.Should().Be(newEmail);
            applicationUser.PhoneNumber.Should().Be(newPhoneNumber);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdateName()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newName = "newName";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .ToName(newName).UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(newName);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdateSubName()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newSubName = "newSubName";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .Continue
                .ToSubName(newSubName)
                .UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(newSubName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdateUserName()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newUserName = "newUserName";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .ToUserName(newUserName).Continue.UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(newUserName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdateEmail()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newEmail = "newEmail";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .ToEmail(newEmail)
                .Continue.UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(newEmail);
            applicationUser.PhoneNumber.Should().Be(phoneNumber);
        }

        [Fact]
        public void ApplicationUserEditBuilder_Success_Should_CreateEntity_Detail_UpdatePhoneNumber()
        {
            var id = Guid.NewGuid();
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            const string newPhoneNumber = "newPhoneNumber";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithId(id).WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            ApplicationUserEditBuilder.UpdateEntity(applicationUser)
                .Continue.ToPhoneNumber(newPhoneNumber)
                .UpdateInstance();

            applicationUser.Id.Should().Be(id);
            applicationUser.Name.Should().Be(name);
            applicationUser.SubName.Should().Be(subName);
            applicationUser.UserName.Should().Be(userName);
            applicationUser.Email.Should().Be(email);
            applicationUser.PhoneNumber.Should().Be(newPhoneNumber);
        }

        [Fact]
        public void Validate_Failure_Should_ThrowValidationException_Detail_InvalidEntity()
        {
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email";
            const string phoneNumber = "phoneNumber";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            Action act = () => applicationUser.Validate();

            act.Should().ThrowExactly<ValidationException>();
        }

        [Fact]
        public void Validate_Success_Should_ThrowValidationException_Detail_InvalidEntity()
        {
            const string name = "name";
            const string subName = "subName";
            const string userName = "userName";
            const string email = "email@email.com";
            const string phoneNumber = "phoneNumber";

            var applicationUser = ApplicationUserCreateBuilder.Create
                .WithRandomId().WithEmail(email)
                .WithName(name).WithUserName(userName)
                .Continue
                .WithPhoneNumber(phoneNumber).WithSubName(subName)
                .GetInstance;

            applicationUser.Validate();

            true.Should().BeTrue();
        }
    }
}
