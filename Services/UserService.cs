using Backend.Models;
using MongoDB.Driver;


namespace Backend.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(MongoDbService mongoDbService)
    {
        _usersCollection = mongoDbService.UsersCollection;
    }

    public async Task<List<User>> GetAsync() => await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetByUsernameAsync(string username) => await _usersCollection.Find(x => x.username == username).FirstOrDefaultAsync();
    public async Task<User?> GetAsync(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) => await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updateUser) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

    public async Task RemoveAsync(string id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);
}