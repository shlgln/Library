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
            Rename.Column("PersonId").OnTable("Entrusts").To("MemberId");
            Rename.Table("Persons").To("Members");
           
        }
        public override void Down()
        {
            Rename.Column("MemberId").OnTable("Entrusts").To("PersonId");
            Rename.Table("Members").To("Persons");
        }

        
    }
}
