namespace Contacts.Services.Contracts
{
    using ViewModels;

    public interface IContactsService
    {
        Task<IEnumerable<ContactAllViewModel>> GetAllAsync();
        Task<IEnumerable<ContactAllViewModel>> GetUserTeamContacts(string userId);
        Task AddAsync(ContactAddViewModel contactViewModel);
        Task<ContactAddViewModel> GetContactByIdAsync(int id);
        Task EditAsync(ContactAddViewModel contactViewModel);
        Task AddContactToUserTeamAsync(int id, string userId);
        Task RemoveContactFromUserTeamAsync(int id, string userId);
    }
}
