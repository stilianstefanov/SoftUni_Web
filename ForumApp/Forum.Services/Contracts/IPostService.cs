namespace Forum.Services.Contracts
{
    using ViewModels;

    public interface IPostService
    { 
        Task<IEnumerable<PostViewModel>> ListAllAsync();

        Task AddPostAsync(PostFormModel postFormModel);

        Task<PostFormModel> GetProductForEditOrDelete(string productId);

        Task EditPostByIdAsync(string id, PostFormModel postFormModel);

        Task DeletePostByIdAsync(string id);
    }
}
