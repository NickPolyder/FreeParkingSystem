using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FreeParkingSystem.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Common.Data
{
	public abstract class BaseRepository<TBusinessObject, TEntity> : IRepository<TBusinessObject> where TEntity : class, IEntity where TBusinessObject : class
	{
		protected bool Disposed { get; private set; }

		private readonly DbContext _dbContext;

		protected DbContext DbContext
		{
			get
			{
				ThrowIfDisposed();
				return _dbContext;
			}
		}

		protected IMap<TEntity, TBusinessObject> Mapper { get; }

		private readonly DbSet<TEntity> _dbSet;

		protected DbSet<TEntity> Set
		{
			get
			{
				ThrowIfDisposed();
				return _dbSet;
			}
		}

		protected BaseRepository(DbContext dbContext, IMap<TEntity, TBusinessObject> mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_dbSet = DbContext.Set<TEntity>();
		}

		public bool Exists(int id)
		{
			return Set.Any(GetPrimaryKey(id));
		}

		public virtual TBusinessObject Get(int id)
		{
			var entity = GetDataSetWithIncludes().FirstOrDefault(GetPrimaryKey(id));

			return entity == null ? null : Mapper.Map(entity);
		}

		public virtual IEnumerable<TBusinessObject> GetAll()
		{
			var query = GetDataSetWithIncludes();

			var entities = query.ToArray();

			return Mapper.Map(entities);
		}

		protected IQueryable<TEntity> GetDataSetWithIncludes()
		{
			var includes = GetIncludes().ToArray();
			IQueryable<TEntity> query = Set;

			foreach (var expression in includes)
			{
				query = query.Include(expression);
			}

			return query;
		}

		public virtual TBusinessObject Add(TBusinessObject entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var dbObject = Mapper.Map(entity);

			DetachObject(dbObject.Id);

			Set.Add(dbObject);

			DbContext.SaveChanges();

			var businessObject = Mapper.Map(dbObject);

			return businessObject;
		}

		public virtual IEnumerable<TBusinessObject> AddRange(IEnumerable<TBusinessObject> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			var dbObjects = Mapper.Map(entities).ToArray();

			DetachObjects(dbObjects.Select(item => item.Id));

			Set.AddRange(dbObjects);

			DbContext.SaveChanges();

			var businessObjects = Mapper.Map(dbObjects);

			return businessObjects;
		}

		public virtual TBusinessObject Update(TBusinessObject entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var dbObject = Mapper.Map(entity);

			DetachObject(dbObject.Id);

			Set.Update(dbObject);

			DbContext.SaveChanges();

			var businessObject = Mapper.Map(dbObject);

			return businessObject;
		}

		public virtual IEnumerable<TBusinessObject> UpdateRange(IEnumerable<TBusinessObject> entities)
		{

			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			var dbObjects = Mapper.Map(entities).ToArray();

			DetachObjects(dbObjects.Select(item => item.Id));

			Set.UpdateRange(dbObjects);

			DbContext.SaveChanges();

			var businessObjects = Mapper.Map(dbObjects);

			return businessObjects;
		}

		public virtual void Delete(int id)
		{
			var claim = Set.Find(id);

			if (claim == null)
				return;

			Set.Remove(claim);

			DbContext.SaveChanges();
		}

		public virtual void DeleteRange(IEnumerable<int> ids)
		{
			var idArray = ids.ToArray();
			var claims = Set.Where(claim => idArray.Contains(claim.Id)).ToArray();

			if (claims.Length == 0)
				return;

			Set.RemoveRange(claims);

			DbContext.SaveChanges();
		}

		protected virtual IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
		{
			return Enumerable.Empty<Expression<Func<TEntity, object>>>();
		}

		protected virtual Expression<Func<TEntity, bool>> GetPrimaryKey(int id) => entity => entity.Id == id;

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ThrowIfDisposed();

				_dbContext.Dispose();
				Disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void DetachObject(int id)
		{
			var local = Set.Local.FirstOrDefault(entry => entry.Id.Equals(id));

			// check if local is not null 
			if (local != null) // I'm using a extension method
			{
				// detach
				DbContext.Entry(local).State = EntityState.Detached;
			}
		}

		private void DetachObjects(IEnumerable<int> ids)
		{
			var localArray = Set.Local.Where(entry => ids.Contains(entry.Id)).ToArray();

			foreach (var local in localArray)
			{
				// detach
				DbContext.Entry(local).State = EntityState.Detached;
			}
		}

		protected void ThrowIfDisposed()
		{
			if (Disposed)
				throw new ObjectDisposedException(nameof(_dbContext));
		}
	}
}