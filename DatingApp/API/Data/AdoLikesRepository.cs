using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;

namespace API.Data
{
    public class AdoLikesRepository : ILikesRepository
    {
        private readonly IConfiguration _configuration;
        public AdoLikesRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserWithLikes(int userId)
        {
            throw new NotImplementedException();
        }
    }
}