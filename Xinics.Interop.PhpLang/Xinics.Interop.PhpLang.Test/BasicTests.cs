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
                "임무에 대해서 얘기하고 다른 경영자들에게도 도전을 권합니다.\";s:9:\"embed_tag\";s:228:\"" +
                "<iframe src=\"https://embed-ssl.ted.com/talks/steve_howard_let_s_go_all_in_on_selling_sustainability.html\" " +
                "width=\"502\" height=\"336\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen " +
                "mozallowfullscreen allowFullScreen></iframe>\";}";

            Serializer srlzr = new Serializer();
            object deserialized = srlzr.Deserialize(strSerialized);
            Hashtable assocDeserialized = deserialized as Hashtable;

            Assert.NotNull(assocDeserialized);

            string expectedContent = "이케아 매장 주변에는 태양 전지판과 풍력 발전용 터빈이 퍼져있습니다; 매장 안에는 전시 선반 LED 전구들과 재활용된 면으로 가득합니다. 왜 그럴까요? 이케아의 최고위 지속 가능 경영 책임자인 스티브 하워드는 &quot;환경 유지는 하면 좋은 것에서 꼭 해야 되는 것으로 바뀌었다&quot; 라고 합니다. 이 강연에서 하워드는 친환경적인 경영을 해야하는 임무에 대해서 얘기하고 다른 경영자들에게도 도전을 권합니다.";
            Assert.AreEqual(expectedContent, assocDeserialized["content"]);

            string expectedEmbedTag = "<iframe src=\"https://embed-ssl.ted.com/talks/steve_howard_let_s_go_all_in_on_selling_sustainability.html\" width=\"502\" height=\"336\" frameborder=\"0\" scrolling=\"no\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>";
            Assert.AreEqual(expectedEmbedTag, assocDeserialized["embed_tag"]);
        }

        [TestCase]
        public void DeserializeSimpleObjectTest()
        {
            string strSerialized = "O:8:\"stdClass\":1:{s:10:\"data_media\";s:5:\"false\";}";

            Serializer srlzr = new Serializer();
            object deserialized = srlzr.Deserialize(strSerialized);
            Hashtable objDeserialized = deserialized as Hashtable;

            Assert.NotNull(objDeserialized);

            Assert.AreEqual("false", objDeserialized["data_media"]);
        }

        [TestCase]
        public void DeserializeNestedObjectTest()
        {
            string strSerialized = "O:8:\"stdClass\":1:{s:8:\"rsz_info\";O:8:\"stdClass\":4:{s:12:\"content_type\";s:3:\"rsz\";s:12:\"project_type\";s:5:\"rapid\";s:20:\"need_story_rendering\";b:0;s:20:\"recorded_story_count\";i:3;}}";

            Serializer srlzr = new Serializer();
            object deserialized = srlzr.Deserialize(strSerialized);
            Hashtable objDeserialized = deserialized as Hashtable;

            Assert.NotNull(objDeserialized);

            Hashtable rszInfo = objDeserialized["rsz_info"] as Hashtable;

            Assert.NotNull(rszInfo);
            Assert.AreEqual("rsz", rszInfo["content_type"]);
            Assert.AreEqual("rapid", rszInfo["project_type"]);
            Assert.AreEqual(false, rszInfo["need_story_rendering"]);
            Assert.AreEqual(3, rszInfo["recorded_story_count"]);

            strSerialized = "O:8:\"stdClass\":2:{s:8:\"rsz_info\";O:8:\"stdClass\":4:{s:12:\"content_type\";s:3:\"rsz\";s:12:\"project_type\";s:4:\"rich\";s:20:\"need_story_rendering\";b:1;s:20:\"recorded_story_count\";i:1;}s:10:\"data_media\";s:5:\"false\";}";

            deserialized = srlzr.Deserialize(strSerialized);
            objDeserialized = deserialized as Hashtable;

            Assert.NotNull(objDeserialized);
            Assert.AreEqual("false", objDeserialized["data_media"]);

            rszInfo = objDeserialized["rsz_info"] as Hashtable;

            Assert.NotNull(rszInfo);
            Assert.AreEqual("rsz", rszInfo["content_type"]);
            Assert.AreEqual("rich", rszInfo["project_type"]);
            Assert.AreEqual(true, rszInfo["need_story_rendering"]);
            Assert.AreEqual(1, rszInfo["recorded_story_count"]);
        }
    }
}
