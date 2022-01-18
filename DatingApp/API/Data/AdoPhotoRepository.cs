using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class AdoPhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration _configuration;

        public AdoPhotoRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = connection.CreateCommand();
            command.CommandText = "dbo.GetPhotoById";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read()){

                return new Photo()
                {
                    Id = reader.GetInt32("Id"),
                    Url = reader.GetString("Url"),
                    IsMain = reader.GetBoolean("IsMain"),
                    IsApproved = reader.GetBoolean("IsApproved"),
                    PublicId = reader.GetString("PublicId"),
                    AppUserId = reader.GetInt32("AppUserId")
                };
            }

            return null;

        }

        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "dbo.GetUnapprovedPhotos";
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            var result = new List<PhotoForApprovalDto>();
            while (reader.Read()){

                result.Add(new PhotoForApprovalDto()
                {
                    IsApproved = reader.GetBoolean("IsApproved"),
                    Id = reader.GetInt32("Id"),
                    Url = reader.GetString("Url"),
                    Username = reader.GetString("Username"),
                });
            }

            return result;
        }

        public async void RemovePhotoAsync(Photo photo)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using var command = connection.CreateCommand();
            command.CommandText = "dbo.GetPhotoById";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", photo.Id);

            await command.ExecuteNonQueryAsync();

        }
    }
}