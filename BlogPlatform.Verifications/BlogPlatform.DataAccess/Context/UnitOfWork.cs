using BlogPlatform.DataAccess.Context.Contracts;
using BlogPlatform.DataAccess.Repositories.Contracts;
using System;
using System.Threading.Tasks;

namespace BlogPlatform.DataAccess.Context
{
    public class UnitOfWork : IBloggingUnitOfWork
    {
        private bool _isDisposed;

        private readonly BlogContext _context;

        public IVerificationRepository AuthorVerifications { get; }

        public IVerificationStatusRepository VerificationStatuses { get; }

        public UnitOfWork(
            BlogContext context,
            IVerificationRepository authorVerifications,
            IVerificationStatusRepository verificationStatuses)
        {
            _context = context;
            AuthorVerifications = authorVerifications;
            VerificationStatuses = verificationStatuses;
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
