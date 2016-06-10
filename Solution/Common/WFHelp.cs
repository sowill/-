using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Runtime.DurableInstancing;
using System.Activities.XamlIntegration;

namespace Common
{
    public class WFHelp
    {
        public string Load(string id, string bookMark, params object[] ids)
        {
            _connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            _instanceStore = new SqlWorkflowInstanceStore(_connectionString);
            InstanceView view = _instanceStore.Execute
                (_instanceStore.CreateInstanceHandle(),
                new CreateWorkflowOwnerCommand(),
                TimeSpan.FromSeconds(30));
            _instanceStore.DefaultInstanceOwner = view.InstanceOwner;
            WorkflowApplication i = new WorkflowApplication(ActivityXamlServices.Load(""));
            i.InstanceStore = _instanceStore;
            i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;
            i.Load(new Guid(id));

            var dd = i.ResumeBookmark(bookMark, ids);
            //var b = i.GetBookmarks();
            return "ok";
        }

        private string _connectionString = "";
        private InstanceStore _instanceStore;
        public string GetId(IDictionary<string, object> parameters)
        {
            _connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            _instanceStore = new SqlWorkflowInstanceStore(_connectionString);
            InstanceView view = _instanceStore.Execute
                (_instanceStore.CreateInstanceHandle(),
                new CreateWorkflowOwnerCommand(),
                TimeSpan.FromSeconds(30));
            _instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            WorkflowApplication i = new WorkflowApplication(ActivityXamlServices.Load(""));
            i.InstanceStore = _instanceStore;
            i.PersistableIdle = (waiea) => PersistableIdleAction.Unload;
            i.Run();

            return i.Id.ToString();
        }
    }
}