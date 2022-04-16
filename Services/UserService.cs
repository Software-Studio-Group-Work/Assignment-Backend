using Backend.Models;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;  
using System.Security.Claims;  
using System.Text;  
using Microsoft.Extensions.Options;

namespace Backend.Services;

public class UserService{
        private readonly IMongoCollection<User> _userCollection;
        private readonly string key;
    public UserService(MongoDbService mongoDbService, IOptions<JWTSettings> jwtSettings) {

        _userCollection = mongoDbService.UsersCollection;
        this.key=jwtSettings.Value.SecretKey;
    }
    public async Task<List<User>> GetAllUserService() { 
        return await _userCollection.Find(_=>true).ToListAsync();
        
    }
    public async Task<User?> GetOneUserService(string userId){
        return await _userCollection.Find(x=>x.Id==userId).FirstOrDefaultAsync();
    }
    public async Task CreateOneUserService(User user) { 
        await _userCollection.InsertOneAsync(user);
        return;
    }
    public async Task UpdateOneUserService(string userId, User updatedUser) {
        await _userCollection.ReplaceOneAsync(x => x.Id == userId, updatedUser);
        return;
    }
    public async Task DeleteOneUserService(string userId) { 
        await _userCollection.DeleteOneAsync(x => x.Id == userId);
        return;
    }

    public bool userIsCreated(string userId){
        var User = GetOneUserService(userId);
        if(User is null){
            return false;
        }
        return true;
    }

    public string AuthenticationService(string username,string password){
        var user= _userCollection.Find(x=>x.username==username&&x.password==password).FirstOrDefault();
        if(user==null){
            return null;
        }
        var tokenHandler=new JwtSecurityTokenHandler();
        var tokenKey=Encoding.ASCII.GetBytes(key);
        var tokenDescriptor= new SecurityTokenDescriptor(){
            Subject =new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.GivenName,username),
            }),
            Expires=DateTime.UtcNow.AddHours(1),

            SigningCredentials=new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token=tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

