using System;
using Domain.Contexts.UserBoundedContext.Core;

namespace Domain.Contexts.UserBoundedContext.Builders
{
    public class ApplicationUserCreateBuilder : IAppUserIdentifierCreateBuilder, IAppUserMandatoryInfoCreateBuilder, IAppUserOptionalInfoCreateBuilder
    {
        internal Guid Id;
        
        internal string Name = string.Empty;
        internal string UserName = string.Empty;
        internal string Email = string.Empty;

        internal string? SubName;
        internal string? PhoneNumber;

        private ApplicationUserCreateBuilder() { }

        public static IAppUserIdentifierCreateBuilder Create => new ApplicationUserCreateBuilder();

        public IAppUserMandatoryInfoCreateBuilder WithId(Guid id)
        {
            Id = id;

            return this;
        }

        public IAppUserMandatoryInfoCreateBuilder WithRandomId()
        {
            Id = Guid.NewGuid();

            return this;
        }

        public IAppUserMandatoryInfoCreateBuilder WithName(string name)
        {
            Name = name;

            return this;
        }

        public IAppUserMandatoryInfoCreateBuilder WithUserName(string userName)
        {
            UserName = userName;

            return this;
        }

        public IAppUserMandatoryInfoCreateBuilder WithEmail(string email)
        {
            Email = email;

            return this;
        }

        public IAppUserOptionalInfoCreateBuilder Continue => this;

        public IAppUserOptionalInfoCreateBuilder WithSubName(string subName)
        {
            SubName = subName;

            return this;
        }

        public IAppUserOptionalInfoCreateBuilder WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;

            return this;
        }

        public ApplicationUser GetInstance => new(this);
    }

    public interface IAppUserIdentifierCreateBuilder
    {
        IAppUserMandatoryInfoCreateBuilder WithId(Guid id);
        IAppUserMandatoryInfoCreateBuilder WithRandomId();
    }

    public interface IAppUserMandatoryInfoCreateBuilder
    {
        IAppUserMandatoryInfoCreateBuilder WithName(string name);
        IAppUserMandatoryInfoCreateBuilder WithUserName(string userName);
        IAppUserMandatoryInfoCreateBuilder WithEmail(string email);

        IAppUserOptionalInfoCreateBuilder Continue { get; }
    }

    public interface IAppUserOptionalInfoCreateBuilder
    {
        IAppUserOptionalInfoCreateBuilder WithSubName(string subName);
        IAppUserOptionalInfoCreateBuilder WithPhoneNumber(string phoneNumber);

        ApplicationUser GetInstance { get; }
    }
}
