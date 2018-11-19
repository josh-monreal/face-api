using FA.Business.Core;
using FA.Business.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using static FA.Business.Enumerations;

namespace FA.Business.Utilities
{
    public class ResponseHelper: IResponseHelper
    {
        public Response CreateResponse<T>(HttpResponseMessage result, string successMessage = null)
        {
            var (originalValue, defaultValue) = GetValues<T>(result);

            var response = new Response();

            if (result.IsSuccessStatusCode)
            {
                response.Data = originalValue;
                response.Status = Status.Successful;
                response.Message = successMessage;
            }
            else
            {
                response.Data = defaultValue;

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    response.Status = Status.Unauthorized;
                else
                    response.Status = Status.Unsuccessful;

                response.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>
                    (result.Content
                        .ReadAsStringAsync()
                        .Result);

                response.Message = response.ErrorResponse
                    .Error
                    .Message;
            }

            return response;
        }

        public string JsonPrettyPrint(HttpResponseMessage result, string successMessage = null)
        {
            var json = result.Content
                        .ReadAsStringAsync()
                        .Result;

            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }

        private (object originalValue, object defaultValue) GetValues<T>(HttpResponseMessage result)
        {
            if (typeof(T) == typeof(bool))
                return (originalValue: true, defaultValue: false);
            else
            {
                var data = JsonConvert.DeserializeObject<T>(result.Content
                        .ReadAsStringAsync()
                        .Result);

                return (originalValue: data, defaultValue: default(T));
            }
        }
    }
}
