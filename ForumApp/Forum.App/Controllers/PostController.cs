namespace Forum.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using ViewModels;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }


        public async Task<IActionResult> All()
        {
            var allPosts =
                await postService.ListAllAsync();

            return View(allPosts);
        }

        public IActionResult Add()
        {
            return View(new PostFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            try
            {
                await postService.AddPostAsync(inputModel);
            }
            catch (Exception)
            {
               ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding your post!");

               return View(inputModel);
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            PostFormModel model = await postService.GetProductForEditOrDelete(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await postService.EditPostByIdAsync(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding your post!");

                return View(model);
            }

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await postService.DeletePostByIdAsync(id);
            }
            catch (Exception)
            {
                
            }

            return RedirectToAction("All");
        }
    }
}
