using System.Collections.Generic;
using Ideal.Core.Model;
using Ideal.Core.Model.Logging;
using NUnit.Framework;

namespace Ideal.Core.Tests.Paging
{
    [TestFixture]
    public class PageTest
    {
        [TestFixture]
        public class ThePageConstructor
        {
            [TestCase(100,10,8,ExpectedResult = 11)]
            public int ShouldSetPageSizeAccurately(int collectionSize, int pageSize, int currentPage)
            {
                List<Log> logs = new List<Log>();
                for (int i = 0; i < collectionSize; i++)
                {
                    logs.Add(new Log() {EventCode = i});
                }

                Page<Log> pageOfLogs = new Page<Log>(logs,collectionSize,pageSize,currentPage);
                return pageOfLogs.PagesCount;
            }
        }
    }
}