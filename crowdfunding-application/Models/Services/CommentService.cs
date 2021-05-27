using crowdfunding_application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowdfunding_application.Models.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        public CommentService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
