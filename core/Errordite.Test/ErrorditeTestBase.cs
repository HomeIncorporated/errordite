using System.Transactions;
using Castle.Core;
using Errordite.Core.IoC;
using Errordite.Core.Session;
using NUnit.Framework;

namespace Errordite.Test
{
    [TestFixture]
    public abstract class ErrorditeTestBase
    {
        private IAppSession _appSession;
        private TransactionScope _transactionScope;

        protected T Get<T>()
        {
            return ObjectFactory.GetObject<T>();
        }

        protected IAppSession Session
        {
            get { return _appSession ?? (_appSession = Get<IAppSession>()); }
        }

        [SetUp]
        protected void Setup()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.Required);
        }

        [TearDown]
        protected void TearDown()
        {
            if(_transactionScope != null)
                if (RollbackTransaction)
                    _transactionScope.Dispose();
                else
                    _transactionScope.Complete();
        }

        protected bool RollbackTransaction = true;
        
        [TestFixtureSetUp]
        public void ErrorditeTestBaseFixtureSetUp()
        {
            ObjectFactory.Container.Kernel.ComponentModelCreated += Kernel_ComponentModelCreated;
            ObjectFactory.Container.Install(
                new ErrorditeCoreInstaller("Errordite.Tests"), 
                new PerThreadAppSessionInstaller());
        }

        void Kernel_ComponentModelCreated(ComponentModel model)
        {
            if (model.LifestyleType == LifestyleType.PerWebRequest)
                model.LifestyleType = LifestyleType.Singleton;
        }
    }

    [TestFixture]
    public abstract class ErrorditeCacheTestBase
    {
        protected T Get<T>()
        {
            return ObjectFactory.GetObject<T>();
        }

        [TestFixtureSetUp]
        public void ErrorditeTestBaseFixtureSetUp()
        {
            ObjectFactory.Container.Kernel.ComponentModelCreated += Kernel_ComponentModelCreated;
            ObjectFactory.Container.Install(
                new ErrorditeCoreInstaller("Errordite.Tests"), 
                new PerThreadAppSessionInstaller());
        }

        [TestFixtureTearDown]
        public void ErrorditeTestBaseFixtureTearDown()
        {
            
        }

        void Kernel_ComponentModelCreated(ComponentModel model)
        {
            if (model.LifestyleType == LifestyleType.PerWebRequest)
                model.LifestyleType = LifestyleType.Singleton;
        }
    }
}