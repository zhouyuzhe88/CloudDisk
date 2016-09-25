using Common.Protocol;
using System;
using System.Text.RegularExpressions;

namespace Common.Util
{
    public static class Coder
    {
        private static string requestFormat = "<Request type=\"{0}\">{1}</Request>";
        private static string responseFormat = "<Response type=\"{0}\">{1}</Response>";
        private static Regex requestRegex = new Regex("<Request type=\"(?<Type>.*?)\">(?<Content>.*?)</Request>", RegexOptions.Compiled);
        private static Regex responseRegex = new Regex("<Response type=\"(?<Type>.*?)\">(?<Content>.*?)</Response>", RegexOptions.Compiled);

        public static string EncodeRequest(Request request)
        {
            return string.Format(requestFormat, request.Name, request.SerializeToJson());
        }

        public static string EncodeResponse(Response response)
        {
            return string.Format(responseFormat, response.Name, response.SerializeToJson());
        }

        public static Response DecodeResponse(string input)
        {
            var match = responseRegex.Match(input);
            if (match.Success)
            {
                string type = match.Groups["Type"].Value;
                string content = match.Groups["Content"].Value;
                Type responseType = ProtocalMapper.GetResponseType(type);
                return content.DeserializeFromJson(responseType) as Response;
            }
            return null;
        }

        public static Request DecodeRequest(string input)
        {
            var match = requestRegex.Match(input);
            if (match.Success)
            {
                string type = match.Groups["Type"].Value;
                string content = match.Groups["Content"].Value;
                Type requestType = ProtocalMapper.GetRequestType(type);
                return content.DeserializeFromJson(requestType) as Request;
            }
            return null;
        }
    }
}
