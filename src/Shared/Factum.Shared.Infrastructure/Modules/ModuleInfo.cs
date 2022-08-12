using System.Collections.Generic;

namespace Factum.Shared.Infrastructure.Modules;

public record ModuleInfo(string Name, IEnumerable<string> Policies);