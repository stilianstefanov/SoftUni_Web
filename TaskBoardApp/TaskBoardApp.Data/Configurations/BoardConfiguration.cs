namespace TaskBoardApp.Data.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Models;

internal class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasData(GenerateBoards());
    }

    private Board[] GenerateBoards()
    {
        ICollection<Board> boards = new HashSet<Board>();

        Board board;

        board = new Board()
        {
            Id = 1,
            Name = "Open"
        };

        boards.Add(board);

        board = new Board()
        {
            Id = 2,
            Name = "In Progress"
        };

        boards.Add(board);

        board = new Board()
        {
            Id = 3,
            Name = "Done"
        };

        boards.Add(board);

        return boards.ToArray();
    }
}
