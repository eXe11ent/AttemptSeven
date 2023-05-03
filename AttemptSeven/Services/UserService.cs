using Amazon.Runtime.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

public class CreateCommand : IRequest<int>
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public interface IRequest<T>
{
}

public class UpdateCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}

public class DeleteCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class GetAllQuery : IRequest<List<User>>
{
}

public class UserService :
    IRequestHandler<CreateCommand, int>,
    IRequestHandler<UpdateCommand, int>,
    IRequestHandler<DeleteCommand, int>,
    IRequestHandler<GetAllQuery, List<User>>
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Login = request.Login,
            Password = request.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<int> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id);

        if (user == null)
            return 0;

        user.Login = request.Login;
        user.Password = request.Password;

        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<int> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id);

        if (user == null)
            return 0;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<List<User>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync();
    }
}
