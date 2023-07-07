using System;
using System.Text.RegularExpressions;

namespace Threads;
public class ThreadsApi
{
    private string fbLSDToken { get; set; } = "NjppQDEgONsU_1LCzrmp6q";
    private HttpClient _client = new HttpClient();
    public async Task<int> GetUserIdFromUserName(string username)
    {

        var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.threads.net/@{username}");
        GetDefaultHeaders(username, request);

        request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        request.Headers.Add("Accept-Language", "ko,en;q=0.9,ko-KR;q=0.8,ja;q=0.7");
        request.Headers.Add("Pragma", "no-cache");
        request.Headers.Add("Referer", $"https://www.threads.net/@${username}");
        request.Headers.Add("Sec-Detch-Dest", "document");
        request.Headers.Add("Sec-Detch-Mode", "navigate");
        request.Headers.Add("Sec-Detch-Site", "cross-site");
        request.Headers.Add("Sec-Detch-User", "?1");
        request.Headers.Add("Upgrade-Insecure-Requests", "1");
        request.Headers.Add("X-asbd-id", "");
        request.Headers.Add("X-fb-lsd", "");
        request.Headers.Add("X-ig-app-id", "");


        request.Headers.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("PostmanRuntime", "7.32.3"));

        var response = await _client.SendAsync(request);
        var text = await response.Content.ReadAsStringAsync();

        text = Regex.Replace(text, @"\s", "");
        text = Regex.Replace(text, @"\n", "");

        string userID = Regex.Match(text, @"""props"":{""user_id"":""(\d+)""},")?.Groups[1].Value;
        string lsdToken = Regex.Match(text, @"""LSD"",\[\],{""token"":""(\w+)""},\d+\]")?.Groups[1].Value;
        this.fbLSDToken = lsdToken;

        return int.Parse(userID);
    }

    private void GetDefaultHeaders(string username, HttpRequestMessage request)
    {
        request.Headers.Add("Authority", "www.threads.net");
        request.Headers.Add("Cache-Control", "no-cache");
        request.Headers.Add("Origin", "https://www.threads.net");
        request.Headers.Add("x-fb-lsd", this.fbLSDToken);
        request.Headers.Add("Accept", "*/*");

    }
}