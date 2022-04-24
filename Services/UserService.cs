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
    public async Task<List<User>> GetUserByReligionService(string religion){
        return await _userCollection.Find(x=>x.religion==religion).ToListAsync();
    }
    public async Task<User?> GetOneUserService(string userId){
        return await _userCollection.Find(x=>x.Id==userId).FirstOrDefaultAsync();
    }
    public async Task<User?> GetOneUserService(string username,string password){
        return await _userCollection.Find(x=>(x.username==username&&x.password==password)).FirstOrDefaultAsync();
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
    public bool usernameExists(string username){
        bool exists = _userCollection.Find(_ => _.username== username).Any();
        return exists;
    }

    public async Task<User?> GetUserByTokenService(string token){
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = jsonToken as JwtSecurityToken;
        if(tokenS==null){
            return null;
        }
        var userId =  ValidateJwtToken(token);
        if(userId==""){
            return null;
        }
        return await GetOneUserService(userId);
    }
    public string ValidateJwtToken(string token)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenKey = Encoding.ASCII.GetBytes(this.key);
    try
    {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var accountId = jwtToken.Claims.First(x => x.Type == "userId").Value;

        // return account id from JWT token if validation successful
        return accountId;
    }
    catch
    {
        // return null if validation fails
        return "";
    }
}

        public string AuthenticationService(string username,string password){
        User user=  _userCollection.Find(x=>x.username==username&&x.password==password).FirstOrDefault();
        if(user==null){
            return "";
        }
        var tokenHandler=new JwtSecurityTokenHandler();
        var tokenKey=Encoding.ASCII.GetBytes(this.key);
        var tokenDescriptor= new SecurityTokenDescriptor(){
            Subject =new ClaimsIdentity(new Claim[]{
                new Claim("userId",user.Id),
                new Claim(ClaimTypes.Role,user.role),
            }),
            Expires=DateTime.UtcNow.AddHours(6),

            SigningCredentials=new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token=tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

