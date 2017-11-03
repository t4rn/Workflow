using Microsoft.AspNet.Identity.EntityFramework;

namespace Workflow.Model
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string name) : base(name)
        {

        }
    }
}
