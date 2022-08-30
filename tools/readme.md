Add new migration

`Add-Migration -Name Init -StartupProject Bootstrapper\Factum.Bootstrapper -Project Modules\Ledger\Factum.Modules.Ledger.Infrastructure -OutputDir .\EF\Migrations -Context LedgerDbContext`