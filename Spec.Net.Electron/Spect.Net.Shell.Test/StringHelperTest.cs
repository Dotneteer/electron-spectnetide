using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Spect.Net.Shell.Shared.Helpers;

namespace Spect.Net.Shell.Test
{
    [TestClass]
    public class StringHelperTest
    {
        [TestMethod]
        public void MenuNameSplittingWithFirstAcceleratorWorks()
        {
            // --- Act
            var (pre, access, post) = "&File".SplitMenuText();

            // --- Assert
            pre.ShouldBe("");
            access.ShouldBe("F");
            post.ShouldBe("ile");
        }

        [TestMethod]
        public void MenuNameSplittingWithInnerAcceleratorWorks()
        {
            // --- Act
            var (pre, access, post) = "V&iew".SplitMenuText();

            // --- Assert
            pre.ShouldBe("V");
            access.ShouldBe("i");
            post.ShouldBe("ew");
        }

        [TestMethod]
        public void MenuNameSplittingWithLastAcceleratorWorks()
        {
            // --- Act
            var (pre, access, post) = "Abor&t".SplitMenuText();

            // --- Assert
            pre.ShouldBe("Abor");
            access.ShouldBe("t");
            post.ShouldBe("");
        }

        [TestMethod]
        public void MenuNameSplittingWithPostAcceleratorWorks()
        {
            // --- Act
            var (pre, access, post) = "Lazy&".SplitMenuText();

            // --- Assert
            pre.ShouldBe("Lazy&");
            access.ShouldBe("");
            post.ShouldBe("");
        }

        [TestMethod]
        public void MenuNameSplittingWithNoAcceleratorWorks()
        {
            // --- Act
            var (pre, access, post) = "Drag and Drop".SplitMenuText();

            // --- Assert
            pre.ShouldBe("Drag and Drop");
            access.ShouldBe("");
            post.ShouldBe("");
        }
    }
}
