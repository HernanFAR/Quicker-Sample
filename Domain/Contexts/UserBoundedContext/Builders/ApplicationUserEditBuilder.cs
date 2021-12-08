using Domain.Contexts.UserBoundedContext.Core;

namespace Domain.Contexts.UserBoundedContext.Builders
{
    public class ApplicationUserEditBuilder : IAppUserMandatoryInfoUpdateBuilder, IAppUserOptionalInfoUpdateBuilder
    {
        private readonly ApplicationUser _Entity;

        internal string? Name;
        internal string? UserName;
        internal string? Email;

        internal string? SubName;
        internal string? PhoneNumber;

        private ApplicationUserEditBuilder(ApplicationUser entity)
        {
            _Entity = entity;
        }

        public static IAppUserMandatoryInfoUpdateBuilder UpdateEntity(ApplicationUser entity) =>
            new ApplicationUserEditBuilder(entity);

        public IAppUserMandatoryInfoUpdateBuilder ToName(string name)
        {
            Name = name;

            return this;
        }

        public IAppUserMandatoryInfoUpdateBuilder ToUserName(string userName)
        {
            UserName = userName;

            return this;
        }

        public IAppUserMandatoryInfoUpdateBuilder ToEmail(string email)
        {
            Email = email;

            return this;
        }

        public IAppUserOptionalInfoUpdateBuilder Continue => this;

        public IAppUserOptionalInfoUpdateBuilder ToSubName(string subName)
        {
            SubName = subName;

            return this;
        }

        public IAppUserOptionalInfoUpdateBuilder ToPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;

            return this;
        }

        public void UpdateInstance() => _Entity.UpdateInformation(this);
    }

    public interface IAppUserMandatoryInfoUpdateBuilder
    {
        IAppUserMandatoryInfoUpdateBuilder ToName(string name);
        IAppUserMandatoryInfoUpdateBuilder ToUserName(string userName);
        IAppUserMandatoryInfoUpdateBuilder ToEmail(string email);

        IAppUserOptionalInfoUpdateBuilder Continue { get; }
        void UpdateInstance();
    }

    public interface IAppUserOptionalInfoUpdateBuilder
    {
        IAppUserOptionalInfoUpdateBuilder ToSubName(string subName);
        IAppUserOptionalInfoUpdateBuilder ToPhoneNumber(string phoneNumber);

        void UpdateInstance();
    }
}
