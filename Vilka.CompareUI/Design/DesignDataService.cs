using System;
using Vilka.CompareUI.Model;

namespace Vilka.CompareUI.Design
{
	public class DesignDataService : IDataService
	{
        public void ComperatorGetNextLeague(Action<string, string, string, string, Action<bool>> callback)
        {
            callback("La Liga", "Primera Division", "Spain", "Football", AnswerLeagueCompareRequest);
        }

        private void AnswerLeagueCompareRequest(bool answer)
        {

        }
	}
}