using SoftStuApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;  
using Microsoft.Extensions.Configuration;  
using Microsoft.IdentityModel.Tokens;  
using System;  
using System.Collections.Generic;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Text;  
using System.Threading.Tasks;


namespace SoftStuApi.Services;

public class UserService{
        private readonly IMongoCollection<User> _userCollection;
        private readonly string key;
    public UserService(IOptions<MongoDBSettings> mongoDBSettings, IOptions<JWTSettings> jwtSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
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