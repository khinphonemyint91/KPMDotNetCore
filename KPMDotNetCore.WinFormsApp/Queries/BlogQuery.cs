﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMDotNetCore.WinFormsApp.Queries
{
    internal class BlogQuery
    {
        public static string BlogCreateQuery { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
        VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        public static string BlogList { get; } = @"SELECT [BlogId]
          ,[BlogTitle]
          ,[BlogAuthor]
          ,[BlogContent]
        FROM [dbo].[Tbl_Blog]";
    }
}
