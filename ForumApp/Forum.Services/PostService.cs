namespace Forum.Services;

using Microsoft.EntityFrameworkCore;
using Data;
using ViewModels;
using Contracts;
using Forum.Data.Models;

public class PostService : IPostService
{
    private readonly ForumDbContext dbContext;

    public PostService(ForumDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<PostViewModel>> ListAllAsync()
    {
        var allProducts = await dbContext.Posts
            .Select(p => new PostViewModel()
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Content = p.Content
            })
            .ToArrayAsync();

        return allProducts;
    }

    public async Task AddPostAsync(PostFormModel postFormModel)
    {
        await dbContext.Posts
            .AddAsync(new Post()
            {
                Content = postFormModel.Content,
                Title = postFormModel.Title
            });

        await dbContext.SaveChangesAsync();
    }

    public async Task<PostFormModel> GetProductForEditOrDelete(string productId)
    {
        var post = await dbContext.Posts
            .FirstAsync(p => p.Id.ToString() == productId);

        return new PostFormModel()
        {
            Title = post.Title,
            Content = post.Content
        };
    }

    public async Task EditPostByIdAsync(string id, PostFormModel postFormModel)
    {
        var postToEdit = await dbContext.Posts
            .FirstAsync(p => p.Id.ToString() == id);

        postToEdit.Content = postFormModel.Content;
        postToEdit.Title = postFormModel.Title;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeletePostByIdAsync(string id)
    {
        var postToDel = await dbContext.Posts
            .FirstAsync(p => p.Id.ToString() == id);

        dbContext.Remove(postToDel);
        await dbContext.SaveChangesAsync();
    }
}
