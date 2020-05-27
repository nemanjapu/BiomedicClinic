using BiomedicClinic.Core.Interfaces;

namespace BiomedicClinic.Core
{
    public interface IUnitOfWork
    {
        IClientReviewsRepository ClientReviews { get; }
        ILeadsRepository Leads { get; }
        IMenusRepository Menus { get; }
        IWebsitePagesRepository WebsitePages { get; }
        IGlobalSettingsRepository GlobalSettings { get; }

        void Complete();
    }
}