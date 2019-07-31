using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Data.Repositories
{
	public class ClaimsRepository : IClaimsRepository
	{
		private bool _disposed;
		private readonly AccountsDbContext _dbContext;
		private readonly IMap<DbClaims, UserClaim> _claimsMapper;

		private readonly DbSet<DbClaims> _dbSet;

		private DbSet<DbClaims> Claims
		{
			get
			{
				ThrowIfDisposed();
				return _dbSet;
			}
		}

		public ClaimsRepository(AccountsDbContext dbContext, IMap<DbClaims, UserClaim> claimsMapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_claimsMapper = claimsMapper ?? throw new ArgumentNullException(nameof(claimsMapper));
			_dbSet = _dbContext.Claims;
		}

		public UserClaim Get(int id)
		{
			var dbClaim = Claims.Find(id);

			return _claimsMapper.Map(dbClaim);
		}

		public IEnumerable<UserClaim> GetAll()
		{
			var dbClaims = Claims.ToArray();

			return _claimsMapper.Map(dbClaims);
		}

		public IEnumerable<UserClaim> GetClaimsByUser(int userId)
		{
			var dbClaims = Claims.Where(claim => claim.UserId == userId).ToArray();

			return _claimsMapper.Map(dbClaims);
		}

		public UserClaim Add(UserClaim entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var dbClaim = _claimsMapper.ReverseMap(entity);

			Claims.Add(dbClaim);
			SaveChanges();

			var userClaim = _claimsMapper.Map(dbClaim);

			return userClaim;
		}

		public IEnumerable<UserClaim> AddRange(IEnumerable<UserClaim> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			var dbClaims = _claimsMapper.ReverseMap(entities).ToArray();

			Claims.AddRange(dbClaims);
			SaveChanges();

			var userClaims = _claimsMapper.Map(dbClaims);

			return userClaims;
		}

		public UserClaim Update(UserClaim entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var dbClaim = _claimsMapper.ReverseMap(entity);

			Claims.Update(dbClaim);
			SaveChanges();

			var userClaim = _claimsMapper.Map(dbClaim);

			return userClaim;
		}

		public IEnumerable<UserClaim> UpdateRange(IEnumerable<UserClaim> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			var dbClaims = _claimsMapper.ReverseMap(entities).ToArray();

			Claims.UpdateRange(dbClaims);
			SaveChanges();

			var userClaims = _claimsMapper.Map(dbClaims);

			return userClaims;
		}

		public void Delete(int id)
		{
			var claim = Claims.Find(id);
			Claims.Remove(claim);
			SaveChanges();
		}

		public void DeleteRange(IEnumerable<int> ids)
		{
			var idArray = ids.ToArray();
			var claims = Claims.Where(claim => idArray.Contains(claim.Id));

			Claims.RemoveRange(claims);
			SaveChanges();
		}


		protected void SaveChanges()
		{
			ThrowIfDisposed();
			_dbContext.SaveChanges();
		}

		public void Dispose()
		{
			ThrowIfDisposed();

			_dbContext.Dispose();
			_disposed = true;
		}

		private void ThrowIfDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(nameof(_dbContext));
		}
	}
}