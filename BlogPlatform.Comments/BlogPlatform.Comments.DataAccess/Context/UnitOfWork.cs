using BlogPlatform.Comments.DataAccess.Context.Contracts;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;
using System;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.DataAccess.Context
{
    public class UnitOfWork : IBloggingUnitOfWork
    {
        private bool _isDisposed;

        private readonly BlogContext _context;

        public ICommentRepository Comments { get; }

        public UnitOfWork(BlogContext context, ICommentRepository comments)
        {
            _context = context;
            Comments = comments;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }
    }
}
