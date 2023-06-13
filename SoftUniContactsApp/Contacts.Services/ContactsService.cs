namespace Contacts.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using ViewModels;
    using Contracts;
    using Data.Models;

    public class ContactsService : IContactsService
    {
        private readonly ContactsDbContext _dbContext;

        public ContactsService(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ContactAllViewModel>> GetAllAsync()
        {
            var contacts = await _dbContext.Contacts
                .Select(c => new ContactAllViewModel
                {
                    ContactId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Email = c.Email,
                    Website = c.Website
                })
                .ToListAsync();

            return contacts;
        }

        public async Task<IEnumerable<ContactAllViewModel>> GetUserTeamContacts(string userId)
        {
            var contacts = await _dbContext
                .Contacts
                .Where(c => c.ApplicationUsersContacts.Any(uc => uc.ApplicationUserId == userId))
                .Select(c => new ContactAllViewModel()
                {
                    Address = c.Address,
                    ContactId = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website
                })
                .ToListAsync();

            return contacts;
        }

        public async Task AddAsync(ContactAddViewModel contactViewModel)
        {
            var contact = new Contact()
            {
                FirstName = contactViewModel.FirstName,
                LastName = contactViewModel.LastName,
                PhoneNumber = contactViewModel.PhoneNumber,
                Address = contactViewModel.Address,
                Email = contactViewModel.Email,
                Website = contactViewModel.Website
            };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ContactAddViewModel> GetContactByIdAsync(int id)
        {
            var contactViewModel = await _dbContext.Contacts
                .Where(c => c.Id == id)
                .Select(c => new ContactAddViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Email = c.Email,
                    Website = c.Website
                })
                .FirstAsync();

            return contactViewModel;
        }

        public async Task EditAsync(ContactAddViewModel contactViewModel)
        {
            var contact = await _dbContext
                .Contacts
                .FirstAsync(c => c.Id == contactViewModel.Id);

            contact.FirstName = contactViewModel.FirstName;
            contact.LastName = contactViewModel.LastName;
            contact.PhoneNumber = contactViewModel.PhoneNumber;
            contact.Address = contactViewModel.Address;
            contact.Email = contactViewModel.Email;
            contact.Website = contactViewModel.Website;

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddContactToUserTeamAsync(int id, string userId)
        {
            bool isAlreadyAdded = _dbContext
                .ApplicationUsersContacts
                .Any(uc => uc.ApplicationUserId == userId && uc.ContactId == id);

            if (!isAlreadyAdded)
            {
                var userContact = new ApplicationUserContact()
                {
                    ApplicationUserId = userId,
                    ContactId = id
                };

                await _dbContext.ApplicationUsersContacts.AddAsync(userContact);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveContactFromUserTeamAsync(int id, string userId)
        {
            var applicationUserContact = _dbContext
                .ApplicationUsersContacts
                .First(uc => uc.ApplicationUserId == userId && uc.ContactId == id);

            _dbContext.ApplicationUsersContacts.Remove(applicationUserContact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
