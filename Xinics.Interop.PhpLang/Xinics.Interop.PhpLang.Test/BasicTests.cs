using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xinics.Interop.PhpLang;

namespace Xinics.Interop.PhpLang.Test
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void DeserializeAssocArrayTest()
        {
            string strSerialized = "a:2:{s:7:\"content\";s:549:\"이케아 매장 주변에는 태양 전지판과 풍력 발전용 " +
                "터빈이 퍼져있습니다; 매장 안에는 전시 선반 LED 전구들과 재활용된 면으로 가득합니다. 왜 그럴까요? " +
                "이케아의 최고위 지속 가능 경영 책임자인 스티브 하워드는 &quot;환경 유지는 하면 좋은 것에서 꼭 " +
                "해야 되는 것으로 바뀌었다&quot; 라고 합니다. 이 강연에서 하워드는 친환경적인 경영을 해야하는 " + 
                "임무에 대해서 얘기하고 다른 경영자들에게도 도전을 권합니다.\";s:9:\"embed_tag\";s:228:\" " +
                "< iframe src = \"https://embed-ssl.ted.com/talks/steve_howard_let_s_go_all_in_on_selling_sustainability.html\" " +
                "width = \"502\" height = \"336\" frameborder = \"0\" scrolling = \"no\" webkitAllowFullScreen " +
                "mozallowfullscreen allowFullScreen ></ iframe > \";}";

            Serializer srlzr = new Serializer();
            object deserialized = srlzr.Deserialize(strSerialized);
            Hashtable assocDeserialized = deserialized as Hashtable;

            Assert.NotNull(assocDeserialized);

            string expectedContent = "이케아 매장 주변에는 태양 전지판과 풍력 발전용 터빈이 퍼져있습니다; 매장 안에는 전시 선반 LED 전구들과 재활용된 면으로 가득합니다. 왜 그럴까요? 이케아의 최고위 지속 가능 경영 책임자인 스티브 하워드는 &quot;환경 유지는 하면 좋은 것에서 꼭 해야 되는 것으로 바뀌었다&quot; 라고 합니다. 이 강연에서 하워드는 친환경적인 경영을 해야하는 임무에 대해서 얘기하고 다른 경영자들에게도 도전을 권합니다.";
            Assert.AreEqual(expectedContent, assocDeserialized["content"]);
        }
    }
}
