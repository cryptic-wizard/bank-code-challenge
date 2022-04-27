using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCodeChallengeTest.StepDefinitions
{
    [Binding]
    public class BankFixtureStepDefinitions
    {
        private readonly IObjectContainer objectContainer;

        public BankFixtureStepDefinitions(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        #region Scenario Steps

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Create global instance bank fixture for the Scenario
            objectContainer.RegisterInstanceAs(new BankFixture());
        }

        [AfterScenario]
        public void AfterScenario()
        {

        }

        #endregion
    }
}
