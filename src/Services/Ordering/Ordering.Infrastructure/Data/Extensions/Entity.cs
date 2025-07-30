using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ordering.Infrastructure.Data.Extensions;

public static class Entity
{
    public static bool HasChangedOwnedEnitities(this EntityEntry entry) =>
        entry.References.Any(r =>
        r.TargetEntry != null &&
        r.TargetEntry.Metadata.IsOwned() &&
        (r.TargetEntry.State == EntityState.Modified || r.TargetEntry.State == EntityState.Added));
    
}

