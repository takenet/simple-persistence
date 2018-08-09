﻿using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Xunit;

namespace Take.Elephant.Tests
{
    public abstract class ListAddableOnHeadFacts<T> : FactsBase
    {
        public abstract IListAddableOnHead<T> Create();

        public virtual T CreateItem()
        {
            return Fixture.Create<T>();
        }

        [Fact(DisplayName = nameof(AddNewItemToStartSucceeds))]
        public virtual async Task AddNewItemToStartSucceeds()
        {
            // Arrange
            var list = Create();
            var item = CreateItem();
            var item2 = CreateItem();

            // Act
            await list.AddAsync(item);
            await list.AddToStartAsync(item2);

            // Assert
            AssertEquals(await list.GetLengthAsync(), 2);
            var listEnumerable = await list.AsEnumerableAsync();
            AssertEquals(await listEnumerable.FirstAsync(), item2);
        }
    }
}