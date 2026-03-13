using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using ReiDoChopp.Application.Users.Services.Interfaces;
using ReiDoChopp.DataTransfer.Pagination.Responses;
using ReiDoChopp.DataTransfer.Users.Requests;
using ReiDoChopp.DataTransfer.Users.Responses;
using ReiDoChopp.Domain.Roles.Entities;
using ReiDoChopp.Domain.Roles.Services;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Users.Repositories;
using ReiDoChopp.Domain.Users.Repositories.Filters;
using ReiDoChopp.Domain.Users.Repositories.Models;
using ReiDoChopp.Domain.Users.Services.Interfaces;
using ReiDoChopp.Domain.Utils.Models;
using ReiDoChopp.Infra.Data;
using System.Data;
using System.Data.SqlTypes;

namespace ReiDoChopp.Application.Users.Services
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;
        private readonly ReiDoChoppDbContext dbContext;
        public UsersAppService(IMapper mapper, IUsersService usersService, IRolesService rolesService, IUsersRepository usersRepository, ReiDoChoppDbContext dbContext)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.usersRepository = usersRepository;
            this.dbContext = dbContext;
        }

        public async Task<LoginResponse> LoginAsync(UserLoginRequest request)
        {
            string token = await usersService.LoginAsync(request.Email, request.Password);

            return new LoginResponse { Token = token };
        }

        public async Task<UserResponse> RegisterAsync(UserRegisterRequest request)
        {
            usersService.MatchPassword(request.Password, request.CheckPassword);

            User user = await usersService.InsertAsync(request.FirstName, request.LastName, request.Email, request.Password);

            await usersService.SetUserRolesAsync(user, request.RoleIds);

            return mapper.Map<UserResponse>(user);
        } 
        
        public async Task<UserResponse> EditAsync(int id, UserEditRequest request)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                User user = await usersService.ValidateAsync(id);

                await usersService.UserEditAsync(user, request.Email, request.FirstName, request.LastName);

                await usersService.SetUserRolesAsync(user, request.RoleIds);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<UserResponse>(user);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UserResponse> ValidateAsync(int id)
        {
            User user = await usersService.ValidateAsync(id);

            UserResponse response = mapper.Map<UserResponse>(user);

            return response;
        }

        public async Task ForgotPasswordAsync(UserForgotPasswordRequest request)
        {
            User user = await usersService.GetUserByEmail(request.Email);

            await usersService.ForgotPasswordAsync(user);
        }

        public async Task ResetPasswordAsync(UserResetPasswordRequest request)
        {
            usersService.MatchPassword(request.Password, request.CheckPassword);

            User user = await usersService.GetUserByEmail(request.Email);

            await usersService.ResetPasswordAsync(user, request.Token, request.Password);
        }

        public async Task<UserResponse> GetCurrentUserAsync()
        {
            User user = await usersService.GetCurrentUserAsync();

            if (!user.Active)
                throw new Exception("Este usuário está inativado");

            return mapper.Map<UserResponse>(user);
        }

        public async Task<PaginationResponse<UserResponse>> ListAsync(UserListRequest request)
        {
            UsersListFilter filter = mapper.Map<UsersListFilter>(request);

            IQueryable<User> query = usersRepository.Filter(filter);

            PaginationModel<UserModel> model = await usersRepository.ListProjectionAsync(query, filter);

            return mapper.Map<PaginationResponse<UserResponse>>(model);
        }

        public async Task<UserResponse> ChangeStatusAsync(int id)
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                User user = await usersService.ValidateAsync(id);

                usersService.ChangeStatus(user);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<UserResponse>(user);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<DbTestResponse> DbTest()
        {
            try
            {
                User user = await usersService.ValidateAsync(1);

                return new DbTestResponse() { Result = true };
            }
            catch (SqlTypeException ex) {

                return new DbTestResponse() { Result = false };
            }
        }
    }
}
