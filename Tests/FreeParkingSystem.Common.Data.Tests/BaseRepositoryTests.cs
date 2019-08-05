using System;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit;
using FreeParkingSystem.Common.Data.Tests.TestObjects;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.Data.Tests
{
	public class BaseRepositoryTests
	{
		private static void ContainerSetup(IFixture fixture)
		{
			MapperSetup(fixture);
		}

		private static void MapperSetup(IFixture fixture)
		{
			fixture.Build<IMap<TestEntity, TestBusinessObject>>()
				.FromFactory(() => new TestMapper())
				.ToCustomization()
				.Customize(fixture);
		}

		[Theory, FixtureData]
		public void Ctor_ShouldThrowExceptionForNullContext(
			Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange

			// Act
			var exception = Record.Exception(() => new TestRepository(null, mapperMock.Object));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Fact]
		public void Ctor_ShouldThrowExceptionForNullMapper()
		{
			// Arrange
			var context = GetDbContext();
			// Act
			var exception = Record.Exception(() => new TestRepository(context, null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void Add_WhenInputIsNull_ShouldThrowArgumentNullException(
			[Frozen] Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange 
			var dbContext = GetDbContext();

			var sut = new TestRepository(dbContext, mapperMock.Object);

			// Act
			var exception = Record.Exception(() => sut.Add(null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void Add_ShouldSaveTheTestBusinessObject(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject businessObject)
		{
			// Arrange
			businessObject.Id = default;

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				// Act
				var result = sut.Add(businessObject);
				var savedItem = dbContext.TestEntities.Find(result.Id);

				// Assert
				savedItem.ShouldNotBeNull();
				savedItem.Id.ShouldNotBe(default);
				savedItem.Id.ShouldBe(result.Id);
				savedItem.Name.ShouldBe(result.Name);
			}
		}

		[Theory, FixtureData]
		public void Add_ShouldNotSaveTheItemTwice(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject businessObject)
		{
			// Arrange
			businessObject.Id = default;

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{
				var result = sut.Add(businessObject);
				var dbItem = dbContext.TestEntities.Find(result.Id);
				dbContext.Entry(dbItem).State = EntityState.Detached;

				// Act
				var exception = Record.Exception(() => sut.Add(result));

				// Assert
				exception.ShouldNotBeNull();
				exception.ShouldBeOfType<ArgumentException>();
				exception.Message.ShouldBe($"An item with the same key has already been added. Key: {result.Id}");
			}
		}

		[Theory, FixtureData]
		public void AddRange_WhenInputIsNull_ShouldThrowArgumentNullException(
			[Frozen] Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange 
			var dbContext = GetDbContext();

			var sut = new TestRepository(dbContext, mapperMock.Object);

			// Act
			var exception = Record.Exception(() => sut.AddRange(null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void AddRange_ShouldSaveTheTestBusinessObjects(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{
				// Act
				var results = sut.AddRange(businessObjects).ToArray();

				// Assert
				results.Length.ShouldBe(businessObjects.Length);
				dbContext.TestEntities.Count().ShouldBe(businessObjects.Length);

				results.ForEach(result =>
				{
					var savedItem = dbContext.TestEntities.Find(result.Id);
					savedItem.ShouldNotBeNull();
					savedItem.Id.ShouldNotBe(default);
					savedItem.Id.ShouldBe(result.Id);
					savedItem.Name.ShouldBe(result.Name);
				});
			}
		}


		[Theory, FixtureData]
		public void Update_WhenInputIsNull_ShouldThrowArgumentNullException(
			[Frozen] Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange 
			var dbContext = GetDbContext();

			var sut = new TestRepository(dbContext, mapperMock.Object);

			// Act
			var exception = Record.Exception(() => sut.Update(null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void Update_ShouldUpdateTheTestBusinessObject(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject businessObject,
			TestBusinessObject businessObjectToUpdate)
		{
			// Arrange
			businessObject.Id = default;

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				var item = sut.Add(businessObject);
				businessObjectToUpdate.Id = item.Id;

				var dbItem = dbContext.TestEntities.Find(item.Id);
				dbContext.Entry(dbItem).State = EntityState.Detached;
				// Act

				var result = sut.Update(businessObjectToUpdate);
				var savedItem = dbContext.TestEntities.Find(result.Id);

				// Assert
				savedItem.ShouldNotBeNull();
				savedItem.Id.ShouldNotBe(default);
				savedItem.Id.ShouldBe(result.Id);
				savedItem.Name.ShouldBe(result.Name);
			}
		}

		[Theory, FixtureData]
		public void UpdateRange_WhenInputIsNull_ShouldThrowArgumentNullException(
			[Frozen] Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange 
			var dbContext = GetDbContext();

			var sut = new TestRepository(dbContext, mapperMock.Object);

			// Act
			var exception = Record.Exception(() => sut.UpdateRange(null));

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ArgumentNullException>();
		}

		[Theory, FixtureData]
		public void UpdateRange_ShouldSaveTheTestBusinessObjects(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects,
			TestBusinessObject[] businessObjectsToUpdate)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{
				// Arrange
				var addedItems = sut.AddRange(businessObjects).ToArray();
				addedItems.ForEach((resultItem, index) =>
				{
					businessObjectsToUpdate[index].Id = resultItem.Id;

					var dbItem = dbContext.TestEntities.Find(resultItem.Id);
					dbContext.Entry(dbItem).State = EntityState.Detached;
				});

				// Act
				var results = sut.UpdateRange(businessObjectsToUpdate).ToArray();

				// Assert
				results.Length.ShouldBe(businessObjects.Length);
				dbContext.TestEntities.Count().ShouldBe(businessObjects.Length);

				results.ForEach((result, index) =>
				{
					var expectedItem = businessObjectsToUpdate[index];

					var savedItem = dbContext.TestEntities.Find(result.Id);
					savedItem.ShouldNotBeNull();
					savedItem.Id.ShouldNotBe(default);
					savedItem.Id.ShouldBe(expectedItem.Id);
					savedItem.Name.ShouldBe(expectedItem.Name);
				});
			}
		}

		[Theory, FixtureData]
		public void Delete_WhenTheItemDoesNotExist_ShouldNotDeleteTheItem(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject businessObject,
			int wrongId)
		{
			// Arrange
			businessObject.Id = default;

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				sut.Add(businessObject);

				// Act
				sut.Delete(wrongId);

				// Assert
				dbContext.TestEntities.Count().ShouldBeGreaterThan(0);
			}
		}

		[Theory, FixtureData]
		public void Delete_WhenTheItemExists_ShouldDeleteTheItem(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject businessObject)
		{
			// Arrange
			businessObject.Id = default;

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				var result = sut.Add(businessObject);

				// Act
				sut.Delete(result.Id);

				// Assert
				dbContext.TestEntities.Count().ShouldBe(0);
			}
		}


		[Theory, FixtureData]
		public void DeleteRange_WhenTheItemsDoesNotExist_ShouldNotDeleteTheItems(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects,
			int[] wrongIds)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				sut.AddRange(businessObjects);

				// Act
				sut.DeleteRange(wrongIds);

				// Assert
				dbContext.TestEntities.Count().ShouldBeGreaterThan(0);
			}
		}

		[Theory, FixtureData]
		public void DeleteRange_WhenTheItemsDotExist_ShouldDeleteTheItems(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				var results = sut.AddRange(businessObjects).Select(item => item.Id);

				// Act
				sut.DeleteRange(results);

				// Assert
				dbContext.TestEntities.Count().ShouldBe(0);
			}
		}


		[Theory, FixtureData]
		public void WhenRepositoryIsDisposed_ShouldThrowObjectDisposedException(
			[Frozen] Mock<IMap<TestEntity, TestBusinessObject>> mapperMock)
		{
			// Arrange 
			var dbContext = GetDbContext();

			var sut = new TestRepository(dbContext, mapperMock.Object);

			// Act
			sut.Dispose();

			var exception = Record.Exception(() => sut.GetAll());

			// Assert
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<ObjectDisposedException>();
		}

		[Theory, FixtureData]
		public void GetAll_ShouldReturnAllItems(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				sut.AddRange(businessObjects);

				// Act
				var results = sut.GetAll().ToArray();

				// Assert
				results.Length.ShouldBe(businessObjects.Length);
				results.ForEach((result, index) => result.Name.ShouldBe(businessObjects[index].Name));
			}
		}

		[Theory, FixtureData]
		public void GetAll_WhenEmpty_ShouldReturnEmptyArray(
			IMap<TestEntity, TestBusinessObject> mapper)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{
				// Act
				var results = sut.GetAll().ToArray();

				// Assert
				results.Length.ShouldBe(0);
			}
		}

		[Theory, FixtureData]
		public void Get_ShouldReturnItem(
			IMap<TestEntity, TestBusinessObject> mapper,
			TestBusinessObject[] businessObjects)
		{
			// Arrange
			businessObjects.ForEach(businessObject => businessObject.Id = default);

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{

				var items = sut.AddRange(businessObjects).ToArray();

				// Act
				var result = sut.Get(items[0].Id);

				// Assert
				result.Name.ShouldBe(businessObjects[0].Name);
			}
		}

		[Theory, FixtureData]
		public void Get_WhenDoesNotExist_ShouldReturnNull(
			IMap<TestEntity, TestBusinessObject> mapper,
			int id)
		{
			// Arrange

			var dbContext = GetDbContext();

			using (var sut = new TestRepository(dbContext, mapper))
			{
				// Act
				var result = sut.Get(id);

				// Assert
				result.ShouldBeNull();
			}
		}

		private static TestContext GetDbContext()
		{
			var dbContext = new TestContext();
			return dbContext;
		}
	}

}