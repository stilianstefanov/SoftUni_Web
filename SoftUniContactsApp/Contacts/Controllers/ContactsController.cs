namespace Contacts.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    using Services.Contracts;
    using ViewModels;

    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public async Task<IActionResult> All()
        {
            var contacts = await _contactsService.GetAllAsync();

            return View(contacts);
        }

        public async Task<IActionResult> Team()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var contacts = await _contactsService.GetUserTeamContacts(userId);

            return View(contacts);
        }

        public IActionResult Add()
        {
            var contactViewModel = new ContactAddViewModel();

            return View(contactViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactAddViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactViewModel);
            }

            await _contactsService.AddAsync(contactViewModel);

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contactViewModel = await _contactsService.GetContactByIdAsync(id);

            return View(contactViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactAddViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactViewModel);
            }

            await _contactsService.EditAsync(contactViewModel);

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _contactsService.AddContactToUserTeamAsync(id, userId);

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _contactsService.RemoveContactFromUserTeamAsync(id, userId);

            return RedirectToAction("Team");
        }
    }
}
