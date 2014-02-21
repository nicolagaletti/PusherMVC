using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Linq;

namespace PusherMvc.Data.Tests.Fakes
{
    public class FakeRavenQueryable<T> : IRavenQueryable<T>
    {
        private IQueryable<T> source;

        public RavenQueryStatistics QueryStatistics { get; set; }

        public FakeRavenQueryable(IQueryable<T> source, RavenQueryStatistics stats = null)
        {
            this.source = source;
            QueryStatistics = stats;
        }

        public IRavenQueryable<T> Customize(Action<Raven.Client.IDocumentQueryCustomization> action)
        {
            return this;
        }

        public IRavenQueryable<T> Statistics(out RavenQueryStatistics stats)
        {
            stats = QueryStatistics;
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return source.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return new FakeRavenQueryProvider(source, QueryStatistics); }
        }

        public IRavenQueryable<T> AddQueryInput(string name, Raven.Json.Linq.RavenJToken value)
        {
            throw new NotImplementedException();
        }

        public IRavenQueryable<T> Spatial(System.Linq.Expressions.Expression<Func<T, object>> path, Func<Raven.Client.Spatial.SpatialCriteriaFactory, Raven.Client.Spatial.SpatialCriteria> clause)
        {
            throw new NotImplementedException();
        }

        public IRavenQueryable<TResult> TransformWith<TTransformer, TResult>() where TTransformer : Raven.Client.Indexes.AbstractTransformerCreationTask, new()
        {
            throw new NotImplementedException();
        }
    }

    public class FakeRavenQueryProvider : IQueryProvider
    {
        private IQueryable source;
        private RavenQueryStatistics stats;

        public FakeRavenQueryProvider(IQueryable source, RavenQueryStatistics stats = null)
        {
            this.source = source;
            this.stats = stats;
        }

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new FakeRavenQueryable<TElement>(source.Provider.CreateQuery<TElement>(expression), stats);
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {

            var type = typeof(FakeRavenQueryable<>).MakeGenericType(expression.Type);
            return (IQueryable)Activator.CreateInstance(type, source.Provider.CreateQuery(expression), stats);
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            return source.Provider.Execute<TResult>(expression);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            return source.Provider.Execute(expression);
        }
    }
}
