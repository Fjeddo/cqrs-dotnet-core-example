using System;
using System.Collections.Generic;
using Parking.Api.Responses;

namespace Parking.Api.Queries
{
    public class GetAllParkingInfoQuery : IQuery<IEnumerable<ParkingInfo>>
    {
        public IEnumerable<ParkingInfo> Execute()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IQuery<TResult>
    {
        TResult Execute();
    }

    public class Queries
    {
        private static Dictionary<Type, object> Qs;


        private static void RegisterQuery<TQuery>(Func<object, TQuery> factory)
        {
            Qs.Add(typeof(TQuery), factory);
        }

        public static void ReqisterQueries()
        {
            Qs = new Dictionary<Type, object>();

            RegisterQuery(x => new GetAllParkingInfoQuery());
        }

        public TResult Execute<TQuery, TResult>(object param = null) where TQuery : IQuery<TResult>
        {
            IQuery<TResult> query = null;
            return query.Execute();
        }
    }
}
