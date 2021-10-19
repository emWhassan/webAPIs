﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NTPWebShop.Services.Tests
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using NSubstitute;

    public static class NSubstituteUtils
    {
        public static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data = null)
            where T : class
        {
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>, IDbAsyncEnumerable<T>>();

            if (data != null)
            {
                var queryable = data.AsQueryable();

                // setup all IQueryable and IDbAsyncEnumerable methods using what you have from "data"
                // the setup below is a bit different from the test above
                ((IDbAsyncEnumerable<T>)mockSet).GetAsyncEnumerator()
                    .Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));
                ((IQueryable<T>)mockSet).Provider.Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
                ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
                ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);
                ((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());
            }

            return mockSet;
        }
    }
}
