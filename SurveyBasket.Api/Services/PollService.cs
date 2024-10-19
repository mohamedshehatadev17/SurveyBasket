
using Microsoft.AspNetCore.Http.HttpResults;
using SurveyBasket.Api.Models;
using System.Collections.Generic;

namespace SurveyBasket.Api.Services
{
    public class PollService : IPollService
    {

        private readonly static List<Poll> _polls = [
        new Poll{
                        Id = 1,
                        Title = "tile",
                        Description ="des1"
            }
        ];
        public Poll Add(Poll poll)
        {
            poll.Id = _polls.Count + 1;
            _polls.Add( poll );
            return ( poll );
        }

        public Poll? Get(int id) =>_polls.SingleOrDefault(p => p.Id == id);
        public IEnumerable<Poll> GetAll()=>_polls;

        public bool Update(int id,Poll poll)
        {
            var currentPool=Get(id);
            if (currentPool is null)
                return false;
            return true;

        }
        public bool Delete(int id)
        {
            var poll=Get(id);
            if (poll is null)
                return false;
            _polls.Remove( poll );

            return true;
        }
    }
}
