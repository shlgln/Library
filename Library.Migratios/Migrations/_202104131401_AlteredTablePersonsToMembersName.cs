using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Migratios.Migrations
{
    [Migration(202104131401)]
    public class _202104131401_AlteredTablePersonsToMembersName : FluentMigrator.Migration
    {
        public override void Up()
        {
            Rename.Table("Persons").To("Members");
        }
        public override void Down()
        {
            Rename.Table("Members").To("Persons");
        }

        
    }
}
